import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const passwordMatchValidator: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    const password = group.get('password')?.value;
    const rePassword = group.get('rePassword')?.value;
  
    return password === rePassword ? null : { passwordsMismatch: true };
  };