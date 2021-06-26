import { Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from '../base/base.component';
import { LoginService } from './login.service';

declare let particlesJS: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [LoginService]
})
export class LoginComponent extends BaseComponent implements OnInit {
  @ViewChild("form") form: any;

  constructor(private readonly _loginService: LoginService) {
    super();
  }

  ngOnInit(): void {
    // initialize particle.js library with configuration
    particlesJS.load('particles-js', 'assets/particles.json', function () {
      console.log('callback - particles.js config loaded');
    });
  }

  login() {
    this._loginService.login(this.model).then(response => {
      this.response = response;
      console.log(this.response);
    });
  }

  logout() {
    this._loginService.logout(this.model).then(response => {
      this.response = response;
      console.log(this.response);
    });
  }
}
