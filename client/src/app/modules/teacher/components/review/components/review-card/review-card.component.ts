import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { User } from 'src/app/_models';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherService } from 'src/app/modules/teacher/_services/teacher.service';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { AnswerDto } from 'src/app/modules/teacher/_models/answer-dto';


@Component({ templateUrl: 'review-card.component.html' , styleUrls : ['review-card.component.scss'] })
export class ReviewCardComponent implements OnInit {
    currentUser: User; 
    validateForm: FormGroup;
    id
    controlArray: Array<{ index: number; show: boolean }> = [];  
    answers: AnswerDto[] = [];
    editMode = false;
    loading = false;
   constructor(
    private authenticationService: AuthenticationService,  
    private route: ActivatedRoute,
    private teacherService: TeacherService,
    private router: Router,
    private fb: FormBuilder){
        
        this.id = Number.parseInt( this.route.snapshot.params["id"]);
        if (teacherService.getOptions().findIndex(x=> x == this.id) == -1){            
            this.router.navigate([`/teacher`]);
        }
    }

    ngOnInit(): void {
        this.validateForm = this.fb.group({});
        for (let i = 1; i < 21; i++) {
            this.answers.push(new AnswerDto(i, null, ""))
          this.validateForm.addControl(`field${i}`, new FormControl());
          }
        
        this.authenticationService.currentUser.subscribe(x => {this.currentUser = x; 
            // console.log(this.currentUser)
        });
    }

    submitForm(): void {
        // for (const i in this.validateForm.controls) {
        //   this.validateForm.controls[i].markAsDirty();
        //   this.validateForm.controls[i].updateValueAndValidity();
        // }

           // stop here if form is invalid
           if (this.validateForm.invalid) {
            return;
        }

        // this.answers[0].errorMessage = "error!!"
        // console.log(this.validateForm.controls);
        // this.validateForm.controls['field1'].setErrors({custom:true})
        // return

        console.log("!!")
        this.loading = true;
        if (this.editMode){
            this.teacherService.setAnswers(this.answers, this.id).subscribe(x=> {
                alert("Успешно!")

                this.loading = false
                this.cancel()
            })
        }
        else{
            this.teacherService.checkAnswers(this.answers, this.id).subscribe(x=> {

                console.log('!!')
                // this.answers[0].errorMessage = "error!!"
                // this.validateForm.controls['field1'].setErrors({custom:true})
                // this.loading = false;
        
        //        for (const i in this.validateForm.controls) {
        //   this.validateForm.controls[i].markAsDirty();
        //   this.validateForm.controls[i].updateValueAndValidity();
        // }
                // this.answers = x;

                for (let index = 0; index < 20; index++) {
                    if (x[index].errorMessage){
                        this.answers[index].errorMessage = x[index].errorMessage       
                        this.validateForm.controls[`field${index+1}`].markAsDirty()    
                        this.validateForm.controls[`field${index+1}`].markAsTouched()                 
                        this.validateForm.controls[`field${index+1}`].setErrors({custom:true})
                    }
                    
                }

                // this.answers.forEach(y=>{
                //     if (y.errorMessage){
                //         console.log(y.errorMessage)
                //         this.validateForm.controls[`field${y.id}`].setErrors({custom:true})
                //     }
                // })
                this.loading = false;
            })
        }

       
    }

    edit(){
        this.editMode = true;
        this.loading = true;
        this.teacherService.getAnswers(this.id).subscribe(x=>{this.answers = x; this.loading = false})
    }
    cancel(){
        this.editMode = false;
        this.answers=[]
        for (let i = 1; i < 21; i++) {
            this.validateForm.controls[`field${i}`].markAsUntouched()  
            this.answers.push(new AnswerDto(i, null, ""))       
                }
                
          
          }

          back(){
              
        this.router.navigate([`/teacher`]);
          }
    
    }
