import { Component, Input } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
    selector: 'app-validation-message',
    standalone: true,
    template: `
        <small *ngIf="errorMessage">{{ errorMessage }}</small>
    `,
    imports: [NgIf],
    host: { 'class': 'p-error' }
})
export class ValidationMessageComponent {
    @Input() control!: AbstractControl | null;

    private errorMessages: { [key: string]: string } = {
        required: 'Zorunlu alan!',
        minlength: 'Girilen değer çok kısa!',
        maxlength: 'Girilen değer çok uzun!',
        pattern: 'Geçerli bir format giriniz!',
        email: 'Geçerli bir e-posta adresi giriniz!',
        passwordPolicy: 'Şifre en az 6 karakter olmalı, bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.',
        passwordsMismatch: 'Şifreler uyuşmuyor.'
    };

    get errorMessage(): string | null {
        if (this.control && this.control.errors && (this.control.dirty || this.control.touched)) {
            const errorKey = Object.keys(this.control.errors)[0];
            return this.errorMessages[errorKey] || 'Geçersiz değer!';
        }
        return null;
    }
}
