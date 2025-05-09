import { Routes } from '@angular/router';
import { LoginComponent } from './RouterOutlet/login/login.component';
import { authGuard } from './guards/auth.guard';
import { LayoutComponent } from './RouterOutlet/layout/layout.component';
import { managerGuard } from './guards/manager.guard';

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
                ]
              },
              {
                path: 'report',
                canActivate: [managerGuard],
                loadComponent: () => import('./pages/report/report.component').then(m => m.ReportComponent),
                children: [
                    {
                        path: '',
                        pathMatch: 'full',
                        redirectTo: 'completed'
                    },
                    {
                        path: 'completed',
                        loadComponent: () => import('./pages/report/reports/completed/completed.component').then(m => m.CompletedComponent),
                    },
                    {
                        path: 'delayed',
                        loadComponent: () => import('./pages/report/reports/delayed/delayed.component').then(m => m.DelayedComponent),
                    },
                    {
                        path: 'average-time',
                        loadComponent: () => import('./pages/report/reports/average-time/average-time.component').then(m => m.AverageTimeComponent),
                    },
                    {
                        path: 'projects-delay',
                        loadComponent: () => import('./pages/report/reports/projects-delay/projects-delay.component').then(m => m.ProjectsDelayComponent),
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
