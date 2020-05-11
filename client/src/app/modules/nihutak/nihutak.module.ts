import { NgModule } from '@angular/core';
import { nihutakRoutingModule } from './nihutak-routing.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
// import { NgZorroAntdModule } from 'ng-zorro-antd';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { IconsProviderModule } from 'src/app/icons-provider.module';
import { NihutakComponent } from './nihutak.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { registerLocaleData, CommonModule } from '@angular/common';
import { MultiTranslateHttpLoader } from 'ngx-translate-multi-http-loader';
import en from '@angular/common/locales/en';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { PhotosComponent } from './components/photos/photos.component';
import { RouterStateSnapshot } from '@angular/router';
import { AddPhotoComponent } from './components/addphoto/add-photo.component';

registerLocaleData(en);
export function HttpLoaderFactory(http: HttpClient) {
    return new MultiTranslateHttpLoader(http, [
        {prefix: "./assets/i18n/", suffix: ".json"}
    ]);
}

@NgModule({
    imports: [
        nihutakRoutingModule,
        InfiniteScrollModule,        
        NgZorroAntdModule,
        CommonModule,
        ReactiveFormsModule,
        FormsModule,
        IconsProviderModule,  
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient]
            }
        }), 
    ],
    declarations: [
        PhotosComponent,
        NihutakComponent,
        AddPhotoComponent
    ],
    providers: [
    ],
})
export class NihutakModule { }