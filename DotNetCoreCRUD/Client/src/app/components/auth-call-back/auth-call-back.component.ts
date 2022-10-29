
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../../auth.service";


@Component({
  selector: 'app-auth-call-back',
  templateUrl: './auth-call-back.component.html',
  styleUrls: ['./auth-call-back.component.css']
})
export class AuthCallbackComponent implements OnInit {
  error!: boolean;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  async ngOnInit() {
    await this.authService.completeAuthentication();
    this.router.navigate(["/home"]);     
  }
}