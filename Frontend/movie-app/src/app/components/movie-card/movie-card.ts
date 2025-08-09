import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import Movie, { MediaType } from '../../models/IMovie';
import { StarRatingComponent } from '../star-rating/star-rating';
import ratingService from '../../services/RatingService';

@Component({
  selector: 'app-movie-card',
  standalone: true,
  imports: [CommonModule, StarRatingComponent],
  templateUrl: './movie-card.html',
  styleUrls: ['./movie-card.css']
})
export class MovieCardComponent implements OnInit {
  @Input() movie!: Movie;
  @Output() ratingChanged = new EventEmitter<{ movieId: number, rating: number }>();
  @Output() openDetails = new EventEmitter<number>();


  async ngOnInit() {
    
  }

  getMediaTypeText(type: MediaType): string {
    return type === MediaType.Movie ? 'Movie' : 'TV Show';
  }

  getAverageRating(): number {
    if (!this.movie.ratings || this.movie.ratings.length === 0) return 0;
    const sum = this.movie.ratings.reduce((acc, rating) => acc + rating.value, 0);
    return sum / this.movie.ratings.length;
  }

  getReviewCount(): number {
    return this.movie.ratings ? this.movie.ratings.length : 0;
  }

  get isAlreadyRated(): boolean {
    return ratingService.hasRated(this.movie.id);
  }

  onCardClick(): void {
    this.openDetails.emit(this.movie.id);
  }

  onRatingChange(rating: number): void {
    if (!this.isAlreadyRated) {
      this.ratingChanged.emit({ movieId: this.movie.id, rating });
    }
  }


  getCastDisplay(): string {
    if (!this.movie.cast || this.movie.cast.length === 0) return '';
    
    if (this.movie.cast.length <= 3) {
      return this.movie.cast.join(', ');
    } else {
      const displayCast = this.movie.cast.slice(0, 3).join(', ');
      const remainingCount = this.movie.cast.length - 3;
      return `${displayCast} and ${remainingCount} more`;
    }
  }
}