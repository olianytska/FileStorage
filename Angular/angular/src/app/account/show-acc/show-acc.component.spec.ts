import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAccComponent } from './show-acc.component';

describe('ShowAccComponent', () => {
  let component: ShowAccComponent;
  let fixture: ComponentFixture<ShowAccComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowAccComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowAccComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
