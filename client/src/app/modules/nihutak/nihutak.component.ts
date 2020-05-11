import { Component, OnDestroy } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';
import { User } from 'src/app/_models';
import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({ selector: 'nihutak', templateUrl: 'nihutak.component.html', styleUrls: ['nihutak.component.scss']})
export class NihutakComponent   {
    currentUser: User;    
  isCollapsed = false;
  error;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.authenticationService.currentUser.subscribe(x => {this.currentUser = x; 
            console.log(this.currentUser)});
    }

    logout() {
        this.authenticationService.logout();
        // this.router.navigate(['/login']);
    }
    public login(){  
        this.router.navigate(['/login'], { queryParams: { returnUrl: this.router.url } });
}


}