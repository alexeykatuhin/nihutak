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

    addCountry(code: string, name:string){     
        return this.http.post<any>(`${this.apiUrl}/photo/AddCountry`, {Code: code, Name: name})
    }

    addCity(name: string, id:string){     
        return this.http.post<any>(`${this.apiUrl}/photo/AddCity`, {Id: id, Name: name})
    }

    addPhoto(ciytId, date, url, desc, tags, id: number){
        var localTags = []
        tags.forEach(element => {
                localTags.push({name :element })
        });

        var params ={
            Id: Number(id),
            Url: url,
            Description: desc,
            Tags : localTags,
            Date: date,
            City: {
                Id: ciytId
            }
        }

        var uri = `${this.apiUrl}/photo/`
        if (id)
            uri+='updatephoto'
        else    
            uri+='addphoto'
        return this.http.post<any>(uri, params)
    }

    removePhoto(id){   
        let params = new HttpParams().set("id", id);  

        return this.http.delete<any>(`${this.apiUrl}/photo/delete`, { params: params })
    }

    get(id){
        
        let params = new HttpParams().set("id", id);  

        return this.http.get<any>(`${this.apiUrl}/photo/get`, { params: params })
    }
}