import { Component } from '@angular/core';
import { first } from 'rxjs/operators';
import { User } from '../../_models';
import { UserService } from '../../_services';
import { PhotosService } from 'src/app/_services/photos.service';


@Component({ templateUrl: 'photos.component.html' , styleUrls : ['photos.component.scss'] })
export class PhotosComponent {
    loading = true;
    photos;

    constructor(private photoService: PhotosService) { }

    ngOnInit() {
        this.loading = true;
        this.photoService.getPhotos().pipe(first()).subscribe(res => {
            this.loading = false;
            this.photos = res;
           console.log(res)
        });
    }
}