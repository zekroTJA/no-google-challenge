import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HoverNavigationComponent } from './hover-navigation.component';

describe('HoverNavigationComponent', () => {
  let component: HoverNavigationComponent;
  let fixture: ComponentFixture<HoverNavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HoverNavigationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HoverNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
