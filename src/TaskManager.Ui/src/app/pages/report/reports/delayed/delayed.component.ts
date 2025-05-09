import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { map, Observable, Subject, takeUntil } from 'rxjs';
import { ReportService } from '../../../../services/report.service';
import { DelayedTasksByUsers } from '../../../../models/reports.model';
import * as d3 from 'd3';


@Component({
  selector: 'app-delayed',
  standalone: true,
  imports: [MatCardModule],
  templateUrl: './delayed.component.html',
  styleUrl: './delayed.component.scss'
})
export class DelayedComponent {
private destroy$ = new Subject<void>();

  report: DelayedTasksByUsers | null = null

  constructor(
    private readonly reportService: ReportService
  ) {}


  ngOnInit(): void {
    this.getReport();
  }
  
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }


  ngAfterViewInit(): void {
    this.getReport().pipe(takeUntil(this.destroy$)).subscribe({
      next: res => {
        this.renderChart();
      }
    })
  }

  private getReport() : Observable<any> {
    return this.reportService.getDelayedTasksByUsers().pipe(map(res=> {
      this.report = res;
    }));
  }
  private renderChart(): void {
    const data = this.report?.delayedTasksByUsers ?? [];

    const width = 600;
    const height = 600;
    const margin = { top: 20, right: 30, bottom: 40, left: 100 };

    const svg = d3.select('#barChart')
      .append('svg')
      .attr('width', width)
      .attr('height', height);

    const x = d3.scaleLinear()
      .domain([0, d3.max(data, d => d.count)!])
      .range([margin.left, width - margin.right]);

    const y = d3.scaleBand()
      .domain(data.map(d => d.userName))
      .range([margin.top, height - margin.bottom])
      .padding(0.3);

    svg.append('g')
      .selectAll('rect')
      .data(data)
      .enter()
      .append('rect')
      .attr('x', x(0))
      .attr('y', d => y(d.userName)!)
      .attr('width', d => x(d.count) - x(0))
      .attr('height', y.bandwidth())
      .attr('fill', '#1976d2');

    // Eixos
    svg.append('g')
      .attr('transform', `translate(0,${margin.top})`)
      .call(d3.axisTop(x).ticks(5));

    svg.append('g')
      .attr('transform', `translate(${margin.left},0)`)
      .call(d3.axisLeft(y));
  }
}
