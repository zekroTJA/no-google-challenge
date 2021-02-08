import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRouteComponent } from './user-route.component';

describe('UserRouteComponent', () => {
  let component: UserRouteComponent;
  let fixture: ComponentFixture<UserRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
