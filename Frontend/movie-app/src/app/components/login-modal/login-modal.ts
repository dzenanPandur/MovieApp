import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login-modal.html',
  styleUrls: ['./login-modal.css']
})
export class LoginModalComponent {
  @Input() isOpen: boolean = false;
  @Output() close = new EventEmitter<void>();
  @Output() login = new EventEmitter<{username: string, password: string}>();
  @Output() googleLogin = new EventEmitter<void>();
  @Output() githubLogin = new EventEmitter<void>();

  username: string = '';
  password: string = '';
  isLoading: boolean = false;

  onClose(): void {
    this.close.emit();
    this.resetForm();
  }

  onLogin(): void {
    if (this.username.trim() && this.password.trim() && !this.isLoading) {
      this.isLoading = true;
      this.login.emit({ username: this.username.trim(), password: this.password });
      this.isLoading = false;
      this.resetForm();
    }
  }

  onGoogleLogin(): void {
    if (!this.isLoading) {
      this.googleLogin.emit();
    }
  }

  onGithubLogin(): void {
    if (!this.isLoading) {
      this.githubLogin.emit();
    }
  }

  onBackdropClick(event: Event): void {
    if (event.target === event.currentTarget && !this.isLoading) {
      this.onClose();
    }
  }

  resetForm(): void {
    this.username = '';
    this.password = '';
    this.isLoading = false;
  }

  onSubmit(event: Event): void {
    event.preventDefault();
    this.onLogin();
  }
}
