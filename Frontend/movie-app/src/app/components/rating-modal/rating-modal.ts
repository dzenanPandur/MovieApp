import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StarRatingComponent } from '../star-rating/star-rating';

@Component({
  selector: 'app-rating-modal',
  standalone: true,
  imports: [CommonModule, StarRatingComponent],
  templateUrl: './rating-modal.html',
  styleUrls: ['./rating-modal.css']
})
export class RatingModalComponent implements OnChanges {
  @Input() isOpen: boolean = false;
  @Input() movieTitle: string = '';
  @Input() selectedRating: number = 0;
  @Input() isLoading: boolean = false;
  @Output() confirm = new EventEmitter<number>();
  @Output() cancel = new EventEmitter<void>();

  currentRating: number = 0;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedRating'] && changes['selectedRating'].currentValue) {
      this.currentRating = changes['selectedRating'].currentValue;
    }
    if (changes['isOpen'] && changes['isOpen'].currentValue) {
      this.currentRating = this.selectedRating;
    }
  }

  onRatingChange(rating: number): void {
    if (!this.isLoading) {
      this.currentRating = rating;
    }
  }

  onConfirm(): void {
    if (this.currentRating > 0 && !this.isLoading) {
      this.confirm.emit(this.currentRating);
    }
  }

  onCancel(): void {
    if (!this.isLoading) {
      this.cancel.emit();
      
    }
  }

  onBackdropClick(event: Event): void {
    if (event.target === event.currentTarget && !this.isLoading) {
      this.onCancel();
    }
  }
}
