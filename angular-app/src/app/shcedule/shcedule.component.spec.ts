import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShceduleComponent } from './shcedule.component';

describe('ShceduleComponent', () => {
  let component: ShceduleComponent;
  let fixture: ComponentFixture<ShceduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShceduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShceduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
