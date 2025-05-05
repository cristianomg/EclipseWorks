import { Component, Input } from '@angular/core';
import {MatChipsModule} from '@angular/material/chips';
import { TaskPriority } from '../../../../models/task.model';

@Component({
  selector: 'app-task-priority-chip',
  standalone: true,
  imports: [MatChipsModule],
  templateUrl: './task-priority-chip.component.html',
  styleUrl: './task-priority-chip.component.scss'
})
export class TaskPriorityChipComponent {
  @Input({required: true}) priority!: TaskPriority


  getLabel() {
    switch (this.priority) {
      case TaskPriority.Low: 
        return 'Baixa';
      case TaskPriority.Medium:
        return 'Média';
      case TaskPriority.High: 
        return 'Alta'
    }
  }
}
