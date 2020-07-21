import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthService } from 'angularx-social-login';
import { FacebookLoginProvider, GoogleLoginProvider } from 'angularx-social-login';
import { AuthenticationService } from 'src/app/_services/authentication.service';

declare var VK: any;

@Component({ templateUrl: 'login.component.html', styleUrls : ['login.component.scss'] })
export class LoginComponent implements OnInit {
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    validateForm: FormGroup;
    authProviders
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private fb: FormBuilder,
        private authService: AuthService
    ) { 
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) { 
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
            this.validateForm = this.fb.group({
                username: [null, [Validators.required]],
                password: [null, [Validators.required]]
              });
    
            // get return url from route parameters or default to '/'
            this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

        this.authService.authState.subscribe((user) => {
            console.log(user)
            if (user)
                this.authenticationService.externalLogin(user.name, user.email)
                    .subscribe(x => { this.router.navigate([this.returnUrl]) })
        
                }
                , (err) => console.log('err'));

     
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
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigateByUrl(this.returnUrl);
                },
                error => {
                    console.log(error)
                    this.error = error;
                    this.loading = false;
                });
      }

      externalAuth(platform: string){
        this.loading = true;
        switch (platform){
            case "Google":
                let p  = GoogleLoginProvider.PROVIDER_ID;
                this.authService.signIn(p)
                this.loading = false;
                break;
            case "Vk":
                VK.Auth.login((res)=> {
                    if (res && res.status == "connected" && res.session && res.session.user){
                        this.authenticationService.externalLogin(`${res.session.user.first_name} ${res.session.user.last_name}`)
                        .subscribe(x => {this.loading = false; this.router.navigate([this.returnUrl]);  })
                    }})
                break;

        }
        
        return

      
    }
    resetPassword (){
        console.log("!!!")
        this.router.navigate(['/resetpassword']);}

}