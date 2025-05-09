import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { CompletedTasksCountByUserLast30Days } from '../models/reports.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(
    private readonly http: HttpClient
  ) { }

  private baseUrl = environment.apiUrl + '/Report'


  getCompletedTasksCountByUserLast30Days() : Observable<CompletedTasksCountByUserLast30Days>{
    return this.http.get<CompletedTasksCountByUserLast30Days>(this.baseUrl + '/CompletedTasksCountByUserLast30Days')

  }
}
