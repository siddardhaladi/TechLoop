import { Component } from '@angular/core';
import { CourseService } from '../course.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from 'express';
import { ActivatedRoute } from '@angular/router';
import { NavbarComponent } from "../navbar/navbar.component";
import { MyCoursesService } from '../mycourses.service';
import { catchError, of, tap } from 'rxjs';

@Component({
  selector: 'app-coursesearch',
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './coursesearch.component.html',
  styleUrl: './coursesearch.component.css'
})
export class CoursesearchComponent {
    searchQuery: string = '';
    courses: any[] = [];
  enrolledCourses: Set<number> = new Set();
  
    constructor(private courseservice: CourseService,private route:ActivatedRoute,
    private mycourseService:MyCoursesService
  ) {}
  
    ngOnInit() {
        this.route.queryParams.subscribe(params => {
          this.searchQuery = params['title'] || '';
          if (this.searchQuery) {
            this.searchCourses();
          }
        });
      }
    

  searchCourses() {
    this.courseservice.searchCourses(this.searchQuery).subscribe({
    next: (data) => {
    this.courses = data;
     },
    error: (error) => {
    console.error('Error fetching courses', error);
    },
    complete: () => {
    console.log('Search completed');
     }
    });
  }
  
  EnrollCourse(courseId: number): void {
    //     this.router.navigate(['/coursedetails', courseId]);
          //  this.mycourseService.enrollInCourse(courseId)
          this.mycourseService.enrollInCourse(courseId).pipe(
            tap(() => {
              this.enrolledCourses.add(courseId);
              console.log('Enrollment successful!');
              // You can add a redirect or any other action here if needed
            }),
            catchError(err => {
              console.error('Enrollment failed:', err);
              return of(null); // Prevents crash on error
            })
          ).subscribe();
        }
       
         
            isEnrolled(courseId: number): boolean {
              return this.enrolledCourses.has(courseId);
            }
  }
  
