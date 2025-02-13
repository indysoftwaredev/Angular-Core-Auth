import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegistrationRequest } from '../models/registration.model';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-registration',
  standalone: false,
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})

  //TODO: Route to login on successful registration
export class RegistrationComponent {
  registrationForm: FormGroup;
  errorMessage: string = '';
  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.registrationForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      this.errorMessage = '';
      const request: RegistrationRequest = this.registrationForm.value;
      this.authService.register(request).subscribe({
        next: (response) => {
          console.log('Registration successful', response);
        },
        error: (error) => {
          console.error('Registration failed', error);
          // Handle different error formats
          if (error.error && typeof error.error === 'object') {
            if (error.error.errors) {
              // Handle validation errors
              const errorMessages = [];
              for (const key in error.error.errors) {
                errorMessages.push(error.error.errors[key].join(' '));
              }
              this.errorMessage = errorMessages.join(' ');
            } else if (error.error.title) {
              // Handle problem details
              this.errorMessage = error.error.title;
            } else {
              // Handle other error objects
              this.errorMessage = Object.values(error.error).join(' ');
            }
          } else {
            this.errorMessage = error.message || "Registration failed.";
          }
        }
      });
     }
  }
}
