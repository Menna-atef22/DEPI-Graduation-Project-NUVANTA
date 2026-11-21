import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  imports: [RouterLink,RouterLinkActive],
  selector: 'app-sidebar',
  styleUrl: './sidebar.css',
  templateUrl: './sidebar.html',
})
export class Sidebar {
  menuItems = [
    { name: 'Dashboard', icon: '▦', route: '/dashboard' },
    { name: 'Budget', icon: '◈', route: '/budget' },
    { name: 'Purchase Requests', icon: '▤', route: '/purchase-requests' },
    { name: 'Approvals', icon: '✓', route: '/approvals' },
    { name: 'Suppliers', icon: '♧', route: '/suppliers' },
    { name: 'Inventory', icon: '▣', route: '/inventory' },
    { name: 'Invoices', icon: '▧', route: '/invoices' },
    { name: 'Payments', icon: '$', route: '/payments' },
    { name: 'Users', icon: '♙', route: '/users' },
    { name: 'Settings', icon: '⚙', route: '/settings' }
  ];
}
