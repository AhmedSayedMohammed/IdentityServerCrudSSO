import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ILogin } from 'src/app/shared/classes/ilogin';
import { BehaviorSubject, finalize, tap } from 'rxjs';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [MessageService]
})
export class LoginComponent implements OnInit {
  image='';
  msgs: any[] = [];
  loading = new BehaviorSubject(false)
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required])
  loginform = new FormGroup({
    email: this.email,
    password: this.password
  })
  constructor( private router: Router, private messageService: MessageService,private authService:AuthService) {

   }

  ngOnInit(): void {

  }


  login(loginf:any) {
   
    
  }
}
