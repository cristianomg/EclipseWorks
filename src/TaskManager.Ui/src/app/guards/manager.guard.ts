import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { User, UserRole } from '../models/user.model';
import { UserService } from '../services/user.service';

export const managerGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService)

  const isAuthenticated = userService.isLoggedIn();

  if (isAuthenticated) {
    if (userService.hasRole(UserRole.Manager)){
      return true;
    }
  }
  router.navigate(['/unauthorized']); 
  return false;
};
