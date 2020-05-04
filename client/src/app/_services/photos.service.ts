import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@environments/environment';
import { PhotoFilterDto } from '../_models/photo-filter-dto';

@Injectable()
export class PhotosService
{
    apiUrl;
    constructor(private http: HttpClient){
    }

    getPhotos(filter: PhotoFilterDto){        
        return this.http.post<any>(`${environment.apiUrl}/photo/GetMany`, filter)
    }
    getFilterData(){
        return this.http.get<any>(`${environment.apiUrl}/photo/GetFilterData`)
    }
}