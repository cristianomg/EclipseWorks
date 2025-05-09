import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {MatListModule} from '@angular/material/list'
import {MatSidenavModule} from '@angular/material/sidenav';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-report',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MatSidenavModule, MatListModule, RouterModule],
  templateUrl: './report.component.html',
  styleUrl: './report.component.scss'
})
export class ReportComponent {

  reports = [
    { url: 'completed', label: 'Tarefas completadas (últimos 30 dias)' },
    { url: 'delayed', label: 'Tarefas atrasadas por usuário' },
    { url: 'average-time', label: 'Tempo médio de conclusão' },
    { url: 'projects-delay', label: 'Projetos com maior taxa de atraso' }
  ];
}
