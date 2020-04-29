import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/_services';


@Component({ templateUrl: 'register.component.html', styleUrls : ['register.component.scss'] })
export class RegisterComponent implements OnInit {
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    validateForm: FormGroup;
    
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private fb: FormBuilder
    ) { 
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) { 
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.validateForm = this.fb.group({
            username: [null, [Validators.required]],
            password: [null, [Validators.required]],
            email: [null, [Validators.email, Validators.required]],
            checkPassword: [null, [Validators.required, this.confirmationValidator]]
          });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() { return this.validateForm.controls; }

 
    public btnClick(){   this.router.navigate(['/register']);}

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
        this.authenticationService.register(this.f.email.value, this.f.password.value, this.f.username.value)
            .pipe(first())
            .subscribe(
                data => {
                    alert(`Письмо для подтверждения вашего адреса выслано на ${data.email}`)
                    this.router.navigate([this.returnUrl]);
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
}