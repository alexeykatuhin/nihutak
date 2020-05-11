import { Component } from '@angular/core';
import { User } from 'src/app/_models';
import { Router } from '@angular/router';
import { TeacherService } from '../../_services/teacher.service';


@Component({ templateUrl: 'review.component.html' , styleUrls : ['review.component.scss'] })
export class ReviewComponent {
    currentUser: User;
    options: number[]
    curOpt
   constructor( private router: Router, private teacherService: TeacherService){
            this.options = teacherService.getOptions();
    }

    change(){
        console.log(this.curOpt)
        this.router.navigate([`/teacher/${this.curOpt}`]);
    }
}