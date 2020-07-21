import { Component, OnInit, HostListener } from '@angular/core';
import { first, map } from 'rxjs/operators';
import { PhotosService } from 'src/app/_services/photos.service';
import { combineLatest } from 'rxjs';
import { PhotoFilterDto } from 'src/app/_models/photo-filter-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/_services/authentication.service';


@Component({ templateUrl: 'photos.component.html' , styleUrls : ['photos.component.scss'] })
export class PhotosComponent implements OnInit {
    loading = true;
    loadingMore = false
    photos;
    page = 0;
    tags;
    cities;
    countries;
    years;
    filter = new PhotoFilterDto()
    selectedPhoto = null;
    isAdmin
    editMode
    
    constructor(private photoService: PhotosService,private activatedRoute: ActivatedRoute
        ,private router: Router,
        private authenticationService: AuthenticationService) {
            this.authenticationService.currentUser.subscribe(x => {this.isAdmin = x && x.isAdmin});
     }

    ngOnInit() {
        this.loading = true;

        this.photoService.getFilterData().subscribe((filt) => {
            this.tags = filt.tags;
            this.countries = filt.countries;
            this.years = filt.years;
            this.routeProcess()
            this.photoService.getPhotos(this.filter).subscribe((res) => {
                this.filter.AlreadyShownPhotos = res.alreadyShownPhotos;
                this.photos = res.photos;
                this.loading = false;
            })
        })
    }

    routeProcess(){
        if (this.activatedRoute.snapshot.queryParams.tags){
            this.filter.Tags = this.activatedRoute.snapshot.queryParams.tags.split(',').map(x=>Number.parseInt(x))
        }
        if (this.activatedRoute.snapshot.queryParams.countries){
            this.filter.Countries = this.activatedRoute.snapshot.queryParams.countries.split(',').map(x=>Number.parseInt(x))
        }
        if (this.activatedRoute.snapshot.queryParams.cities){
            this.filter.Cities = this.activatedRoute.snapshot.queryParams.cities.split(',').map(x=>Number.parseInt(x))
        }  
        if (this.activatedRoute.snapshot.queryParams.years){
            this.filter.Years = this.activatedRoute.snapshot.queryParams.years.split(',').map(x=>Number.parseInt(x))
        } 
        if (this.activatedRoute.snapshot.queryParams.order){
            this.filter.Order =Number.parseInt( this.activatedRoute.snapshot.queryParams.order.split(','))
        } 
    }

    getClass(code){
        return 'flag-icon flag-icon-'+code;
    }

    onScroll(){
        if(this.loadingMore)
            return;
        this.loadingMore = true;
        this.filter.Page++;
        this.photoService.getPhotos(this.filter).pipe(first()).subscribe(res => {
            this.loadingMore = false;
            this.filter.AlreadyShownPhotos = res.alreadyShownPhotos;
            this.photos = this.photos.concat(res.photos);
        });
    }

    onClick(id, type) {
        this.filter = new PhotoFilterDto();
        switch (type) {
            case "tag":
                this.filter.Tags = [id]
                break;
            case "city":
                this.filter.Cities = [id]
                break;
            case "country":
                this.filter.Countries = [id]
                break;
        }

        this.applyFilters();
    }
 

    applyFilters(){        
        this.loading = true;
        this.filter.Page = 0;
        this.filter.AlreadyShownPhotos = [];
        this.photoService.getPhotos(this.filter).pipe(first()).subscribe(res => {      
            this.photos = res.photos;
            this.filter.AlreadyShownPhotos = res.alreadyShownPhotos;
            this.loading = false;
        });
        console.log(this.filter.Tags.join(','))
        this.router.navigate(['./'],{
            relativeTo: this.activatedRoute,
            queryParams: {
            tags: this.filter.Tags.join(','),
            countries: this.filter.Countries.join(','),
            cities: this.filter.Cities.join(','),
            years: this.filter.Years.join(','),
            order: this.filter.Order
        }})
    }

    resetFilters(){
        this.loading = true;
        this.filter = new PhotoFilterDto();
        this.photoService.getPhotos(this.filter).pipe(first()).subscribe(res => {      
            this.photos = res.photos;
            this.loading = false;
        });
        this.router.navigate(['./'],{
            relativeTo: this.activatedRoute})
    }

    handleCancel(){
        this.selectedPhoto = null
    }

    @HostListener('window:keyup', ['$event'])
  keyEvent(event: KeyboardEvent) {
    // console.log(event);
      if (event.key == 'q' && event.ctrlKey)
        this.editMode = !this.editMode
    }

    edit(photo){
        this.router.navigate(['./addphoto'],{
            relativeTo: this.activatedRoute,
            queryParams: {
            id: photo.id
        }})
    }
}