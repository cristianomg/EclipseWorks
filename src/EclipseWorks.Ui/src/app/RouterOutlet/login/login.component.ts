import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import {MatSelectModule} from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import { MatCardModule } from '@angular/material/card'
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCardModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  
  selectedUsuario: number | null = null;
  users: User[] = [];

  constructor(private readonly userService: UserService,
    private readonly router: Router
  ) {

  }

  ngOnInit(): void {
    this.userService.getAll().subscribe({
      next: response => {
        this.users = response;
      },
      error: error => {
      }
    })
  }

  login() {
    const selectedUser = this.users.find(u => u.id === this.selectedUsuario);
    if (selectedUser) {
      this.userService.setUser(selectedUser);
      this.router.navigate(['/']); 
    }
  }


}
