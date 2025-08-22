import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, Observable, of, switchMap, tap } from 'rxjs';
import { Enrollment } from './enrollment.model';
import { AuthService } from './Authservice';
 
@Injectable({
  providedIn: 'root'
})
export class MyCoursesService {
  private apiUrl = 'https://localhost:7219/api/Enrollment';
 
  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}
 
  // Get the student ID by calling AuthService
  getStudentIdFromToken(): Observable<number> {
    return this.authService.getStudentIdFromToken();
  }
 
  // Post the enrollment data to the backend API
  enrollStudentInCourse(studentId: number, courseId: number, progress: number = 0): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
 
    const enrollmentData = {
      studentId: studentId,
      courseId: courseId,
      progress: progress
    };
 
    return this.http.post(this.apiUrl, enrollmentData, { headers });
  }
 
  getEnrollments(studentId: number): Observable<Enrollment[]> {
    return this.http.get<Enrollment[]>(`${this.apiUrl}/student/${studentId}/courses`);
  }
  
  enrollInCourse(courseId: number): Observable<any> {
    return this.getStudentIdFromToken().pipe(
      switchMap(studentId => {
        const progress = 0; // Default progress set
        return this.enrollStudentInCourse(studentId, courseId, progress);
      })
    );
  }
 
}
 
 