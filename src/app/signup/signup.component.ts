import { Component, OnInit, ViewChild } from '@angular/core';
import { routerTransition } from '../router.animations';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { SignupService } from './signup.service';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss'],
    animations: [routerTransition()]
})
export class SignupComponent implements OnInit {
    @ViewChild('f') signupForm: NgForm;
    Email:string;
    Password:string;
    ConfirmPassword:string;
    Credentials;
    Token;
    constructor(private router:Router,private signupService : SignupService) { }

    ngOnInit() { }

    register(){
        this.Email =this.signupForm.value.Email;
        this.Password =this.signupForm.value.Password;
        this.ConfirmPassword =this.signupForm.value.ConfirmPassword;
        console.log(this.Email);
        console.log(this.Password);
        console.log(this.ConfirmPassword);
        this.Credentials={
            "Email":this.Email,
            "Password":this.Password,
            "ConfirmPassword":this.ConfirmPassword
        };
        this.signupService.checkSignup(this.Credentials)
        .subscribe(
        (data) => {
          console.log(data.json()["Success"]);
          if(data.json()["Success"]){
            this.Token = data.json()["Token"];
            console.log(this.Token);
            alert("Registration Succesful !")
            // localStorage.setItem('isLoggedin', 'true');
            this.router.navigateByUrl('/login');          }
          else
          {
              alert("Registration failed ! Try new email or re-type password !");
              this.signupForm.reset();
          }
          
        },
        (error) => {
          alert('Failed !');
          console.log(error)
        }
        ); 
    }
}
