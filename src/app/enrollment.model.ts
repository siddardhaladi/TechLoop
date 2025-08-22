export interface Enrollment {
    EnrollmentId: number; // Primary key
    StudentId: number;
    CourseId: number;
    Progress: number; // You can change the type based on your requirements, e.g., number or enum
  }
 
 