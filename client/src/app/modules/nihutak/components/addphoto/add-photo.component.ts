import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PhotosService } from 'src/app/_services/photos.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({ templateUrl: 'add-photo.component.html' , styleUrls : ['add-photo.component.scss'] })
export class AddPhotoComponent implements OnInit  {
    loading = true;
    filter
    validateForm: FormGroup;
    validateCountryForm: FormGroup
    validateCityForm: FormGroup
    addModal = null
    id 
    initCountry
    countryCode

    constructor( private fb: FormBuilder, private photoService:PhotosService, private activatedRoute: ActivatedRoute
        ,private router: Router,){}
    ngOnInit(): void {
        this.id = this.activatedRoute.snapshot.queryParams.id
       
        this.photoService.getFilterData().subscribe((filt) => {
            this.initCountry = filt.countries[0]
            console.log(filt)
            this.filter = filt;
            this.validateForm = this.fb.group({
                url:[null, [Validators.required]],
                city:[2, [Validators.required]],
                datePicker:[null, [Validators.required]],
                desc:[null, [Validators.required]],
                tags:[null, [Validators.required]]                
            });

            this.validateCountryForm = this.fb.group({
                code: [null, [Validators.required]],
                name: [null, [Validators.required]],
            })

            this.validateCityForm = this.fb.group({
                country: [1, [Validators.required]],
                name: [null, [Validators.required]]
            })
            if (this.id){
                this.photoService.get(this.id).subscribe(x=>
                    {
                        this.validateForm.reset({url: x.url,
                             city: x.city.id, 
                             datePicker: x.date,
                            desc: x.description,
                            tags: x.tags.map(y=>y.name)})
                        console.log(x)
                        this.loading = false;
                    })
            }
            else
                this.loading = false;
        })
    }

    get f() { return this.validateForm.controls; }
    
    submitForm(): void {
        console.log(this.f)

        this.photoService.addPhoto(this.f.city.value,
             this.f.datePicker.value, this.f.url.value,
              this.f.desc.value,
            this.f.tags.value,
            this.id)
            .subscribe(()=>alert('succes'), (e)=> alert(e))

           // stop here if form is invalid
        //    if (this.validateForm.invalid) {
        //     return;
        }


        // this.loading = true;
        // this.authenticationService.register(this.f.email.value, this.f.password.value, this.f.username.value)
        //     .pipe(first())
        //     .subscribe(
        //         data => {
        //             alert(`Письмо для подтверждения вашего адреса выслано на ${data.email}`)
        //             this.router.navigate([this.returnUrl]);
        //         },
        //         error => {
        //             console.log(error)
        //             this.error = error;
        //             this.loading = false;
        //         });
    //   }

      handleCancel(){
        this.addModal = null
      }


      submitFormCountry(){
          this.photoService
          .addCountry(this.validateCountryForm.controls.code.value, this.validateCountryForm.controls.name.value)
          .subscribe(()=>{this.addModal = null; alert('succes')}, (e)=> alert(e))
        }
        countryCodeChanged(){
            this.countryCode = `flag-icon flag-icon-${this.validateCountryForm.controls.code.value}`
        }

        submitFormCity(){
            console.log(this.validateCityForm.controls)
            this.photoService
            .addCity(this.validateCityForm.controls.name.value, this.validateCityForm.controls.country.value)
            .subscribe(()=>{this.addModal = null; alert('succes')}, (e)=> alert(e))
        }
delete(){
    this.photoService
    .removePhoto(this.id)
    .subscribe(()=>{ alert('succes'); this.router.navigate(['./'],{
        relativeTo: this.activatedRoute})}, (e)=> alert(e))
}
        
    getClass(code){
        return 'flag-icon flag-icon-'+code;
    }
}