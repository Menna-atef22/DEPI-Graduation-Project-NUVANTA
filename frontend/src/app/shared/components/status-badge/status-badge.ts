import { Component, input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-status-badge',
  styleUrl: './status-badge.css',
  templateUrl: './status-badge.html',
  template: `
    <span class="px-2 py-1 rounded-full text-xs font-medium" [class]="color()">
      {{ status() }}
    </span>
  `,
})
export class StatusBadge {
  status = input.required<string>();

  private map: Record<string, string> = {
    Pending: 'bg-yellow-100 text-yellow-800',
    Approved: 'bg-green-100 text-green-800',
    Succeeded: 'bg-green-100 text-green-800',
    Paid: 'bg-green-100 text-green-800',
    Active: 'bg-green-100 text-green-800',
    'In Stock': 'bg-green-100 text-green-800',
    Rejected: 'bg-red-100 text-red-800',
    Failed: 'bg-red-100 text-red-800',
    Overdue: 'bg-red-100 text-red-800',
    Inactive: 'bg-gray-200 text-gray-700',
    'Low Stock': 'bg-orange-100 text-orange-800',
  };
  color(){
    return this.map[this.status()] ?? 'bg-gray-100 text-gray-700';
  }
}
