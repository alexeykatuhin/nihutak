import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './_helpers';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { ConfirmEmailComponent } from './components/auth/confirm-email/confirm-email.component';
import { ResetPasswordComponent } from './components/auth/reset-password/reset-password.component';
import { ResetConfirmComponent } from './components/auth/reset-confirm/reset-confirm.component';

const routes: Routes = [
    { path: '', redirectTo: 'teacher', pathMatch:'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'confirmemail', component: ConfirmEmailComponent },
    { path: 'resetpassword', component: ResetPasswordComponent },
    { path: 'resetconfirm', component: ResetConfirmComponent },
    { path: 'home', loadChildren: () => import('./modules/nihutak/nihutak.module').then(m=>m.NihutakModule)},
    { path: 'teacher', loadChildren: () => import('./modules/teacher/teacher.module').then(m=>m.TeacherModule)},
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);