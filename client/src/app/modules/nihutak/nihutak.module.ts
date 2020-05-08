import { NgModule } from '@angular/core';
import { PhotosComponent } from './components/photos/photos.component';
import { nihutakRoutingModule } from './nihutak-routing.module';
import { AppModule } from 'src/app/app.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

@NgModule({
    imports: [
        nihutakRoutingModule,
        InfiniteScrollModule
    ],
    declarations: [
        PhotosComponent
    ],
    providers: [
    ],
})
export class NihutakModule { }