import { Component, Inject, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ProjectService } from '../../../services/project.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule} from '@angular/material/input'
import { MatButtonModule } from '@angular/material/button';
import { TaskPriority } from '../../../models/task.model';
import { MatSelectModule } from '@angular/material/select';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { TaskService } from '../../../services/task.service';
import { CreateTask } from '../../../models/create-task.model';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-create-task',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatSelectModule, MatDatepickerModule],
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.scss'
})
export class CreateTaskComponent  implements OnDestroy {
 private destroy$ = new Subject<void>()
 form: FormGroup;
 minDate: Date;
 priorityEnum = TaskPriority;
 priorityLabels: Record<keyof typeof TaskPriority, string> = {
  Low: 'Baixa',
  Medium: 'Média',
  High: 'Alta'
};

priorities = Object.keys(TaskPriority)
  .filter(key => isNaN(Number(key)))
  .map(key => ({
    label: this.priorityLabels[key as keyof typeof TaskPriority],
    value: TaskPriority[key as keyof typeof TaskPriority]
  }));

  constructor(
    private readonly dialogRef: MatDialogRef<CreateTaskComponent>,
    private readonly fb: FormBuilder,
    private readonly taskService: TaskService,
    @Inject(MAT_DIALOG_DATA) public readonly data: {projectId: Number}
  )
  {

    if (!this.data?.projectId) {
      this.close();
    }
    this.minDate = new Date();
    this.minDate.setDate(this.minDate.getDate() + 1);

    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      priority: [null, Validators.required],
      dueDate: [null, Validators.required]
    });
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSubmit() {
    if (this.form.valid) {
      const data = {...this.form.value, projectId: this.data.projectId} as unknown as CreateTask
      this.taskService.createTask(data).pipe(takeUntil(this.destroy$)).subscribe({
        next: result => {
          this.dialogRef.close(true);
        },
        error: err => {
        }
      })
    }
  }

  close() {
    this.dialogRef.close(false);
  }
}
