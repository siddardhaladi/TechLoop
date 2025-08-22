import { Component, OnInit } from '@angular/core';
import { AuthService } from '../Authservice';
import { CourseService } from '../course.service';
import { Router } from '@angular/router';
import { NavbarComponent } from "../navbar/navbar.component";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MyCoursesService } from '../mycourses.service';

@Component({
  selector: 'app-mycourses',
  imports: [NavbarComponent,FormsModule,CommonModule],
  templateUrl: './mycourses.component.html',
  styleUrl: './mycourses.component.css'
})
export class MyCoursesComponent implements OnInit {
  enrollInCourse(courseId: number) {
    throw new Error('Method not implemented.');
  }
  courses: any[] = [];
  studentId: number | null = null;
  
 
  constructor(private courseService: CourseService,
     private authService: AuthService,
     private mycourseService: MyCoursesService,

    private router:Router) {}
 
  ngOnInit(): void {
    this.authService.getStudentIdFromToken().subscribe({
      next: (id) => {
        this.studentId = id;
        this.loadCourses();
      },
      error: (err) => console.error('Error fetching student ID:', err)
    });
  }
  
 
  getCourses(): void {
    if (this.studentId !== null) {
      this.mycourseService.getEnrollments(this.studentId).subscribe(data => {
        console.log(data);
        this.courses = data;
      });
    }
  }
 
  loadCourses(): void {
    if (this.studentId !== null) {
      this.mycourseService.getEnrollments(this.studentId).subscribe({
        next: (courses) => {
          this.courses = courses;
        },
        error: (error) => {
          console.error('Error fetching courses:', error);
        }
      });
 
     
    }
 
  }
  viewCourseDetails(courseId: number): void {
    this.router.navigate(['/coursedetails', courseId]);
  }
}
 


 