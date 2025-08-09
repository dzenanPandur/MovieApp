import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import authService from '../../services/AuthService';
import { ToastService } from '../../services/ToastService';

@Component({
  selector: 'app-auth-callback',
  standalone: true,
  template: `
    <div class="auth-callback-container">
      <div class="spinner"></div>
      <p>Processing authentication...</p>
    </div>
  `,
  styles: [`
    .auth-callback-container {
      display:flex; flex-direction:column; align-items:center; justify-content:center; height:100vh;
    }
    .spinner {
      width:40px; height:40px; border:4px solid #f3f3f3; border-top:4px solid #2563eb;
      border-radius:50%; animation:spin 1s linear infinite; margin-bottom:14px;
    }
    @keyframes spin { 0%{transform:rotate(0)} 100%{transform:rotate(360deg)} }
  `]
})
export class Oauth implements OnInit {
  constructor(
    private router: Router,
    private toastService: ToastService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {

    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const token = this.extractTokenFromLocation();

    if (token) {
      try {
        authService.handleOAuthCallback(token);
        this.toastService.success('Welcome!', 'Successfully logged in with OAuth');
      } catch (e) {
        this.toastService.error('Login Failed', 'Could not process OAuth token');
      } finally {
        queueMicrotask(() => this.router.navigate(['/']));
      }
    } else {
      this.toastService.error('Login Failed', 'OAuth authentication failed');
      this.router.navigate(['/']);
    }
  }

  private extractTokenFromLocation(): string | null {
    try {
      const loc = window.location;

      if (loc.hash?.startsWith('#')) {
        const params = new URLSearchParams(loc.hash.substring(1));
        const t = params.get('token');
        if (t) return t;
      }

      if (loc.search?.startsWith('?')) {
        const params = new URLSearchParams(loc.search.substring(1));
        const t = params.get('token');
        if (t) return t;
      }
    } catch {
      return null;
    }
    return null;
  }
}