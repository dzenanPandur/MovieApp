import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService, Toast } from '../../services/ToastService';

@Component({
  selector: 'app-toast-container',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './toast-container.html',
  styleUrls: ['./toast-container.css']
})
export class ToastContainerComponent {
  constructor(public toastService: ToastService) {}

  trackToast(index: number, toast: Toast): number {
    return toast.id;
  }

  getIcon(type: string): string {
    switch (type) {
      case 'success': return '✓';
      case 'error': return '✕';
      case 'warning': return '⚠';
      case 'info': return 'ℹ';
      default: return 'ℹ';
    }
  }
}
