import { TestBed } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { vi, describe, it, expect, beforeEach } from 'vitest';
import { of, throwError } from 'rxjs';

describe('AuthService', () => {
    let service: AuthService;
    let httpClientSpy: { get: any, post: any };
    let routerSpy: { navigate: any };

    beforeEach(() => {
        httpClientSpy = {
            get: vi.fn().mockReturnValue(of(null)), // Default for checkAuth
            post: vi.fn()
        };
        routerSpy = { navigate: vi.fn() };

        TestBed.configureTestingModule({
            providers: [
                AuthService,
                { provide: HttpClient, useValue: httpClientSpy },
                { provide: Router, useValue: routerSpy }
            ]
        });

        service = TestBed.inject(AuthService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('login should set user and navigate to dashboard', () => {
        const mockUser = { id: '1', email: 'test@test.com' };
        httpClientSpy.post.mockReturnValue(of(mockUser));

        service.login({ email: 'test@test.com', password: 'password' }).subscribe();

        expect(httpClientSpy.post).toHaveBeenCalled();
        expect(service.user()).toEqual(mockUser);
        expect(routerSpy.navigate).toHaveBeenCalledWith(['/dashboard']);
    });

    it('logout should clear user and navigate to login', () => {
        httpClientSpy.post.mockReturnValue(of({}));

        service.logout().subscribe();

        expect(service.user()).toBeNull();
        expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
    });
});
