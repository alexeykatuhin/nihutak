import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { User, AuthProviderDto } from '../_models';
import { AuthService } from 'angularx-social-login';
import { ConfigService } from 'ngx-envconfig';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    apiurl;
    constructor(private http: HttpClient, private authService: AuthService,private configService: ConfigService) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
        this.apiurl = environment.apiUrl;
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`${this.apiurl}/account/Login`, { Email: username, Password: password })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));

                this.currentUserSubject.next(user);
                return user;
            }));
    }

    externalLogin(name: string, email = null) {
        return this.http.post<any>(`${this.apiurl}/account/ExternalLogin`, { userName: name, email: email })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));

                this.currentUserSubject.next(user);
                return user;
            }));

    }

    register(Email: string, Password: string, UserName: string) {
        return this.http.post<any>(`${this.apiurl}/account/RegisterWithConfirm`, { Password: Password, Email: Email, UserName: UserName })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    confirmEmail(userId: string, code: string) {
        let params = new HttpParams().set("userId", userId).set("code", code);
        return this.http.get<any>(`${this.apiurl}/account/ConfirmEmail`, { params: params })
    }

    resetPassword(email: string) {
        return this.http.post<any>(`${this.apiurl}/account/ResetPassword`, { Email: email })
    }

    changePassword(userId: string, password: string, code:string){
        return this.http.post<any>(`${this.apiurl}/account/changePassword`,
         { UserId: userId, Password: password, Code:code })
         .pipe(map(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUserSubject.next(user);
            return user;
        }));

    }
    logout() {
        this.authService.signOut();
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

}