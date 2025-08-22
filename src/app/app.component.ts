
import {Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { LoginComponent } from './Login.component';
import { HomeComponent } from "./Homepage/Homepage.component";
import { CourseService } from './course.service';
import { CoursesComponent } from './course/course.component';
 
 
@Component({
  selector: 'app-root',
  standalone:true,
  imports: [CommonModule,FormsModule, RouterModule,CoursesComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'e_lproject';
}
