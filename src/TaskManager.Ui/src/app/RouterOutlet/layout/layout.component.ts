import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import {MatToolbarModule} from '@angular/material/toolbar';
import { BreadcrumbComponent } from '../../components/breadcrumb/breadcrumb.component';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../../services/user.service';
import { UserRole } from '../../models/user.model';
import {MatMenuModule} from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatToolbarModule, BreadcrumbComponent, MatButtonModule, MatMenuModule, MatIconModule, RouterModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {

  constructor(private readonly router: Router,
    private readonly userService: UserService
  ) {
    
  }

  logout() {
    this.userService.clearUser();
    this.router.navigate(['/login']);
  }


  isManager() {
    return this.userService.hasRole(UserRole.Manager);
  }
}
