import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const router = inject(Router);

    // Clone request to ensure credentials are sent if needed, though AuthService handles 'withCredentials: true' manually.
    // We can force it here or just handle errors.

    return next(req).pipe(
        catchError(err => {
            if (err.status === 401) {
                // Redirect to login on unauthorized access
                router.navigate(['/login']);
            }
            return throwError(() => err);
        })
    );
};
