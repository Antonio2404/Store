import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { User } from '../models/user';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable()
export class AuthenticationService {
    constructor(
        private http: HttpClient,
        private route: ActivatedRoute,
        private router: Router) { }

    login(username: string, password: string): Observable<any> {
        return this.http.post<any>(`http://localhost:5000/api/user`, { Login: username, Password: password })
            .pipe(map(
                user => {
                    // login successful if there's a jwt token in the response
                    if (user && user.token) {
                        // store user details and jwt token in local storage to keep user logged in between page refreshes
                        localStorage.setItem('currentUser', JSON.stringify(user));
                    }
                    return user;
                }),
                catchError(error => {
                    console.log(error);
                    return throwError(error);
                }));
    }
    public isLoggedIn() {
        return localStorage.getItem('currentUser') !== null;

    }
    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        // get return url from route parameters or default to '/'
        let returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.router.navigate([returnUrl]);
    }
}