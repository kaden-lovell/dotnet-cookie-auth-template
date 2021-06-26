import { Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from '../base/base.component';

declare let particlesJS: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {
  model: any;
  response: any;
  errors: any;
  view: any;

  @ViewChild("form") form: any;

  constructor() {
    super();
  }

  ngOnInit(): void {
    particlesJS.load('particles-js', 'assets/particles.json', function () {
      console.log('callback - particles.js config loaded');
    });
    this.view = 1;
  }

  login() { }
}
