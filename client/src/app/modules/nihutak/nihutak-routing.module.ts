import { Routes, RouterModule } from '@angular/router';
import { PhotosComponent } from './components/photos/photos.component';

const routes: Routes = [
    { path: '', component: PhotosComponent }
];

export const nihutakRoutingModule = RouterModule.forRoot(routes);