
import { Component, OnInit } from '@angular/core';
import { CourseService } from '../course.service';
import { Console } from 'console';
import { CommonModule } from '@angular/common';
import { Router,RouterOutlet } from '@angular/router';
import { Course } from '../coursedetail/coursedetails.model';
import { NavbarComponent } from "../navbar/navbar.component";
import { MyCoursesComponent } from '../mycourses/mycourses.component';
import { MyCoursesService } from '../mycourses.service';
import { of } from 'rxjs';
import { catchError, switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-courses',
  standalone:true,
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
  imports: [CommonModule, RouterOutlet, NavbarComponent],
})

export class CoursesComponent implements OnInit {
  courses: Course[] = [];
  enrolledCourses: Set<number> = new Set();

  constructor(private courseService: CourseService,private router:Router,private mycourseService:MyCoursesService) {}

  ngOnInit(): void {
    this.getCourses();
    
    this.loadCourses();

  }

  getCourses(): void {
    this.courseService.getCourses().subscribe(data => {
      console.log(data);
      this.courses = data;
        //console.log(this.courses);
      });
  }
  
    loadCourses(): void {
        this.courseService.getCourses().subscribe({
          next: (courses) => {
            this.courses = courses;
          },
          error: (error) => {
            console.error('Error fetching courses:', error);
          }
        });
      }
    
      EnrollCourse(courseId: number): void {
    //     this.router.navigate(['/coursedetails', courseId]);
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
  
    
     
  
 