import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Task } from '../models/task.model';
import { Observable } from 'rxjs';
import { CreateTask } from '../models/create-task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(
    private readonly http: HttpClient
  ) { }

  baseUrl = environment.apiUrl + "/task";

  getTasksByProjectId(projectId: Number): Observable<Task[]> {  
    return this.http.get<Task[]>(this.baseUrl + `/${projectId}`)
  }

  updateTask(task: Task) {
    const {status, description} = task;
    const body = {status, description}
    return this.http.patch(this.baseUrl + `/${task.id}`, body);
  }
  createTask(data: CreateTask) : Observable<any> {
    return this.http.post(this.baseUrl, data);
  }

  deleteTask(taskId: number) : Observable<any>{
    return this.http.delete(this.baseUrl + `/${taskId}`);
  }
}
