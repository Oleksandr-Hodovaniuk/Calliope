import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';

@Service()
export class ApiService {
  private readonly http = inject(HttpClient);

  private readonly baseUrl = 'https://localhost:7167/api';

  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}`);
  }

  post<TRequest, TResponse>(
    endpoint: string,
    body: TRequest
  ): Observable<TResponse> {
    return this.http.post<TResponse>(
      `${this.baseUrl}/${endpoint}`,
      body
    );
  }

  put<TRequest, TResponse>(
    endpoint: string,
    body: TRequest
  ): Observable<TResponse> {
    return this.http.put<TResponse>(
      `${this.baseUrl}/${endpoint}`,
      body
    );
  }

  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${endpoint}`);
  }
}
