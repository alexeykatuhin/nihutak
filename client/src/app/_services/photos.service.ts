import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@environments/environment';

@Injectable()
export class PhotosService
{
    apiUrl;
    constructor(private http: HttpClient){
    }

    getPhotos(page = 0){
        let params = new HttpParams().set("page",page.toString());
        return this.http.get<any>(`${environment.apiUrl}/photo/GetMany`, {params: params})
    }
}