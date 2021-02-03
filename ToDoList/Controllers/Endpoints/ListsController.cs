using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Filters;
using ToDoList.Models;
using ToDoList.Models.Requests;
using ToDoList.Models.Responses;

namespace ToDoList.Controllers.Endpoints
{
    /// <summary>
    /// Endpoint controller to create, modify and remove lists
    /// as well as list entries.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(RequiresAuthorization))]
    public class ListsController : AuthorizedControllerBase
    {
        private readonly Context db;

        public ListsController(Context _db)
        {
            db = _db;
        }

        // -------------------------------------------------------------------------------
        // --- GET /api/lists

        [HttpGet]
        public async Task<ActionResult<List<TodoList>>> GetLists()
        {
            var myLists = await db.Lists
                .Where(l => l.Owner.Id == AuthorizedUser.Id)
                .Select(l => new TodoListView(l, false))
                .ToListAsync();

            return Ok(myLists);
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/lists

        [HttpPost]
        public async Task<ActionResult<TodoListView>> GetLists([FromBody] ListCreate list)
        {
            var createdList = new TodoList()
            {
                Name = list.Name,
                Owner = AuthorizedUser,
            };

            db.Add(createdList);
            await db.SaveChangesAsync();

            return Created($"/api/lists/{createdList.Id}", new TodoListView(createdList));
        }

        // -------------------------------------------------------------------------------
        // --- GET /api/lists/:id

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListView>> GetList([FromRoute] Guid id)
        {
            var list = await db.Lists.FindAsync(id);
            if (list == null || list.Owner.Id != AuthorizedUser.Id)
                return NotFound();

            return Ok(new TodoListView(list));
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/lists/:id

        [HttpPost("{id}")]
        public async Task<ActionResult<TodoListView>> UpdateList(
            [FromRoute] Guid id,
            [FromBody] ListCreate updatedList)
        {
            var list = await db.Lists.FindAsync(id);
            if (list == null || list.Owner.Id != AuthorizedUser.Id)
                return NotFound();

            list.Name = updatedList.Name;

            db.Update(list);
            await db.SaveChangesAsync();

            return Ok(new TodoListView(list));
        }

        // -------------------------------------------------------------------------------
        // --- DELETE /api/lists/:id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList([FromRoute] Guid id)
        {
            var list = await db.Lists.FindAsync(id);
            if (list == null || list.Owner.Id != AuthorizedUser.Id)
                return NotFound();

            db.Remove(list);
            await db.SaveChangesAsync();

            return NoContent();
        }

        // -------------------------------------------------------------------------------
        // --- GET /api/lists/:id/entries

        [HttpGet("{id}/entries")]
        public async Task<ActionResult<List<TodoEntryView>>> GetListEntries([FromRoute] Guid id)
        {
            var list = await db.Lists.FindAsync(id);
            if (list == null || list.Owner.Id != AuthorizedUser.Id)
                return NotFound();

            var entries = await db.Entries
                .Where(e => e.ContainedIn.Id == list.Id)
                .Select(e => new TodoEntryView(e, false, false))
                .ToListAsync();

            return Ok(entries);
        }

        // -------------------------------------------------------------------------------
        // --- GET /api/lists/:id/entries/:id

        [HttpGet("{id}/entries/{entryId}")]
        public async Task<ActionResult<TodoEntryView>> GetListEntry(
            [FromRoute] Guid id,
            [FromRoute] Guid entryId)
        {
            var entry = await GetListEntryChecked(id, entryId);
            if (entry == null)
                return NotFound();

            return Ok(new TodoEntryView(entry));
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/lists/:id/entries

        [HttpPost("{id}/entries")]
        public async Task<ActionResult<TodoEntryView>> AddListEntry(
            [FromRoute] Guid id,
            [FromBody] ListEntryCreate entry)
        {
            var list = await db.Lists.FindAsync(id);
            if (list == null || list.Owner.Id != AuthorizedUser.Id)
                return NotFound();

            if (string.IsNullOrEmpty(entry.Content))
                return BadRequest("content value can not be null or empty");

            var createdEntry = new TodoEntry()
            {
                Content = entry.Content,
                Checked = entry.Checked ?? false,
                ContainedIn = list,
            };

            db.Add(createdEntry);
            await db.SaveChangesAsync();

            return Created(
                $"/api/lists/{list.Id}/entries/{createdEntry.Id}", 
                new TodoEntryView(createdEntry));
        }

        // -------------------------------------------------------------------------------
        // --- POST /api/lists/:id/entries/:id

        [HttpPost("{id}/entries/{entryId}")]
        public async Task<ActionResult<TodoEntryView>> UpdateListEntry(
            [FromRoute] Guid id,
            [FromRoute] Guid entryId,
            [FromBody] ListEntryCreate updatedEntry)
        {
            var entry = await GetListEntryChecked(id, entryId);
            if (entry == null)
                return NotFound();

            entry.Content = updatedEntry.Content ?? entry.Content;
            entry.Checked = updatedEntry.Checked ?? entry.Checked;

            db.Update(entry);
            await db.SaveChangesAsync();

            return Ok(new TodoEntryView(entry));
        }

        // -------------------------------------------------------------------------------
        // --- DELETE /api/lists/:id/entries/:id

        [HttpDelete("{id}/entries/{entryId}")]
        public async Task<IActionResult> DeleteListEntry(
            [FromRoute] Guid id,
            [FromRoute] Guid entryId)
        {
            var entry = await GetListEntryChecked(id, entryId);
            if (entry == null)
                return NotFound();

            db.Remove(entry);
            await db.SaveChangesAsync();

            return NoContent();
        }

        // -------------------------------------------------------------------------------
        // --- HELPER FUNCTIONS

        private async Task<TodoEntry> GetListEntryChecked(Guid id, Guid entryId)
        {
            var list = await db.Lists.FindAsync(id);
            if (list == null || list.Owner.Id != AuthorizedUser.Id)
                return null;

            var entry = await db.Entries.FindAsync(entryId);
            if (entry == null || entry.ContainedIn.Id != id || entry.ContainedIn.Owner.Id != AuthorizedUser.Id)
                return null;

            return entry;
        }
    }
}
