import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { TaskComment } from '../models/task-comment.model';

@Injectable({
  providedIn: 'root'
})
export class TaskCommentService {

  constructor(
    private readonly http: HttpClient
  ) { }

  baseUrl = environment.apiUrl + '/TaskComment/'

  createComment(taskId: Number,comment: string): Observable<any> {
    return this.http.post(this.baseUrl + taskId, {comment: comment});
  }

  getComments(taskId: Number) : Observable<TaskComment[]>  {
    return this.http.get<TaskComment[]>(this.baseUrl + taskId);
  }
}
