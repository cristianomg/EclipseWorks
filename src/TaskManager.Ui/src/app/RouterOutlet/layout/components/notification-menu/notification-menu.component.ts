import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { TimeAgoPipe } from '../../../../pipes/time-ago.pipe';
import { CommonModule } from '@angular/common';
import { NotificationService } from '../../../../services/notification.service';
import { interval, Subject, takeUntil } from 'rxjs';
import { NotificationEntity } from '../../../../models/notification.model';
import { MatTabsModule } from '@angular/material/tabs';
import { Router } from '@angular/router';


@Component({
  selector: 'app-notification-menu',
  standalone: true,
  imports: [CommonModule, TimeAgoPipe, MatBadgeModule, MatDividerModule, MatMenuModule, MatButtonModule, MatIconModule, MatTabsModule],
  templateUrl: './notification-menu.component.html',
  styleUrl: './notification-menu.component.scss'
})
export class NotificationMenuComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  constructor(
    private readonly notificationService: NotificationService,
    private readonly router: Router
  ) { }

  notReadNotifications: NotificationEntity[] = []
  allNotifications: NotificationEntity[] = []

  ngOnInit(): void {
    this.getNotification();

    interval(5000)
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.getNotification();
      });
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }


  getNotification() {
    this.notificationService.getAll().pipe(takeUntil(this.destroy$)).subscribe({
      next: res => {
        this.notReadNotifications = res.filter(x => !x.read)
        this.allNotifications = res
      }
    })
  }

  onClick(notification: NotificationEntity, event: MouseEvent) {
    event.stopPropagation();
    if (!notification) {
      return;
    }

    if (!notification.read) {
      this.notificationService.markAsRead(notification.id).pipe(takeUntil(this.destroy$)).subscribe({
        next: res => {
          this.getNotification();
        }
      })
    }
    this.redirectNotification(notification);
  }

  redirectNotification(notification: NotificationEntity) {
    if (notification.redirectUrl) {
      window.open(notification.redirectUrl, '_blank');
    }
  }
}
