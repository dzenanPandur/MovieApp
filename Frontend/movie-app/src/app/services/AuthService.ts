import networkService from "./networkService";
import { BehaviorSubject } from 'rxjs';

export interface LoginCredentials {
  username: string;
  password: string;
}

export interface RegisterCredentials {
  username: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  user: {
    email: string;
    userName: string;
  };
}

export interface User {
  email: string;
  userName: string;
}

class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
  private readonly USER_KEY = 'auth_user';
  
  private currentUserSubject = new BehaviorSubject<User | null>(this.getUserFromStorage());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    if (this.isBrowser()) {
      this.loadUserFromToken();
    }
  }


  private isBrowser(): boolean {
    return typeof window !== 'undefined' && typeof localStorage !== 'undefined';
  }


  async login(credentials: LoginCredentials): Promise<AuthResponse> {
    try {
      const result = await networkService.post<LoginCredentials, AuthResponse>('/Auth/login', credentials);
      
      if (!result.isSuccessful || !result.data) {
        throw new Error('Login failed');
      }

      const authData = result.data;
      if ('token' in authData && 'user' in authData) {
        this.setAuthData(authData);
        return authData;
      } else {
        throw new Error('Invalid authentication response');
      }
    } catch (error: any) {
      console.error('Login error:', error);
      
      if (error.data && error.data.message) {
        throw new Error(error.data.message);
      } else if (error.status === 401) {
        throw new Error('Invalid username or password');
      } else {
        throw new Error('Login failed. Please try again.');
      }
    }
  }


  async register(credentials: RegisterCredentials): Promise<void> {
    try {
      const result = await networkService.post<RegisterCredentials, any>('/Auth/register', credentials);
      
      if (!result.isSuccessful) {
        throw new Error('Registration failed');
      }
    } catch (error: any) {
      console.error('Registration error:', error);
      
      if (error.data && error.data.errors) {
        const errorMessages = Object.values(error.data.errors).flat();
        throw new Error(errorMessages.join(', '));
      } else if (error.data && error.data.message) {
        throw new Error(error.data.message);
      } else {
        throw new Error('Registration failed. Please try again.');
      }
    }
  }


  loginWithGoogle(): void {
    if (!this.isBrowser()) return;
    
    const returnUrl = encodeURIComponent(window.location.origin + '/auth-callback');
    window.location.href = `https://localhost:7237/api/Auth/login-google?returnUrl=${returnUrl}`;
  }


  loginWithGitHub(): void {
    if (!this.isBrowser()) return;
    
    const returnUrl = encodeURIComponent(window.location.origin + '/auth-callback');
    window.location.href = `https://localhost:7237/api/Auth/login-github?returnUrl=${returnUrl}`;
  }


  handleOAuthCallback(token: string): void {
    if (!this.isBrowser()) return;
    
    if (token) {
      this.setToken(token);
      this.loadUserFromToken();
      
    
      window.history.replaceState({}, document.title, window.location.pathname);
    }
  }


  async logout(): Promise<void> {
    try {

      await networkService.post('/Auth/logout');
    } catch (error) {
      console.error('Logout error:', error);
    } finally {
      this.clearAuthData();
    }
  }


  getAccessToken(): string | null {
    if (!this.isBrowser()) return null;
    return localStorage.getItem(this.TOKEN_KEY);
  }


  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }


  isAuthenticated(): boolean {
    if (!this.isBrowser()) return false;
    
    const token = this.getAccessToken();
    if (!token) return false;


    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const currentTime = Date.now() / 1000;
      return payload.exp > currentTime;
    } catch {
      return false;
    }
  }


  private setAuthData(authData: AuthResponse): void {
    if (!this.isBrowser()) return;
    
    localStorage.setItem(this.TOKEN_KEY, authData.token);
    localStorage.setItem(this.USER_KEY, JSON.stringify(authData.user));
    this.currentUserSubject.next(authData.user);
  }


  private setToken(token: string): void {
    if (!this.isBrowser()) return;
    
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  private clearAuthData(): void {
    if (!this.isBrowser()) return;
    
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.USER_KEY);
    this.currentUserSubject.next(null);
  }

  private getUserFromStorage(): User | null {
    if (!this.isBrowser()) return null;
    
    try {
      const userJson = localStorage.getItem(this.USER_KEY);
      return userJson ? JSON.parse(userJson) : null;
    } catch (error) {
      console.error('Error getting user from storage:', error);
      return null;
    }
  }

  private loadUserFromToken(): void {
    if (!this.isBrowser()) return;
    
    const token = this.getAccessToken();
    if (!token) return;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      console.log('JWT Payload:', payload);
      
    
      const email = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] ||
                    payload['email'] ||
                    payload['Email'] ||
                    '';
      
      const userName = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] ||
                       payload['unique_name'] ||
                       payload['name'] ||
                       payload['UserName'] ||
                       payload['username'] ||
                       email;
      
      const user: User = {
        email: email,
        userName: userName
      };
      
      if (user.email || user.userName) {
        localStorage.setItem(this.USER_KEY, JSON.stringify(user));
        this.currentUserSubject.next(user);
      }
    } catch (error) {
      console.error('Error parsing token:', error);
      this.clearAuthData();
    }
  }


  async onGoogleLogin(): Promise<void> {
    try {

      if (this.isAuthenticated()) {
        await this.logout();
      }
      

      setTimeout(() => {
        this.loginWithGoogle();
      }, 100);
    } catch (error) {
      console.error('Error during Google login:', error);
      this.loginWithGoogle();
    }
  }


  async onGithubLogin(): Promise<void> {
    try {

      if (this.isAuthenticated()) {
        await this.logout();
      }
      

      setTimeout(() => {
        this.loginWithGitHub();
      }, 100);
    } catch (error) {
      console.error('Error during GitHub login:', error);
      this.loginWithGitHub();
    }
  }
}

const authService = new AuthService();
export default authService;