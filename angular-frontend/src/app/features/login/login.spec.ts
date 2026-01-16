import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login';
import { AuthService } from '../../core/auth/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { vi, describe, it, expect, beforeEach } from 'vitest';

describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;
    let authServiceSpy: { login: any };

    beforeEach(async () => {
        authServiceSpy = { login: vi.fn() };

        await TestBed.configureTestingModule({
            imports: [LoginComponent, ReactiveFormsModule],
            providers: [
                { provide: AuthService, useValue: authServiceSpy }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should call authService.login on valid form submit', () => {
        authServiceSpy.login.mockReturnValue(of({ id: '1', email: 'test@test.com' }));

        component.loginForm.controls['email'].setValue('test@test.com');
        component.loginForm.controls['password'].setValue('password');

        component.onSubmit();

        expect(authServiceSpy.login).toHaveBeenCalledWith({
            email: 'test@test.com',
            password: 'password'
        });
    });

    it('should not call authService.login on invalid form submit', () => {
        component.loginForm.controls['email'].setValue(''); // Invalid
        component.onSubmit();
        expect(authServiceSpy.login).not.toHaveBeenCalled();
    });
});
