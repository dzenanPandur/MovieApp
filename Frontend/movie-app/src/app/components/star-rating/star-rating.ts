import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-star-rating',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './star-rating.html',
  styleUrls: ['./star-rating.css']
})
export class StarRatingComponent {
  @Input() rating: number = 0;
  @Input() readonly: boolean = true;
  @Input() showValue: boolean = true;
  @Input() reviewCount: number = 0;
  @Output() ratingChange = new EventEmitter<number>();

  stars: number[] = [1, 2, 3, 4, 5];
  hoverRating: number = 0;

  get filledStars(): number {
    return Math.floor(this.rating);
  }

  get hasHalfStar(): boolean {
    return this.rating % 1 >= 0.5;
  }

  get emptyStars(): number {
    return 5 - this.filledStars - (this.hasHalfStar ? 1 : 0);
  }

  rate(rating: number): void {
    if (!this.readonly) {
      this.ratingChange.emit(rating);
    }
  }

  onHover(rating: number): void {
    if (!this.readonly) {
      this.hoverRating = rating;
    }
  }

  onLeave(): void {
    this.hoverRating = 0;
  }

  getStarClass(index: number): string {
    const starNumber = index + 1;
    
    if (this.hoverRating > 0 && !this.readonly) {
      return starNumber <= this.hoverRating ? 'filled' : 'empty';
    }
    
    if (starNumber <= this.filledStars) {
      return 'filled';
    } else if (starNumber === this.filledStars + 1 && this.hasHalfStar) {
      return 'half';
    } else {
      return 'empty';
    }
  }
}