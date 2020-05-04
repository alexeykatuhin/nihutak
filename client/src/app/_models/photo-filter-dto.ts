export class PhotoFilterDto{
    constructor(page = 0){
        this.Page = page;
        this.Tags = [];
        this.Countries = [];
        this.Cities = [];
    }
    Page: number;
    Tags: number[];
    Countries: number[];
    Cities: number[];
}