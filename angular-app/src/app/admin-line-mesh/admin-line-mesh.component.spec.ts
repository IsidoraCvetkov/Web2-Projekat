import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminLineMeshComponent } from './admin-line-mesh.component';

describe('AdminLineMeshComponent', () => {
  let component: AdminLineMeshComponent;
  let fixture: ComponentFixture<AdminLineMeshComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminLineMeshComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminLineMeshComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
