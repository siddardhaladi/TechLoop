import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Homepage/Homepage.component';
import { AppComponent } from './app.component';
import { CoursesComponent } from './course/course.component';
import { CoursedetailComponent } from './coursedetail/coursedetails.component';
import { LoginComponent } from './Login.component';
import { AddCourseComponent } from './addcourse/addcourse.component';
import { InstructorCoursesComponent } from './instructorcourses/instructorcourses.component';
import { MyCoursesComponent } from './mycourses/mycourses.component';
import { CoursesearchComponent } from './coursesearch/coursesearch.component';
import { QuestionFormComponent } from './question-form/question-form.component';
import { QuestionListComponent } from './question-list/question-list.component';
import { ContactComponent } from './contact/contact.component';
import { AuthGuard } from './Authgaurd';
 
 
 
 
 
export const routes: Routes = [
{ path: '', component:LoginComponent },
{ path:'Homepage', component:HomeComponent,canActivate: [AuthGuard]},
{ path: 'add-course', component:AddCourseComponent,canActivate: [AuthGuard]},
{ path: 'course', component:CoursesComponent,canActivate: [AuthGuard]},
{ path: 'mycourse', component:MyCoursesComponent,canActivate: [AuthGuard]},
{ path: 'coursedetails/:courseId', component:CoursedetailComponent,canActivate: [AuthGuard]},
{ path: 'instructorcourses', component:InstructorCoursesComponent,canActivate: [AuthGuard]},
{ path: 'coursesearch', component:CoursesearchComponent,canActivate: [AuthGuard]},
{ path: 'question-form/:courseId', component:QuestionFormComponent,canActivate: [AuthGuard]},
{ path: 'question-list/:assessmentId', component:QuestionListComponent,canActivate: [AuthGuard]},
{path:'contact', component: ContactComponent}
];
 
export class AppRoutingModule { }