import { Component ,input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-page-header',
  styleUrl: './page-header.css',
  templateUrl: './page-header.html',
  template: `
    <div class="flex items-center justify-between mb-4">
      <h2 class="text-xl font-semibold text-slate-800">{{ title() }}</h2>
      <ng-content />
    </div>
  `,
})
export class PageHeader {
  title = input.required<string>();
}
