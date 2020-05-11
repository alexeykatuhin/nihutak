import { Routes, RouterModule } from '@angular/router';
import { TeacherComponent } from './teacher.component';
import { ReviewComponent } from './components/review/review.component';
import { ReviewCardComponent } from './components/review/components/review-card/review-card.component';

const routes: Routes = [
    { path: '', component: TeacherComponent, children:[
        {path: '', component: ReviewComponent},
        {path: ':id', component: ReviewCardComponent}
    ] }
];

export const teacherRoutingModule = RouterModule.forChild(routes);