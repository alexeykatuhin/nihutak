import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/_services';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({ 
    templateUrl: 'reset-password.component.html'
    // , styleUrls : ['login.component.scss']
 })
export class ResetPasswordComponent implements OnInit{
    loading = false;    
    validateForm: FormGroup;
    error = '';

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private fb: FormBuilder){}

    ngOnInit(): void {
        this.validateForm = this.fb.group({
            email: [null, [Validators.email, Validators.required]]
          });
    }
    get f() { return this.validateForm.controls; }
    submitForm(): void {
        for (const i in this.validateForm.controls) {
          this.validateForm.controls[i].markAsDirty();
          this.validateForm.controls[i].updateValueAndValidity();
        }

           // stop here if form is invalid
           if (this.validateForm.invalid) {
            return;
        }
console.log(this.f)
        this.loading = true;
        this.authenticationService.resetPassword(this.f.email.value)
            .pipe(first())
            .subscribe(
                data => {
                    alert(`Ссылка для сброса пароля отправлена на ${this.f.email.value}`)
                    this.router.navigate(['/']);
                },
                error => {
                    console.log(error)
                    this.error = error;
                    this.loading = false;
                });
      }
}