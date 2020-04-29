import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/_services';
import { ActivatedRoute } from '@angular/router';

@Component({ templateUrl: 'confirm-email.component.html' })
export class ConfirmEmailComponent implements OnInit {
    loading = true;

    constructor(
        private authenticationService: AuthenticationService,
        private route: ActivatedRoute,
   
    ) { 
        }
    

    ngOnInit() {

        let userId = this.route.snapshot.queryParams['id'] ;
        let code = this.route.snapshot.queryParams['code'] ;

        this.authenticationService.confirmEmail(userId, code)
        .subscribe(x=> this.loading = false);



     
    }

   

}