import { Component } from '@angular/core';
import { first, map } from 'rxjs/operators';
import { User } from '../../_models';
import { UserService } from '../../_services';
import { PhotosService } from 'src/app/_services/photos.service';
import { combineLatest } from 'rxjs';
import { PhotoFilterDto } from 'src/app/_models/photo-filter-dto';
import { ActivatedRoute, Router } from '@angular/router';


@Component({ templateUrl: 'photos.component.html' , styleUrls : ['photos.component.scss'] })
export class PhotosComponent {
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
    constructor(private photoService: PhotosService,private activatedRoute: ActivatedRoute
        ,private router: Router) {
        
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
            this.photos = this.photos.concat(res.photos);
        });
    }

    applyFilters(){        
        this.loading = true;
        this.filter.Page = 0;
        this.photoService.getPhotos(this.filter).pipe(first()).subscribe(res => {      
            this.photos = res.photos;
            this.loading = false;
        });
        console.log(this.filter.Tags.join(','))
        this.router.navigate(['/'],{
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
        this.router.navigate(['/'],{
            relativeTo: this.activatedRoute})
    }

    handleCancel(){
        this.selectedPhoto = null
    }
}