import { Component, OnInit } from '@angular/core';
import {  ActivatedRoute, ActivatedRouteSnapshot, NavigationEnd, Route, Router, RouterModule } from '@angular/router';
import { filter } from 'rxjs';
import {MatIconModule} from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-breadcrumb',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule, RouterModule],
  templateUrl: './breadcrumb.component.html',
  styleUrl: './breadcrumb.component.scss'
})
export class BreadcrumbComponent implements OnInit {
  breadcrumbs: { label: string; url: string }[] = [];
  constructor(
    private router: Router, 
    private readonly route: ActivatedRoute
  ) {}
  
  ngOnInit(): void {
    this.updateBreadcrumbs(); 

    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.updateBreadcrumbs();
      });
  }
  
  private updateBreadcrumbs(): void {
    const pathSegments = this.router.url.split('/').filter(p => p);
    let accumulatedPath = '';
    this.breadcrumbs = pathSegments.map(segment => {
      accumulatedPath += `/${segment}`;
      return {
        label: this.formatLabel(segment),
        url: accumulatedPath
      };
    });
  }
  formatLabel(label: string): string {
    return label
      .replace(/-/g, ' ')
      .replace(/\b\w/g, c => c.toUpperCase());
  }
}
