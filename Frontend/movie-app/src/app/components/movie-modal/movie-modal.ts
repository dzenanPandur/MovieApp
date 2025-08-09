import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  NgZone,
  OnInit,
  OnChanges,
  SimpleChanges
} from '@angular/core';
import { CommonModule } from '@angular/common';
import movieService from '../../services/MovieService';
import authService from '../../services/AuthService';
import Movie from '../../models/IMovie';

@Component({
  selector: 'app-movie-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './movie-modal.html',
  styleUrls: ['./movie-modal.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MovieModalComponent implements OnInit, OnChanges {
  @Input() isOpen = false;
  @Input() movieId: number | null = null;

  @Output() closeModal = new EventEmitter<void>();
  @Output() openLogin = new EventEmitter<void>();

  movie: Movie | null = null;
  isLoading = false;
  errorMessage: string | null = null;

  private loadToken = 0;
  private initialized = false;

  constructor(private cdr: ChangeDetectorRef, private zone: NgZone) {}


  ngOnInit(): void {
    this.initialized = true;
    if (this.isOpen && this.movieId != null) {
      this.loadMovie();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.initialized) return;

    const openedNow = !!changes['isOpen']?.currentValue && !changes['isOpen']?.previousValue;
    const movieChanged = changes['movieId'] && !changes['movieId'].firstChange;

    if ((openedNow || movieChanged) && this.isOpen && this.movieId != null) {
      this.loadMovie();
    }

    if (changes['isOpen'] && !this.isOpen) {
      this.reset();
    }
  }

  private reset() {
    this.movie = null;
    this.errorMessage = null;
    this.isLoading = false;
    this.cdr.markForCheck();
  }

  private loadMovie() {
    const token = ++this.loadToken;


    if (!authService.isAuthenticated()) {
      this.zone.run(() => {
        if (token !== this.loadToken) return;
        this.isLoading = false;
        this.movie = null;
        this.errorMessage = 'You need to be logged in to view this';
        this.cdr.markForCheck();
      });
      return;
    }


    this.zone.run(() => {
      if (token !== this.loadToken) return;
      this.isLoading = true;
      this.movie = null;
      this.errorMessage = null;
      this.cdr.markForCheck();
    });

    movieService.getMovie(this.movieId!)
      .then(data => {
        this.zone.run(() => {
          if (token !== this.loadToken || !this.isOpen) return;
          this.movie = data;
          this.isLoading = false;
          this.errorMessage = null;
          this.cdr.markForCheck();
        });
      })
      .catch(err => {
        this.zone.run(() => {
          if (token !== this.loadToken) return;
          this.movie = null;
          this.isLoading = false;
            this.errorMessage = err?.status === 401
              ? 'You need to be logged in to view this'
              : 'Failed to load movie details.';
          this.cdr.markForCheck();
        });
      });
  }

  onLoginClick() {
    this.closeModal.emit();
    this.openLogin.emit();
  }
}
