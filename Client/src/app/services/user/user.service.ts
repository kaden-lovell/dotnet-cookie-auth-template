import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './user';

// shared
import { HttpService } from "../http/http.service";

@Injectable({
    providedIn: 'root'
})

export class UserService {
    user: User;

    constructor(private readonly _httpService: HttpService, private readonly _router: Router) { }
    loadUser() {
        const url = `/api/user/activeuser`;
        this._httpService.get(url).then(activeUser => {
            try {
                const user: User = {
                    id: activeUser.id,
                    username: activeUser.username,
                    role: activeUser.role
                }

                this.user = user;
            }
            catch (e) {
                this.user = undefined;
                this._router.navigateByUrl("https://localhost:4200/login");
            }
        });
    }
}