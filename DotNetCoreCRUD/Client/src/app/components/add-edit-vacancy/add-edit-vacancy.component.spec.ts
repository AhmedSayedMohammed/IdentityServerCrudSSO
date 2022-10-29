import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditVacancyComponent } from './add-edit-vacancy.component';

describe('AddEditVacancyComponent', () => {
  let component: AddEditVacancyComponent;
  let fixture: ComponentFixture<AddEditVacancyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditVacancyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditVacancyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
