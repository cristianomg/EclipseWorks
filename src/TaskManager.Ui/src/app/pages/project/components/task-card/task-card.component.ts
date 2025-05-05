import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { Task } from '../../../../models/task.model';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../../../services/task.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-task-card',
  standalone: true,
  imports: [CommonModule ,MatCardModule, MatButtonModule, MatIconModule, MatDividerModule],
  templateUrl: './task-card.component.html',
  styleUrl: './task-card.component.scss'
})
export class TaskCardComponent implements OnDestroy {
  @Input({required: true}) task!: Task
  @Output() onDelete: EventEmitter<void> = new EventEmitter<void>();
  private destroy$ = new Subject<void>()

  constructor(
    private readonly taskService: TaskService
  ) {}

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  
  deleteTask(taskId: number) {
    this.taskService.deleteTask(taskId).pipe(takeUntil(this.destroy$)).subscribe({
      next: result => {
        this.onDelete.emit();
      }
    })
  }
}
