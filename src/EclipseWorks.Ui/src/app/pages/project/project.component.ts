import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute,  Router,  RouterModule } from '@angular/router';
import { Task, TaskStatus } from '../../models/task.model';
import { TaskCardComponent } from "./components/task-card/task-card.component";
import { Project } from '../../models/project.model';
import { ProjectService } from '../../services/project.service';
import { TaskService } from '../../services/task.service';
import { CdkDragDrop, DragDropModule, transferArrayItem } from '@angular/cdk/drag-drop';
import { CreateTaskComponent } from './create-task/create-task.component';
import {MatSidenavModule} from '@angular/material/sidenav'
import { forkJoin, map, Observable, Subject, takeUntil } from 'rxjs';
import { TaskDrawerComponent } from './components/task-drawer/task-drawer.component';

@Component({
  selector: 'app-project',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatDividerModule, MatDialogModule, RouterModule, TaskCardComponent, DragDropModule, MatSidenavModule, TaskDrawerComponent],
  templateUrl: './project.component.html',
  styleUrl: './project.component.scss'
})
export class ProjectComponent implements OnInit, OnDestroy  {

  constructor(
    private readonly route: ActivatedRoute,
    private readonly projectService: ProjectService,
    private readonly taskService: TaskService,
    private readonly dialog: MatDialog,
    private readonly router: Router
  ) {}


  private destroy$ = new Subject<void>()

  projectId: Number | null = null
  project: Project | null = null;
  pendingTasks: Task[] = []
  inProgressTasks : Task[] = []
  completedTasks : Task[] = []

  PENDING_TASK_STATUS = TaskStatus.Pending
  IN_PROGRESS_TASK_STATUS = TaskStatus.InProgress
  COMPLETED_TASK_STATUS = TaskStatus.Completed

  selectedTask: any;
  isDrawerOpen = false;

  ngOnInit(): void {
  this.projectId = Number(this.route.snapshot.paramMap.get('id'));
  const taskId = Number(this.route.snapshot.queryParamMap.get('taskId'));
  
    if (this.projectId) {
      forkJoin({
        project: this.getProject(this.projectId),
        tasks: this.getTasks(this.projectId)
      }).pipe(takeUntil(this.destroy$)).subscribe(res => {
        if (taskId) {
          const allTasks = [
            ...this.pendingTasks,
            ...this.inProgressTasks,
            ...this.completedTasks
          ];
          const openedTask = allTasks.find(x=>x.id === Number(taskId));
          if (openedTask) {
            this.openDrawer(openedTask)
          }
        }
      })
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  getProject(projectId: Number) {
    return this.projectService.getProjectById(projectId).pipe(map(result => {
      this.project = result;
      return result;
    }));
  }

  getTasks(projectId: Number): Observable<Task[]>{
    return this.taskService.getTasksByProjectId(projectId).pipe(map(result=> {
      this.pendingTasks = result.filter(t=>t.status === this.PENDING_TASK_STATUS);
        this.inProgressTasks = result.filter(t=>t.status === this.IN_PROGRESS_TASK_STATUS);
        this.completedTasks = result.filter(t=>t.status === this.COMPLETED_TASK_STATUS);

        return result;
    }))
  }

  changeStatus(event: CdkDragDrop<Task[]>, newStatus: TaskStatus) {
    if (event.previousContainer === event.container) return;
  
    const task = event.previousContainer.data[event.previousIndex];
  
    transferArrayItem(
      event.previousContainer.data,
      event.container.data,
      event.previousIndex,
      event.currentIndex
    );
  
    task.status = newStatus;
    this.taskService.updateTask(task).pipe(takeUntil(this.destroy$)).subscribe();
  }

  createTask() {
    const dialogRef = this.dialog.open(CreateTaskComponent, {
      data: {
        projectId: this.projectId
      }
    })

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getTasks(this.projectId!).pipe(takeUntil(this.destroy$)).subscribe();
      }
    })
  }

  openDrawer(task: Task) {
    this.selectedTask = task;
    this.isDrawerOpen = true;
    this.router.navigate([], {
      queryParams: { taskId: task.id },
      queryParamsHandling: 'merge'
    });
  }
  
  closeDrawer() {
    this.isDrawerOpen = false;
    this.selectedTask = null;
    
    this.router.navigate([], {
      queryParams: { taskId: null },
      queryParamsHandling: 'merge'
    });
  }

  onDelete() {
    this.getTasks(this.projectId!).pipe(takeUntil(this.destroy$)).subscribe({
      next: res => {
        this.closeDrawer();
      }
    });
  }


}
