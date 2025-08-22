import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from './coursedetail/coursedetails.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private apiUrl = 'https://localhost:7219/api/Course';
  private ApiUrl ='https://localhost:7219/api/Assessment';

  constructor(private http: HttpClient) {}

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.apiUrl);
  }

  getCourseById(id: number): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`);
  }


getCoursesByInstructor(instructorID: number): Observable<Course[]> {
      return this.http.get<Course[]>(`${this.apiUrl}/instructor/${instructorID}`);
    }
  
   deleteCourse(courseId: number): Observable<any> {
        return this.http.delete<any>(`${this.apiUrl}/${courseId}`);
     }
    
  updateCourse(course: Course): Observable<Course> {
      return this.http.put<Course>(`${this.apiUrl}/${course.courseId}`, course);
    }
  

    searchCourses(title: string): Observable<any[]> {
        return this.http.get<any[]>(`${this.apiUrl}/search?title=${title}`);
      }
    

    
}
