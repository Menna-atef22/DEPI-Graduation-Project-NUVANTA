import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css'
})
export class MainLayout {

  open = signal(false);

  links = [
    { label: 'Dashboard', to: '/dashboard' },
    { label: 'Budget', to: '/budget' },
    { label: 'Purchase Requests', to: '/purchase-requests' },
    { label: 'Approvals', to: '/approvals' },
    { label: 'Suppliers', to: '/suppliers' },
    { label: 'Inventory', to: '/inventory' },
    { label: 'Invoices', to: '/invoices' },
    { label: 'Payments', to: '/payments' },
    { label: 'Users', to: '/users' },
    { label: 'Settings', to: '/settings' }
  ];
}
