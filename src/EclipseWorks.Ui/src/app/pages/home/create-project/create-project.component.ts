import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ProjectService } from '../../../services/project.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule} from '@angular/material/input'
import { MatButtonModule } from '@angular/material/button';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-create-project',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './create-project.component.html',
  styleUrl: './create-project.component.scss'
})
export class CreateProjectComponent  implements OnDestroy {
  form: FormGroup;
  private destroy$ = new Subject<void>()

  constructor(
    private readonly dialogRef: MatDialogRef<CreateProjectComponent>,
    private readonly fb: FormBuilder,
    private readonly projectService: ProjectService
  )
  {
    this.form = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSubmit() {
    if (this.form.valid) {
      this.projectService.create(this.form.value).pipe(takeUntil(this.destroy$)).subscribe({
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
