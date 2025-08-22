
import { Component, OnInit } from '@angular/core';
import { CourseService } from '../course.service';
import { Course } from '../coursedetail/coursedetails.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from "../navbar/navbar.component";
import { AuthService } from '../Authservice';
import { Router } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, of, tap } from 'rxjs';
import { QuestionFormComponent } from '../question-form/question-form.component';

@Component({
  selector: 'app-instructorcourses',
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './instructorcourses.component.html',
  styleUrls: ['./instructorcourses.component.css'],
  providers:[QuestionFormComponent]
})
export class InstructorCoursesComponent implements OnInit {
  courses: Course[] = [];
  instructorID: number = 1; // Example instructor ID
  selectedCourse: Course | null = null;


  constructor(private courseService: CourseService
    ,private authService: AuthService,private router:Router,
    private http:HttpClient){ }

  ngOnInit(): void {
  this.getCoursesByInstructor();
    
  this.authService.getInstructorIdFromToken().subscribe(
        (instructorId: number) => {
          this.instructorID = instructorId;
          this.getCoursesByInstructor();
        },
        (error: any) => {
          console.error('Error fetching instructor ID:', error);
        }
      );
  
  
  }
  


  getCoursesByInstructor():void{

    this.courseService.getCoursesByInstructor(this.instructorID).subscribe({
            next: (data: Course[]) => {
              console.log(data);
              this.courses = data;
            },
            error: (error) => {
              console.error('Error fetching courses', error);
            },
            complete: () => {
              console.log('Courses fetch complete');
            }
          });
  }


  
ViewCourse(courseId: number): void {
   this.router.navigate(['/coursedetails',courseId]);
    }
  
editCourse(course: Course): void {
      this.selectedCourse = { ...course };
    }
  
    updateCourse(): void {
      if (this.selectedCourse) {
        this.courseService.updateCourse(this.selectedCourse).subscribe({
          next: (updatedCourse: Course) => {
            const index = this.courses.findIndex(course => course.courseId === updatedCourse.courseId);
            if (index !== -1) {
              this.courses[index] = updatedCourse;
            }
            this.selectedCourse = null;
          },
          error: (error) => {
            console.error('Error updating course', error);
          },
          complete: () => {
            console.log('Course update complete');
          }
        });
      }
    }
  
    cancelEdit(): void {
      this.selectedCourse = null;
    }



  
    addAssessment(courseId: number): void {
    //  this.questionform.addAssessmet.emit(courseId);
        this.router.navigate(['/question-form',courseId])
    
  }
}

