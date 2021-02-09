namespace ToDoList.Extensions
{
    /// <summary>
    /// Integer extension methods.
    /// </summary>
    public static class IntegerExtension
    {
        /// <summary>
        /// Returns max if i is larger than max.
        /// Otherwise, i is returned.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Cap(this int i, int max) =>
            i > max ? max : i;
    }
}
