import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services';
import { User } from './_models';
import { SettingService } from './_services/settings.service';
import { ErrorService } from './_services/error.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({ selector: 'app', templateUrl: 'app.component.html', styleUrls: ['app.component.scss']})
export class AppComponent  implements OnDestroy {
    currentUser: User;    
  isCollapsed = false;
  private ngUnsubscribe = new Subject();
  error;

    constructor(
        private errorService: ErrorService,
        private router: Router,
        private authenticationService: AuthenticationService,      public settingService: SettingService,
    ) {
        this.initializeErrors()
        this.authenticationService.currentUser.subscribe(x => {this.currentUser = x; 
            console.log(this.currentUser)});
            this.settingService.setupLanguage();
    }
    ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    logout() {
        this.authenticationService.logout();
        // this.router.navigate(['/login']);
    }
    public login(){   this.router.navigate(['/login']);}

    private initializeErrors()
    {
        this
            .errorService
            .getErrors()
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((errors) =>
            {
                if (errors.length>0)
                this.error = errors[errors.length-1]
            });
    } 

    afterClose(){
        this.error = null
    }
}