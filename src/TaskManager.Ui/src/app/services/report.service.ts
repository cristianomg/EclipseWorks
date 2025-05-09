import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { CompletedTasksCountByUserLast30Days, DelayedTasksByUsers } from '../models/reports.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(
    private readonly http: HttpClient
  ) { }

  private baseUrl = environment.apiUrl + '/Report'


  getDelayedTasksByUsers() : Observable<DelayedTasksByUsers> {
    return this.http.get<DelayedTasksByUsers>(this.baseUrl + '/DelayedTasksByUsers')
  }
  getCompletedTasksCountByUserLast30Days() : Observable<CompletedTasksCountByUserLast30Days>{
    return this.http.get<CompletedTasksCountByUserLast30Days>(this.baseUrl + '/CompletedTasksCountByUserLast30Days')
  }
}
