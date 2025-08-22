
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';
import { CoursesearchComponent } from "../coursesearch/coursesearch.component";
 
@Component({
  selector: 'app-homepage',
  standalone:true,
  imports: [FormsModule, CommonModule, NavbarComponent, CoursesearchComponent],
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomeComponent {
  courses = [
    // { image: 'assets/images/course1.jpg', title: 'Course 1', description: 'Description for Course 1' },
    // { image: 'assets/images/course2.jpg', title: 'Course 2', description: 'Description for Course 2' },
    // { image: 'assets/images/course3.jpg', title: 'Course 3', description: 'Description for Course 3' }
  ];
  selectedCourse: any;
  searchQuery: any;
  
constructor(private router: Router) {}
  enroll(){
        this.router.navigate(['/course']);

  }
  goToCourses() {
    this.router.navigate(['/course']); 

     }
}