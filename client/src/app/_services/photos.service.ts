import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@environments/environment';
import { PhotoFilterDto } from '../_models/photo-filter-dto';
import { ConfigService } from 'ngx-envconfig';

@Injectable()
export class PhotosService
{
    apiUrl;
    constructor(private http: HttpClient, private configService: ConfigService){
        this.apiUrl = configService.get('HOST_API')
    }

    getPhotos(filter: PhotoFilterDto){        
        return this.http.post<any>(`${this.apiUrl}/photo/GetMany`, filter)
    }
    getFilterData(){
        return this.http.get<any>(`${this.apiUrl}/photo/GetFilterData`)
    }
}