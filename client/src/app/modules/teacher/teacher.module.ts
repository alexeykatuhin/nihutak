import { NgModule } from '@angular/core';
import { IconsProviderModule } from 'src/app/icons-provider.module';
import { registerLocaleData, CommonModule } from '@angular/common';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { teacherRoutingModule } from './teacher-routing.module';
import { TeacherComponent } from './teacher.component';
import { ReviewComponent } from './components/review/review.component';
import { ReviewCardComponent } from './components/review/components/review-card/review-card.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TeacherService } from './_services/teacher.service';


@NgModule({
    imports: [
        teacherRoutingModule,     
        NgZorroAntdModule,
        CommonModule,
        IconsProviderModule,
        FormsModule,
        ReactiveFormsModule 
    ],
    declarations: [
        TeacherComponent,
        ReviewComponent,
        ReviewCardComponent
    ],
    providers: [
        TeacherService
    ],
})
export class TeacherModule { }