import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../core/auth/auth.service';

@Component({
    selector: 'app-signup',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    template: `
    <div class="signup-container">
      <h2>Sign Up</h2>
      <form [formGroup]="signupForm" (ngSubmit)="onSubmit()">
        <div>
          <label>Email</label>
          <input type="email" formControlName="email">
        </div>
        <div>
          <label>Password</label>
          <input type="password" formControlName="password">
        </div>
        <button type="submit" [disabled]="!signupForm.valid">Sign Up</button>
      </form>
    </div>
  `,
    styles: [`
    .signup-container { max-width: 400px; margin: 2rem auto; padding: 1rem; border: 1px solid #ccc; }
    div { margin-bottom: 1rem; }
    label { display: block; }
    input { width: 100%; padding: 0.5rem; }
  `]
})
export class SignupComponent {
    private fb = inject(FormBuilder);
    private authService = inject(AuthService);

    signupForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]]
    });

    onSubmit() {
        if (this.signupForm.valid) {
            this.authService.signup(this.signupForm.value).subscribe();
        }
    }
}
