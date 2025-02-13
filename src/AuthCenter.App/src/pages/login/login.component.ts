import { Component, AfterViewInit, AfterViewChecked, NgModule } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError, map, throwError } from 'rxjs';
import { Messages } from '../../models/messages.model';
import { NotifyMessageComponent } from '../../components/notify-message.component';

declare const $: any;

@Component({
  selector: 'login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class LoginComponent implements AfterViewInit {

  loginModel: LoginModel = new LoginModel();

  constructor(private httpClient: HttpClient) { }

  ngAfterViewInit() {
  }

  login() {
    this.httpClient.post(`./connect/token`, {
      client_id: 'accounting-system',
      grant_type: 'password',
      username: this.loginModel.account,
      password: this.loginModel.password,
      scope: 'openid profile api1'
    }).subscribe(response => {
      NotifyMessageComponent.popupBy(response);
    });
  }
}