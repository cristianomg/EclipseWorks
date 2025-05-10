import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { NotificationEntity } from '../models/notification.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private readonly http: HttpClient
  ) { }


  private readonly baseUrl = environment.apiUrl + '/Notification'


  getAll() : Observable<NotificationEntity[]> {
    return this.http.get<NotificationEntity[]>(this.baseUrl);
  }

  markAsRead(id: Number) {
    return this.http.post(this.baseUrl + '/MarkAsRead/' + id, {});
  }
}
