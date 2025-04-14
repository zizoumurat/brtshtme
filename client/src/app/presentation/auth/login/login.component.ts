import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AuthLogoWidget } from '@/presentation/auth/components/authlogowidget';
import { Router, RouterModule } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { IAuthService } from '@/core/services/admin/auth-service';
import { AUTH_SERVICE } from '@/core/services/admin/auth-token';
import { LoginRequest } from '@/core/models/admin/login-request';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, AuthLogoWidget, RouterModule, InputTextModule, FormsModule, ReactiveFormsModule, CheckboxModule, ButtonModule],
    templateUrl: './login.component.html',
})
export class LoginComponent {
    private router = inject(Router);
    private fb = inject(FormBuilder);
    private authService = inject<IAuthService>(AUTH_SERVICE);

    errorMessage: string | null = null;

    loginForm: FormGroup = this.fb.group({
        emailOrUserName: ['', [Validators.required]],
        password: ['', [Validators.required]],
    });

    ngOnInit() {
        if (this.authService.isAuthenticated()) {
            this.router.navigate(['']);
        }
    }

    async onSubmit(): Promise<void> {
        if (this.loginForm.invalid) {
            return;
        }

        const request: LoginRequest = this.loginForm.value;

        try {
            await this.authService.login(request);
        } catch (error) {
            this.errorMessage = error instanceof Error ? error.message : 'Login failed!';
        }
    }
}
