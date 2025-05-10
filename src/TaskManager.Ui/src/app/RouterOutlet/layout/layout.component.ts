import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BreadcrumbComponent } from '../../components/breadcrumb/breadcrumb.component';

import { NotificationMenuComponent } from './components/notification-menu/notification-menu.component';
import { SettingsMenuComponent } from './components/settings-menu/settings-menu.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatToolbarModule, BreadcrumbComponent, NotificationMenuComponent, SettingsMenuComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {
}
