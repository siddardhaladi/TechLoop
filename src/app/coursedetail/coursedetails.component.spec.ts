import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoursedetailComponent } from './coursedetails.component';

describe('CourseComponent', () => {
  let component: CoursedetailComponent;
  let fixture: ComponentFixture<CoursedetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoursedetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoursedetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
