import { Component } from '@angular/core';
import { UserService } from './services/user/user.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Client';

  constructor(private readonly _userService: UserService) {}

  ngOnInit() {
    this._userService.loadUser().then((response) => {
      this._userService.user = response;
    });
  }
}
