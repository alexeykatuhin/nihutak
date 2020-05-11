import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ConfigService } from 'ngx-envconfig';
import { AnswerDto } from '../_models/answer-dto';

@Injectable()
export class TeacherService
{
    apiUrl;
    options = [];
    constructor(private http: HttpClient, private configService: ConfigService){
        this.apiUrl = configService.get('HOST_API')
        for (let index = 1; index < 11; index++) {
            this.options.push(index)
            
        }
    }

    getOptions  = () :number[] => this.options;

    getAnswers(optionId:number){        
        let params = new HttpParams().set("id", optionId.toString())
        return this.http.get<AnswerDto[]>(`${this.apiUrl}/teacher/getanswers`, { params: params })
    }

    setAnswers(list: AnswerDto[], id: number){
        return this.http.post<any>(`${this.apiUrl}/teacher/setanswers`, {Answers: list, OptionId:id})
    }

    checkAnswers(list: AnswerDto[], id: number){
        return this.http.post<AnswerDto[]>(`${this.apiUrl}/teacher/checkanswers`, {Answers: list, OptionId:id})
    }
    // getPhotos(filter: PhotoFilterDto){        
    //     return this.http.post<any>(`${this.apiUrl}/photo/GetMany`, filter)
    // }
    // getFilterData(){
    //     return this.http.get<any>(`${this.apiUrl}/photo/GetFilterData`)
    // }
}