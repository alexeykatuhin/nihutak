import { SortOrder } from '../_enums/sort-order-enum';



export class PhotoFilterDto{
    constructor(page = 0){
        this.Page = page;
        this.Tags = [];
        this.Countries = [];
        this.Cities = [];
        this.Years = [];
        this.Order = SortOrder.Random;
        this.AlreadyShownPhotos = [];
    }
    Page: number;
    Tags: number[];
    Countries: number[];
    Cities: number[];
    Years: number[];
    Order: SortOrder;
    AlreadyShownPhotos: number[]
}