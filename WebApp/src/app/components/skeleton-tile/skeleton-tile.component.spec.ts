import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkeletonTileComponent } from './skeleton-tile.component';

describe('SkeletonTileComponent', () => {
  let component: SkeletonTileComponent;
  let fixture: ComponentFixture<SkeletonTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SkeletonTileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SkeletonTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
