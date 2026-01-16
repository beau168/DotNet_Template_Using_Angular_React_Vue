import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SignupComponent } from './signup';
import { AuthService } from '../../core/auth/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { vi, describe, it, expect, beforeEach } from 'vitest';

describe('SignupComponent', () => {
    let component: SignupComponent;
    let fixture: ComponentFixture<SignupComponent>;
    let authServiceSpy: { signup: any };

    beforeEach(async () => {
        authServiceSpy = { signup: vi.fn() };

        await TestBed.configureTestingModule({
            imports: [SignupComponent, ReactiveFormsModule],
            providers: [
                { provide: AuthService, useValue: authServiceSpy }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(SignupComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should call authService.signup on valid form submit', () => {
        authServiceSpy.signup.mockReturnValue(of({ id: '1', email: 'test@test.com' }));

        component.signupForm.controls['email'].setValue('test@test.com');
        component.signupForm.controls['password'].setValue('password123');

        component.onSubmit();

        expect(authServiceSpy.signup).toHaveBeenCalledWith({
            email: 'test@test.com',
            password: 'password123'
        });
    });

    it('should not call authService.signup on invalid form submit', () => {
        component.signupForm.controls['email'].setValue('invalid-email');
        component.onSubmit();
        expect(authServiceSpy.signup).not.toHaveBeenCalled();
    });
});
