import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Project } from '../models/project.model';
import { UserService } from './user.service';
import { CreateProject } from '../models/create-project.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  constructor(
    private readonly http: HttpClient,
    private readonly userService: UserService
  ) { }

  baseUrl = `${environment.apiUrl}/project`

  getProjectById(projectId: Number) {
    return this.http.get<Project>(this.baseUrl + `/${projectId}`);
  }

  getMyProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.baseUrl);
  }

  delete(projectId: number): Observable<any> {
    return this.http.delete(this.baseUrl + `/${projectId}`);
  }

  create(body: CreateProject) : Observable<any> {
    return this.http.post(this.baseUrl, body);
  }
}
