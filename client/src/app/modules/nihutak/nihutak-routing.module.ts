import { Routes, RouterModule } from '@angular/router';
import { NihutakComponent } from './nihutak.component';
import { PhotosComponent } from './components/photos/photos.component';
import { AddPhotoComponent } from './components/addphoto/add-photo.component';
const routes: Routes = [
    { path: '', component: NihutakComponent, children:[
        {path: '', component: PhotosComponent},
        {path: 'addphoto', component: AddPhotoComponent}
    ] }
];

export const nihutakRoutingModule = RouterModule.forChild(routes);