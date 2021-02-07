import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListsRouteComponent } from './lists-route.component';

describe('ListsRouteComponent', () => {
  let component: ListsRouteComponent;
  let fixture: ComponentFixture<ListsRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListsRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListsRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
