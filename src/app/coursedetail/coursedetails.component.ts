import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from './coursedetails.model';
import { CommonModule } from '@angular/common';
import { CourseService } from '../course.service';
import { NavbarComponent } from "../navbar/navbar.component";
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-coursedetails',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    FormsModule,
  ],
  templateUrl: './coursedetails.component.html',
  styleUrls: ['./coursedetails.component.css']
})
export class CoursedetailComponent implements OnInit {
  course: Course | undefined;
  courseId!: number;
  assessmentId: number | null = null;
 
  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService,
    private router: Router
  ) {}
 
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const courseId = Number(params['courseId']);
      this.courseId = courseId;
      this.fetchCourseDetails(courseId);
    });
  }
 
  fetchCourseDetails(id: number): void {
    this.courseService.getCourseById(id).subscribe({
      next: (course) => {
        this.course = course;
      },
      error: (error) => {
        console.error('Error fetching course details:', error);
      },
      complete: () => {
        console.log('Course details fetched successfully');
      }
    });
  }
 
  goToAssessment(): void {
    if (this.assessmentId) {
      this.router.navigate(['/question-list', this.assessmentId]);
    }
  }
}
