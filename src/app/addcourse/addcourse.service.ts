import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddCourse } from './addcourse.model';

@Injectable({
  providedIn: 'root'
})
export class AddCourseService {
  private apiUrl = 'https://localhost:7219/api/Course';

  constructor(private http: HttpClient) {}

  AddCourse(addcourse: AddCourse): Observable<AddCourse> {
    return this.http.post<AddCourse>(this.apiUrl, addcourse);
  }

  getAddCourses(): Observable<AddCourse[]> {
    return this.http.get<AddCourse[]>(this.apiUrl);
  }
}
