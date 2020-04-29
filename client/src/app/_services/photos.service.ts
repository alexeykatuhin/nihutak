import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable()
export class PhotosService
{
    apiUrl;
    constructor(private http: HttpClient){
    }

    getPhotos(){
        return this.http.get<any>(`${environment.apiUrl}/photo/GetMany`)
    }
}