import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { Project } from '../../../../models/project.model';
import { ProjectService } from '../../../../services/project.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ToastService } from '../../../../services/toast.service';

@Component({
  selector: 'app-project-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterModule],
  templateUrl: './project-card.component.html',
  styleUrl: './project-card.component.scss'
})
export class ProjectCardComponent implements OnDestroy {
  constructor(
    private readonly projectService: ProjectService,
    private readonly toastService: ToastService
  ) {

  }
  @Input({ required: true }) project!: Project
  @Output() onDelete: EventEmitter<void> = new EventEmitter<void>();
  private destroy$ = new Subject<void>()

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  deleteProject(id: number) {
    this.projectService.delete(id).pipe(takeUntil(this.destroy$)).subscribe({
      next: response => {
        this.onDelete.emit();
        this.toastService.showSuccess("Project deleted successfully.")

      },
      error: err => {

      }
    });
  }
}
