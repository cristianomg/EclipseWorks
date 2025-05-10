import { Component } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../../../services/user.service';
import { UserRole } from '../../../../models/user.model';


@Component({
  selector: 'app-settings-menu',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, RouterModule, MatMenuModule],
  templateUrl: './settings-menu.component.html',
  styleUrl: './settings-menu.component.scss'
})
export class SettingsMenuComponent {

  constructor(private readonly router: Router,
    private readonly userService: UserService) {

  }

  logout() {
    this.userService.clearUser();
    this.router.navigate(['/login']);
  }

  isManager() {
    return this.userService.hasRole(UserRole.Manager);
  }
}
