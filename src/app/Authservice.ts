import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, switchMap, throwError } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { log } from 'console';
 
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl_login = 'https://localhost:7219/api/User';
  private apiUrl_register = 'https://localhost:7219/api/User';
  private apiurl_stu='https://localhost:7219/api/Role/student';
  private apiurl_instr='https://localhost:7219/api/Role/instructor'
  private jwtHelper = new JwtHelperService();
  private tokenKey: string = 'token'; // Key used to store the token in local storage
 
  constructor(private http: HttpClient) { }
 
  register(user: { name: string, email: string, password: string, role: string }): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(`${this.apiUrl_register}/register`, user, { headers });
  }
 
  login(credentials: { email: string, password: string, role: string }): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(`${this.apiUrl_login}/login`, credentials, { headers });
  }
 
  saveToken(token: string): void {
    if (this.isLocalStorageAvailable()) {
      localStorage.setItem(this.tokenKey, token);
    }
  }
 
  getToken(): string | null {
    if (this.isLocalStorageAvailable()) {
      console.log("entered the islocalstorage function");
      const dy = localStorage.getItem(this.tokenKey);
      console.log(dy);
      return dy;
    }
    return null;
  }
 
  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }
 
  isAuthenticated(): boolean {
    const token = this.getToken();
    return token ? !this.isTokenExpired(token) : false;
  }
 
  isTokenExpired(token: string): boolean {
    return this.jwtHelper.isTokenExpired(token);
  }
 
  getDecodedToken(): any {
    const token = this.getToken();
    return token ? this.jwtHelper.decodeToken(token) : null;
  }
 
  getEmail(): string | null {
    const decodedToken = this.getDecodedToken();
    return decodedToken ? decodedToken.email : null;
  }
  getRole(): string | null {
    const decodedToken = this.getDecodedToken();
    return decodedToken ? decodedToken.role : null;
  }
 
  /**
   * New Method: Fetches studentId using the email from the JWT token
   */
  getStudentIdFromToken(): Observable<number> {
    const email = this.getEmail();
    if (!email) {
      console.error("Email not found in token.");
      return throwError(() => new Error("Email not found in token."));
    }
 
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
 
    return this.http.get<{ userID: number }>(`${this.apiUrl_login}/GetbyEmail/${email}`, { headers }).pipe(
      switchMap((user) => {
        const userId = user.userID;
        return this.http.get<number>(`${this.apiurl_stu}/${userId}`, { headers });
      })
    );
  }
 
  getInstructorIdFromToken(): Observable<number> {
    const email = this.getEmail();
    if (!email) {
      console.error("Email not found in token.");
      return throwError(() => new Error("Email not found in token."));
    }
 
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
 
    return this.http.get<{ userID: number }>(`${this.apiUrl_login}/GetbyEmail/${email}`, { headers }).pipe(
      switchMap((user) => {
        const userId = user.userID;
        return this.http.get<number>(`${this.apiurl_instr}/${userId}`, { headers });
      })
    );
  }
 
  private isLocalStorageAvailable(): boolean {
    try {
      const testKey = '__test__';
      localStorage.setItem(testKey, testKey);
      localStorage.removeItem(testKey)
      return true;
    } catch (e) {
      return false;
    }
  }
}
 