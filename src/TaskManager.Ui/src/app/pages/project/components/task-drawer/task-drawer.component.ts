import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { Task } from '../../../../models/task.model';
import { TaskPriorityChipComponent } from "../task-priority-chip/task-priority-chip.component";
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { TaskCommentService } from '../../../../services/task-comment.service';
import { map, Subject, takeUntil } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-task-drawer',
  standalone: true,
  imports: [CommonModule, MatSidenavModule, MatButtonModule, MatIconModule, MatExpansionModule, MatCardModule, TaskPriorityChipComponent, FormsModule, MatFormFieldModule, MatInputModule],
  templateUrl: './task-drawer.component.html',
  styleUrl: './task-drawer.component.scss'
})
export class TaskDrawerComponent implements OnInit, OnDestroy, OnChanges {
  private destroy$: Subject<void> = new Subject<void>();
  @Input({ required: true }) task!: Task
  @Input() opened: boolean = false;
  @Output() openedChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() onClose: EventEmitter<void> = new EventEmitter<void>();
  isAddingComment: boolean = false;
  newComment: string = '';

  constructor(
    private readonly taskCommentService: TaskCommentService,
    private readonly sanitizer: DomSanitizer
  ) { }

  ngOnInit(): void {
    this.getComments();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['task']) {
      this.getComments();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }


  getComments() {
    if (this.task?.id) {
      this.taskCommentService.getComments(this.task.id).pipe(takeUntil(this.destroy$)).subscribe({
        next: res => {
          this.task.comments = res;
        }
      })
    }


  }

  submitComment() {
    this.taskCommentService.createComment(this.task.id, this.newComment).pipe(takeUntil(this.destroy$)).subscribe({
      next: res => {
        this.isAddingComment = false;
        this.newComment = '';
        this.getComments();
      }
    })
  }

  cancelComment() {
    this.newComment = '';
    this.isAddingComment = false;
  }

  formatTextToHtmlWithLink(texto: string) {
    const httpPrefix = ['http://', 'https://']
    let textoFormatado = texto.split(' ').map((word, index) => {
      if (word.startsWith(httpPrefix[0]) || word.startsWith(httpPrefix[1])) {
        word = `<a href="${word}" target="_blank">${word}</a>`
      }
      return word
    }).join(" ")

    return this.sanitizer.bypassSecurityTrustHtml(textoFormatado);
  }
}
