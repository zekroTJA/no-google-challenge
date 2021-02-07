import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListRouteComponent } from './list-route.component';

describe('ListRouteComponent', () => {
  let component: ListRouteComponent;
  let fixture: ComponentFixture<ListRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
