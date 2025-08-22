import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstructorCoursesComponent } from './instructorcourses.component';

describe('InstructorcoursesComponent', () => {
  let component: InstructorCoursesComponent;
  let fixture: ComponentFixture<InstructorCoursesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InstructorCoursesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InstructorCoursesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
