import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export const passwordPolicyValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const value = control.value;

  if (!value) return null;

  const hasUpperCase = /[A-Z]/.test(value);
  const hasLowerCase = /[a-z]/.test(value);
  const hasNumber = /[0-9]/.test(value);
  const hasSpecialChar = /[^A-Za-z0-9]/.test(value);
  const hasMinLength = value.length >= 6;

  const valid = hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar && hasMinLength;

  return valid ? null : { passwordPolicy: true };
};
