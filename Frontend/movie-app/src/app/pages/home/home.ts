import { Component, OnInit, NgZone, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import Movie, { MediaType } from '../../models/IMovie';
import movieService from '../../services/MovieService';
import authService, { LoginCredentials, User } from '../../services/AuthService';
import { SearchBarComponent } from '../../components/search-bar/search-bar';
import { MovieCardComponent } from '../../components/movie-card/movie-card';
import { RatingModalComponent } from '../../components/rating-modal/rating-modal';
import { LoginModalComponent } from '../../components/login-modal/login-modal';
import { ToastService } from '../../services/ToastService';
import ratingService from '../../services/RatingService';
import { MovieModalComponent } from '../../components/movie-modal/movie-modal';

enum LoadStatus {
  LOADING = 'loading',
  SUCCESS = 'success',
  ERROR = 'error'
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, SearchBarComponent, MovieCardComponent, RatingModalComponent, LoginModalComponent, MovieModalComponent],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  activeTab: string = 'movies';
  searchQuery: string = '';
  displayedItems: Movie[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 10;
  status: LoadStatus = LoadStatus.LOADING;

  allMovies: Movie[] = [];
  filteredItems: Movie[] = [];


  currentUser: User | null = null;
  private userSubscription: Subscription = new Subscription();


  showRatingModal: boolean = false;
  showLoginModal: boolean = false;
  selectedMovieForRating: Movie | null = null;
  selectedRating: number = 0;
  isSubmittingRating: boolean = false; 
  selectedMovieId: number | null = null;
  showMovieModal = false;

  constructor(
    private zone: NgZone, 
    private cdr: ChangeDetectorRef,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.userSubscription = authService.currentUser$.subscribe(user => {
      this.currentUser = user;
      this.cdr.markForCheck();
    });

    this.status = LoadStatus.LOADING;

    movieService.getMovies()
      .then(movies => {
        this.zone.run(() => {
          this.allMovies = movies;
          this.setActiveTab('movies')
          this.updateDisplayedItems();
          this.status = LoadStatus.SUCCESS;
          this.cdr.markForCheck();
        });
      })
      .catch(error => {
        this.zone.run(() => {
          console.error('Error loading movies:', error);
          this.status = LoadStatus.ERROR;
          this.toastService.error('Error', 'Failed to load movies. Please try again.');
          this.cdr.markForCheck();
        });
      });
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  get isLoading(): boolean {
    return this.status === LoadStatus.LOADING;
  }

  get hasError(): boolean {
    return this.status === LoadStatus.ERROR;
  }

  get isSuccess(): boolean {
    return this.status === LoadStatus.SUCCESS;
  }

  setActiveTab(tab: 'movies' | 'tvshows'): void {
    this.activeTab = tab;
    this.currentPage = 1;
    

    let items = tab === 'movies' 
      ? this.allMovies.filter(movie => movie.type === MediaType.Movie) 
      : this.allMovies.filter(movie => movie.type === MediaType.TvShow);


    if (this.searchQuery.trim().length >= 2) {
      items = this.searchMovies(items, this.searchQuery);
    }


    this.filteredItems = items.sort((a, b) => this.getAverageRating(b) - this.getAverageRating(a));
    this.updateDisplayedItems();
  }

  onSearchChange(query: string): void {
    this.searchQuery = query.trim();
    this.currentPage = 1;


    let baseItems = this.activeTab === 'movies'
      ? this.allMovies.filter(movie => movie.type === MediaType.Movie)
      : this.allMovies.filter(movie => movie.type === MediaType.TvShow);

    if (this.searchQuery.length >= 2) {

      this.filteredItems = this.searchMovies(baseItems, this.searchQuery);
    } else {

      this.filteredItems = baseItems;
    }


    this.filteredItems = this.filteredItems.sort((a, b) => this.getAverageRating(b) - this.getAverageRating(a));
    this.updateDisplayedItems();
  }

  onMovieRatingChanged(event: { movieId: number, rating: number }): void {
    const movie = this.allMovies.find(m => m.id === event.movieId);
    if (!movie) return;


    if (ratingService.hasRated(event.movieId)) {
      this.toastService.warning('Already Rated', 'You have already rated this movie in this session.');
      return;
    }


    this.selectedMovieForRating = movie;
    this.selectedRating = event.rating;
    this.showRatingModal = true;
    

    this.cdr.markForCheck();
  }

  async onConfirmRating(rating: number): Promise<void> {
    if (!this.selectedMovieForRating || this.isSubmittingRating) return;

    this.isSubmittingRating = true;
    this.cdr.markForCheck();


    try {
      await ratingService.addRating(this.selectedMovieForRating.id, rating);
      

      const updatedMovies = await movieService.getMovies();
      this.allMovies = updatedMovies;
      

      this.setActiveTab(this.activeTab as 'movies' | 'tvshows');
      
      this.toastService.success('Rating Submitted', 'Your rating has been successfully submitted!');
      
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : String(error);
      
      if (errorMessage.includes('already rated')) {
        this.toastService.warning('Already Rated', 'You have already rated this movie in this session.');
      } else {
        this.toastService.error('Error', 'Failed to submit rating. Please try again.');
        console.error('Failed to add rating:', error);
      }
    } finally {
      this.isSubmittingRating = false;
      this.closeRatingModal();
    }
  }

  onCancelRating(): void {
    if (!this.isSubmittingRating) {
      this.closeRatingModal();
    }
  }

  closeRatingModal(): void {
    this.showRatingModal = false;
    this.selectedMovieForRating = null;
    this.selectedRating = 0;
    this.isSubmittingRating = false;
    this.cdr.markForCheck();
  }

  retry(): void {
    this.ngOnInit();
  }

  private searchMovies(movies: Movie[], query: string): Movie[] {
    const searchTerm = query.toLowerCase();

    return movies.filter(movie => {

      if (this.isRatingQuery(searchTerm)) {
        return this.matchesRatingQuery(movie, searchTerm);
      }

      if (this.isYearQuery(searchTerm)) {
        return this.matchesYearQuery(movie, searchTerm);
      }


      return this.matchesTextSearch(movie, searchTerm);
    });
  }

  private isRatingQuery(query: string): boolean {
    return /(\d+)\s*stars?|at\s*least\s*(\d+)\s*stars?|below\s*(\d+)\s*stars?|less than\s*(\d+)\s*stars?/.test(query);
  }

  private matchesRatingQuery(movie: Movie, query: string): boolean {
    const avgRating = this.getAverageRating(movie);

    const exactMatch = query.match(/^(\d+)\s*stars?$/);
    if (exactMatch) {
      const targetRating = parseInt(exactMatch[1], 10);
      return Math.abs(avgRating - targetRating) < 0.25;
    }

    const minMatch = query.match(/at\s*least\s*(\d+)\s*stars?/);
    if (minMatch) {
      const minRating = parseInt(minMatch[1], 10);
      return avgRating >= minRating;
    }

    const belowMatch = query.match(/below\s*(\d+)\s*stars?/);
    if (belowMatch) {
      const belowRating = parseInt(belowMatch[1], 10);
      return avgRating < belowRating;
    }

    const lessThanMatch = query.match(/less than\s*(\d+)\s*stars?/);
    if (lessThanMatch) {
      const lessThanRating = parseInt(lessThanMatch[1], 10);
      return avgRating < lessThanRating;
    }

    return false;
  }

  private isYearQuery(query: string): boolean {
    return /after\s*(\d{4})|older\s*than\s*(\d+)\s*years?/.test(query);
  }

  private matchesYearQuery(movie: Movie, query: string): boolean {
    const movieYear = new Date(movie.releaseDate).getFullYear();
    const currentYear = new Date().getFullYear();


    const afterMatch = query.match(/after\s*(\d{4})/);
    if (afterMatch) {
      const targetYear = parseInt(afterMatch[1]);
      return movieYear > targetYear;
    }


    const olderMatch = query.match(/older\s*than\s*(\d+)\s*years?/);
    if (olderMatch) {
      const yearsOld = parseInt(olderMatch[1]);
      return (currentYear - movieYear) > yearsOld;
    }

    return false;
  }

  private matchesTextSearch(movie: Movie, query: string): boolean {
    const searchableText = [
      movie.title,
      movie.shortDescription,
      ...movie.cast
    ].join(' ').toLowerCase();

    return searchableText.includes(query);
  }

  private getAverageRating(movie: Movie): number {
    if (!movie.ratings || movie.ratings.length === 0) {
      
      return 0;
    }
    
    const sum = movie.ratings.reduce((acc, rating) => acc + rating.value, 0);
    const average = sum / movie.ratings.length;
    
    return average;
  }

  loadMore(): void {
    this.currentPage++;
    this.updateDisplayedItems();
  }

  hasMoreItems(): boolean {
    return this.displayedItems.length < this.filteredItems.length;
  }

  get isAllItemsLoaded(): boolean {
    return this.displayedItems.length === this.filteredItems.length && this.filteredItems.length > 0;
  }

  updateDisplayedItems(): void {
    const endIndex = this.currentPage * this.itemsPerPage;
    this.displayedItems = this.filteredItems.slice(0, endIndex);
  }


  get isAuthenticated(): boolean {
    return authService.isAuthenticated();
  }

  openLoginModal(): void {
    this.showLoginModal = true;
  }

  closeLoginModal(): void {
    this.showLoginModal = false;
  }

  openMovieModal(movieId: number) {
  this.selectedMovieId = movieId;
  this.showMovieModal = true;
}

closeMovieModal() {
  this.showMovieModal = false;
  this.selectedMovieId = null;
}

onMovieModalOpenLogin() {
  this.showMovieModal = false;
  this.openLoginModal();
}

  async onLogin(credentials: LoginCredentials): Promise<void> {
    try {
      await authService.login(credentials);
      this.toastService.success('Welcome!', `Successfully logged in as ${credentials.username}`, 3000);
      this.closeLoginModal();
    } catch (error: any) {
      this.toastService.error('Login Failed', error.message || 'Please check your credentials and try again.');
    }
  }

  onGoogleLogin(): void {
    authService.loginWithGoogle();
  }

  onGithubLogin(): void {
    authService.loginWithGitHub();
  }

  async onLogout(): Promise<void> {
    try {
      await authService.logout();
      this.toastService.success('Goodbye!', 'You have been logged out successfully.', 3000);
    } catch (error: any) {
      this.toastService.error('Logout Error', 'There was an issue logging out.');
    }
  }
}