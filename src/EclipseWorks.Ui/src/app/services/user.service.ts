import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { User, UserRole } from '../models/user.model';
import { environment } from '../../environments/environment';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly cookieKey = 'app_user';
  private document = inject(DOCUMENT);

  constructor(private readonly httpClient: HttpClient, 
  ) { }

  getAll() {
    return this.httpClient.get<User[]>(`${environment.apiUrl}/user`)
  }

  setUser(user: User): void {
    const expires = new Date();
    expires.setDate(expires.getDate() + 7);

    const encoded = encodeURIComponent(JSON.stringify(user));
    this.document.cookie = `${this.cookieKey}=${encoded}; expires=${expires.toUTCString()}; path=/`;
  }

  getUser(): User | null {
    const cookies = this.document.cookie.split(';');
    for (const cookie of cookies) {
      const [name, value] = cookie.trim().split('=');
      if (name === this.cookieKey) {
        try {
          return JSON.parse(decodeURIComponent(value));
        } catch (e) {
          return null;
        }
      }
    }
    return null;
  }

  clearUser(): void {
    this.document.cookie = `${this.cookieKey}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
  }

  isLoggedIn(): boolean {
    return !!this.getUser();
  }

  hasRole(role: UserRole): boolean {
    const user = this.getUser();
    return user?.role === role;
  }
}
