import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User, UserManager, UserManagerSettings } from "oidc-client";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class AuthService  {
  private _authNavStatusSource = new BehaviorSubject(false);
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private manager = new UserManager(getClientSettings());
  private user!: User | null;

  constructor(private http: HttpClient) {
 
    this.manager.getUser().then(user => {
      this.user = user;
      this._authNavStatusSource.next(this.isAuthenticated());
    });
  }

  login() {
    return this.manager.signinRedirect();
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this._authNavStatusSource.next(this.isAuthenticated());
  }
  signinSilentCallback(){
    this.manager.signinSilentCallback()
    .catch((err) => {
      console.log(err);
  });
  }

  isAuthenticated(): boolean {
    return (this.user != null && !this.user.expired);
  }

  get authorizationHeaderValue(): string {
    return `${this.user!.token_type} ${this.user!.access_token}`;
  }

  get name(): string {
    if (this.user !== null && this.user.profile !== undefined){
      return  this.user.profile.name!;
    } else {
      return "";
    }
  }

  async signout() {
    await this.manager.signoutRedirect();
  }

}
function getClientSettings(): UserManagerSettings {
  return {
    authority: ClientSettings.authority,
    client_id: ClientSettings.client_id,
    redirect_uri: ClientSettings.redirect_uri,
    post_logout_redirect_uri: ClientSettings.post_logout_redirect_uri,
    response_type: ClientSettings.response_type,
    scope: ClientSettings.scope,
    filterProtocolClaims: ClientSettings.filterProtocolClaims,
    loadUserInfo: ClientSettings.loadUserInfo,
    automaticSilentRenew: ClientSettings.automaticSilentRenew,
    silent_redirect_uri:ClientSettings.silent_redirect_uri
  };
}


export const ClientSettings = {
  authority: "http://localhost:5443",
  client_id: "interactive",
  redirect_uri: "http://localhost:4200/auth-callback",
  post_logout_redirect_uri: "http://localhost:5000/account/login/",
  response_type: "code",
  scope: "openid profile api",
  filterProtocolClaims: true,
  loadUserInfo: true,
  automaticSilentRenew: true,
  silent_redirect_uri: 'http://localhost:4200/silent-refresh'
}; 

