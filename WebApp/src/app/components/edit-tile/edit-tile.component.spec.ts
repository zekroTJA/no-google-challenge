import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTileComponent } from './edit-tile.component';

describe('EditTileComponent', () => {
  let component: EditTileComponent;
  let fixture: ComponentFixture<EditTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditTileComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
