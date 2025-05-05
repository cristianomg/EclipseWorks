import { Routes } from '@angular/router';
import { LoginComponent } from './RouterOutlet/login/login.component';
import { authGuard } from './guards/auth.guard';
import { LayoutComponent } from './RouterOutlet/layout/layout.component';

export const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        canActivate: [authGuard],
        children: [
            {
                path: 'home',
                loadComponent: () => import('./pages/home/home.component').then(m => m.HomeComponent)
              },
              {
                path: 'project/:id',
                loadComponent: () => import('./pages/project/project.component').then(m => m.ProjectComponent),
                children: [
                    {
                        path: 'task',
                        loadComponent: () => import('./pages/project/project.component').then(m => m.ProjectComponent),
                    },
                    {
                        path: 'abc',
                        loadComponent: () => import('./pages/project/project.component').then(m => m.ProjectComponent),
                    }
                ]
              },
              {
                path: '',
                pathMatch: 'full',
                redirectTo: 'home'
              }
        ]
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'unauthorized',
        loadComponent: () => import('./RouterOutlet/unauthorized/unauthorized.component').then(m => m.UnauthorizedComponent)
    },
    {
        path: '**',
        redirectTo: '/home',
    }
];
