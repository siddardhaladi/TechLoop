import { Component } from '@angular/core';
import { AddCourseService } from './addcourse.service';
import { AddCourse } from './addcourse.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "../navbar/navbar.component";
import { AuthService } from '../Authservice';

@Component({
  selector: 'app-addcourse',
  imports: [CommonModule, FormsModule, RouterOutlet, NavbarComponent],
  templateUrl: './addcourse.component.html',
  styleUrls: ['./addcourse.component.css']
})
export class AddCourseComponent {
  addcourse: AddCourse = {
    title: '',
    description: '',
    contentURL: '',
    instructorId: 1,
  };

  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private addCourseService: AddCourseService,
    private router: Router,
    private authservice: AuthService
  ) {}

  AddCourse() {
    this.addCourseService.AddCourse(this.addcourse).subscribe({
      next: (response) => {
        console.log('AddCourse added:', response);

        // Show success message
        this.successMessage = ' Course added successfully!';
        this.errorMessage = '';

        //  Reset form
        this.addcourse = { title: '', description: '', contentURL: '', instructorId: this.addcourse.instructorId };

      },
      error: (err) => {
        console.error('Error adding course:', err);
        this.errorMessage = ' Failed to add course. Please try again!';
        this.successMessage = '';
      }
    });
  }

  ngOnInit(): void {
    this.authservice.getInstructorIdFromToken().subscribe(
      (instructorId) => {
        this.addcourse.instructorId = instructorId;
      },
      (error) => {
        console.error('Error fetching instructor ID:', error);
      }
    );
  }
}
