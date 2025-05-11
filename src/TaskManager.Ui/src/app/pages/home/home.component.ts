import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { Project } from '../../models/project.model';
import { ProjectService } from '../../services/project.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateProjectComponent } from './create-project/create-project.component';
import { RouterModule } from '@angular/router';
import { ProjectCardComponent } from "./components/project-card/project-card.component";
import { Subject, takeUntil } from 'rxjs';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatDividerModule, MatDialogModule, RouterModule, ProjectCardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit, OnDestroy {
  projects: Project[] = [];
  private destroy$ = new Subject<void>()

  constructor(
    private readonly projectService: ProjectService,
    private readonly dialog: MatDialog,
    private readonly toastService: ToastService
  ) { }

  ngOnInit(): void {
    this.getProjects();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  getProjects() {
    this.projectService.getMyProjects().pipe(takeUntil(this.destroy$)).subscribe({
      next: response => {
        this.projects = response
      },
      error: err => {

      }
    })
  }

  createProject() {
    const dialogRef = this.dialog.open(CreateProjectComponent, {
    })

    dialogRef.afterClosed().pipe(takeUntil(this.destroy$)).subscribe(result => {
      if (result) {
        this.getProjects();
        this.toastService.showSuccess("Project created successfully.")

      }
    })
  }

  onDelete() {
    this.getProjects();
  }
}
