import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authConfig: AuthConfig = {
    issuer: 'asxaxsasx', // IdentityServer 地址
    redirectUri: window.location.origin, // 登入後導回的網址
    postLogoutRedirectUri: window.location.origin, // 登出後導回的網址
    clientId: 'asxasx', // IdentityServer 設定的 Client ID
    scope: 'openid profile employee.profile', // 權限範圍，至少要包含 openid
    responseType: 'code', // 採用授權碼 (Code Flow)
    useSilentRefresh: true, // 啟用靜默刷新 Token
    silentRefreshRedirectUri: window.location.origin + '/silent-refresh.html', // 靜默刷新網址
    sessionChecksEnabled: true, // 啟用會話檢查
    showDebugInformation: true, // 顯示除錯資訊 (可關閉)
  };

  constructor(private oauthService: OAuthService) {}

  async initAuth(): Promise<void> {
    this.oauthService.configure(this.authConfig);
    await this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  login() {
    this.oauthService.initLoginFlow();
  }

  logout() {
    this.oauthService.logOut();
  }

  get userProfile() {
    return this.oauthService.getIdentityClaims();
  }

  get isAuthenticated() {
    return this.oauthService.hasValidAccessToken();
  }
}