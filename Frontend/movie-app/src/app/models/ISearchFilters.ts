export interface SearchFilters {
  query: string;
  minRating?: number;
  maxRating?: number;
  afterYear?: number;
  beforeYear?: number;
  olderThanYears?: number;
}