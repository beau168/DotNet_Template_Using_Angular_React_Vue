import { TestBed } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { Router } from '@angular/router';
import { authInterceptor } from './auth.interceptor';
import { vi, describe, it, expect, beforeEach, afterEach } from 'vitest';

describe('AuthInterceptor', () => {
    let httpMock: HttpTestingController;
    let httpClient: HttpClient;
    let routerSpy: { navigate: any };

    beforeEach(() => {
        routerSpy = { navigate: vi.fn() };

        TestBed.configureTestingModule({
            providers: [
                provideHttpClient(withInterceptors([authInterceptor])),
                provideHttpClientTesting(),
                { provide: Router, useValue: routerSpy }
            ]
        });

        httpMock = TestBed.inject(HttpTestingController);
        httpClient = TestBed.inject(HttpClient);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should redirect to login on 401 error', () => {
        httpClient.get('/api/test').subscribe({
            error: () => { }
        });

        const req = httpMock.expectOne('/api/test');
        req.flush('Unauthorized', { status: 401, statusText: 'Unauthorized' });

        expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
    });

    it('should pass through other errors', () => {
        httpClient.get('/api/test').subscribe({
            error: (err) => {
                expect(err.status).toBe(500);
            }
        });

        const req = httpMock.expectOne('/api/test');
        req.flush('Server Error', { status: 500, statusText: 'Server Error' });

        expect(routerSpy.navigate).not.toHaveBeenCalled();
    });
});
