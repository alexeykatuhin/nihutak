import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({ 
    templateUrl: 'reset-confirm.component.html'
    // , styleUrls : ['login.component.scss']
 })
export class ResetConfirmComponent implements OnInit{
    loading = false;    
    validateForm: FormGroup;
    error = '';
    userId;
    code

    constructor(     private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private fb: FormBuilder){}

    ngOnInit(): void {

        this.userId = this.route.snapshot.queryParams['id'] ;
        this.code = this.route.snapshot.queryParams['code'] ;

        this.validateForm = this.fb.group({
            password: [null, [Validators.required]],
            checkPassword: [null, [Validators.required, this.confirmationValidator]]
          });
    }
    confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
        if (!control.value) {
          return { required: true };
        } else if (control.value !== this.validateForm.controls.password.value) {
          return { confirm: true, error: true };
        }
        var passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
        if (!control.value.match(passw))
        return { invalid: true}

        return {};
      };
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

        this.loading = true;
        this.authenticationService.changePassword(this.userId, this.f.password.value, this.code)
            .pipe(first())
            .subscribe(
                data => {
                    alert(`Пароль успешно сменен!`)
                    this.router.navigate(['/']);
                },
                error => {
                    console.log(error)
                    this.error = error;
                    this.loading = false;
                });
      }

      updateConfirmValidator(): void {
        /** wait for refresh value */
        Promise.resolve().then(() => this.validateForm.controls.checkPassword.updateValueAndValidity());
      }

}