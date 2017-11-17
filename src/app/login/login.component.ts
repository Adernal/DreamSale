import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { routerTransition } from '../router.animations';
import { NgForm } from '@angular/forms';
import { LoginService } from './login.service';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent implements OnInit {
    @ViewChild('f') loginForm: NgForm;
    Email:string;
    Password:string;
    Credentials;
    Token;
    constructor(public router: Router,private loginService: LoginService) {
    }

    ngOnInit() {
       // this.checkLogin();
    }

    onLoggedin() {
        localStorage.setItem('isLoggedin', 'true');
    }
    checkLogin(){
        this.Email = this.loginForm.value.Email;
        this.Password = this.loginForm.value.Password;
        console.log(this.Email);
        console.log(this.Password);
        this.Credentials={
            "Email":this.Email,
            "Password":this.Password,
            "RememberMe": true
          };
          this.loginService.checkLogin(this.Credentials)
          .subscribe(
          (data) => {
            console.log(data.json().success);
            if(data.json().success===false){
                alert("Failed !");
            }
            else
            {
                this.Token = data.json();
                console.log(this.Token);
                this.router.navigateByUrl("/dashboard");
            }
            
          },
          (error) => {
            alert('Failed !');
            console.log(error)
          }
          ); 
    }

}
