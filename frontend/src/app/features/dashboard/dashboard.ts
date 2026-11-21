import { Component, AfterViewInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements AfterViewInit, OnDestroy {
  @ViewChild('financeChart') financeChart!: ElementRef<HTMLCanvasElement>;
  chart?: Chart;

  stats = [
    { label: 'Total Budget', value: 'EGP 1,000,000', change: 'Annual company budget', icon: '◈', color: 'teal' },
    { label: 'Committed', value: 'EGP 250,000', change: '25% of total budget', icon: '▤', color: 'blue' },
    { label: 'Spent', value: 'EGP 300,000', change: '30% of total budget', icon: '↗', color: 'amber' },
    { label: 'Available', value: 'EGP 450,000', change: '45% remaining', icon: '✓', color: 'green' }
  ];

  transactions = [
    { title: 'Office Equipment', category: 'Operations', date: 'Nov 02, 2026', amount: '- EGP 24,000', status: 'Paid' },
    { title: 'Software Subscription', category: 'Engineering', date: 'Nov 01, 2026', amount: '- EGP 12,500', status: 'Pending' },
    { title: 'Marketing Campaign', category: 'Marketing', date: 'Oct 29, 2026', amount: '- EGP 35,000', status: 'Approved' },
    { title: 'Laptop Purchase', category: 'Engineering', date: 'Oct 27, 2026', amount: '- EGP 85,000', status: 'Pending' }
  ];

  approvals = [
    { id: 'PR-1001', title: 'MacBook Pro', person: 'Ahmed Ali', amount: 'EGP 85,000' },
    { id: 'PR-1003', title: 'Marketing Software', person: 'Omar Hassan', amount: 'EGP 12,500' },
    { id: 'PR-1005', title: 'Office Desks', person: 'Mona Samir', amount: 'EGP 18,000' }
  ];

  alerts = [
    { item: 'Printer Paper A4', quantity: '5 units left', level: 'Low Stock' },
    { item: 'Wireless Mouse', quantity: '0 units left', level: 'Out of Stock' },
    { item: 'USB-C Hubs', quantity: '3 units left', level: 'Low Stock' }
  ];

  ngAfterViewInit(): void {
    this.chart = new Chart(this.financeChart.nativeElement, {
      type: 'bar',
      data: {
        labels: ['Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov'],
        datasets: [
          {
            label: 'Budget',
            data: [150, 165, 180, 190, 200, 210],
            backgroundColor: '#cbd5e1',
            borderRadius: 6,
            barThickness: 15
          },
          {
            label: 'Spending',
            data: [90, 110, 125, 140, 155, 130],
            backgroundColor: '#0d9488',
            borderRadius: 6,
            barThickness: 15
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'bottom',
            labels: { usePointStyle: true, boxWidth: 8, padding: 20 }
          }
        },
        scales: {
          x: { grid: { display: false }, border: { display: false } },
          y: {
            beginAtZero: true,
            grid: { color: '#f1f5f9' },
            border: { display: false },
            ticks: { callback: value => `${value}K` }
          }
        }
      }
    });
  }

  get pendingApprovalsCount(): number {
  return this.approvals.length;
}

  ngOnDestroy(): void {
    this.chart?.destroy();
  }
}
