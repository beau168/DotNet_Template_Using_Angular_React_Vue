import { Injectable, signal, computed, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap, catchError, of } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface User {
  id: string;
  email: string;
  firstName?: string;
  lastName?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  // State
  private userSignal = signal<User | null>(null);

  // Selectors
  readonly user = this.userSignal.asReadonly();
  readonly isAuthenticated = computed(() => !!this.userSignal());

  private readonly API_URL = `${environment.apiBaseUrl}/auth`;

  constructor() {
    this.checkAuth().subscribe();
  }

  checkAuth() {
    return this.http.get<User>(`${this.API_URL}/me`, { withCredentials: true }).pipe(
      tap(user => this.userSignal.set(user)),
      catchError(() => {
        this.userSignal.set(null);
        return of(null);
      })
    );
  }

  login(credentials: any) {
    return this.http.post<User>(`${this.API_URL}/login`, credentials, { withCredentials: true }).pipe(
      tap(user => {
        this.userSignal.set(user);
        this.router.navigate(['/dashboard']);
      })
    );
  }

  signup(credentials: any) {
    return this.http.post<User>(`${this.API_URL}/signup`, credentials, { withCredentials: true }).pipe(
      tap(user => {
        this.userSignal.set(user);
        this.router.navigate(['/dashboard']);
      })
    );
  }

  logout() {
    return this.http.post(`${this.API_URL}/logout`, {}, { withCredentials: true }).pipe(
      tap(() => {
        this.userSignal.set(null);
        this.router.navigate(['/login']);
      })
    );
  }
}
