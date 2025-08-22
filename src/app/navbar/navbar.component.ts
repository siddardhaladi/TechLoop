

// import { Component } from '@angular/core';
// import { Router } from '@angular/router';
// import { AuthService } from '../Authservice';
// import { CommonEngine } from '@angular/ssr/node';
// import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';
// import { CoursesearchComponent } from '../coursesearch/coursesearch.component';
 
// @Component({
//   selector: 'app-navbar',
//   standalone:true,
//   imports: [CommonModule,FormsModule],
//   templateUrl: './navbar.component.html',
//   styleUrl: './navbar.component.css'
// })
// export class NavbarComponent {
//   role: any;
//   searchQuery: any;
//   constructor(private router: Router,
//     private authservice:AuthService
//   ) {}
  
// ngOnInit(): void {
//     this.role = this.authservice.getRole();
//     if (!this.role) {
//       console.error('Error fetching role');
//     }
//   }

  
//     goToMyCourses(): void {
//       if (this.role === 'Instructor') {
//         this.router.navigate(['/instructorcourses']);
//       } else if (this.role === 'Student') {
//         this.router.navigate(['/mycourse']);
//       } else {
//         console.error('Unknown role:', this.role);
//       }
//     }
  
 
//   goToHome(): void {
//     this.router.navigate(['/Homepage']);
//   }
 
//   goToCourses(): void {
//     this.router.navigate(['/course']);
//   }
 
 
//   goToAbout(): void {
//     this.router.navigate(['/about']);
//      this.router.navigate(['/contact']);
//   }
 
//   logout(): void {
//     // Implement logout functionality
//     this.authservice.logout()
//     this.router.navigate(['/'])
//   }
 
//   goToAddCourses(): void {
//     if (this.role === 'Instructor') {
//     this.router.navigate(['/add-course']);
//     }else if(this.role==='Student'){
//       alert('Only Instructors Can Access');
//     }
//   }
//   searchCourses() {
//         this.router.navigate(['/coursesearch'], { queryParams: { title: this.searchQuery } });
//       }
 
//   // selectCourse(course: any): void {
//   //   this.selectedCourse = course;
//   // }
 
// }
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Authservice';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
 
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  role: any;
  userEmail: string | null = null;
  searchQuery: any;
 
  constructor(private router: Router, private authservice: AuthService) {}
 
  ngOnInit(): void {
    this.role = this.authservice.getRole();
    this.userEmail = this.authservice.getEmail(); // Fetch user email
 
    if (!this.role) {
      console.error('Error fetching role');
    }
 
    if (!this.userEmail) {
      console.error('Error fetching email');
    }
  }
 
  goToMyCourses(): void {
    if (this.role === 'Instructor') {
      this.router.navigate(['/instructorcourses']);
    } else if (this.role === 'Student') {
      this.router.navigate(['/mycourse']);
    } else {
      console.error('Unknown role:', this.role);
    }
  }
 
  goToHome(): void {
    this.router.navigate(['/Homepage']);
  }
 
  goToCourses(): void {
    this.router.navigate(['/course']);
  }
 
  goToContact(): void {
    this.router.navigate(['/contact']);
  }
 
  logout(): void {
    this.authservice.logout();
    this.router.navigate(['/']);
  }
 
  goToAddCourses(): void {
    if (this.role === 'Instructor') {
      this.router.navigate(['/add-course']);
    } else if (this.role === 'Student') {
      alert('Only Instructors Can Access');
    }
  }
 
  searchCourses() {
    this.router.navigate(['/coursesearch'], { queryParams: { title: this.searchQuery } });
  }
}
 