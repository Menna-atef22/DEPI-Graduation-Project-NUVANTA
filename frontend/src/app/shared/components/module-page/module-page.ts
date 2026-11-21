
import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

interface Column {
  key: string;
  label: string;
}

interface Field {
  key: string;
  label: string;
  type?: string;
  options?: string[];
}

interface Metric {
  label: string;
  value: string;
  note: string;
}

interface Allocation {
  department: string;
  amount: number | null;
}

@Component({
  selector: 'app-module-page',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './module-page.html',
  styleUrl: './module-page.css'
})
export class ModulePage implements OnDestroy {

  module = '';
  title = '';
  subtitle = '';

  columns: Column[] = [];
  fields: Field[] = [];
  metrics: Metric[] = [];
  rows: any[] = [];

  search = '';
  filter = 'All';
  roleFilter = 'All';

  showForm = false;
  selected: any = null;
  formData: any = {};

  settings = {
    fullName: 'Demo User',
    email: 'nourhan@nuvanta.com',
    jobTitle: 'Finance Manager',
    companyName: 'NUVANTA',
    currency: 'EGP',
    timeZone: 'Africa/Cairo',
    emailNotifications: true,
    approvalReminders: true,
    lowStockAlerts: true
  };

  budgetPeriod = '2026';
  budgetTotalAmount: number | null = 1000000;

  allocations: Allocation[] = [
    { department: '', amount: null }
  ];

  private routeSubscription: Subscription;

  private data: Record<string, any> = {

    budget: {
      title: 'Budget Management',
      subtitle: 'Manage your company budget and department allocations.',

      metrics: [],

      columns: [
        { key: 'department', label: 'Department' },
        { key: 'allocated', label: 'Allocated Budget' },
        { key: 'spent', label: 'Spent' },
        { key: 'progress', label: 'Utilization' },
        { key: 'status', label: 'Status' }
      ],

      fields: [],

      rows: [
        {
          department: 'Engineering',
          allocated: 250000,
          spent: 120000,
          progress: '48%',
          status: 'On Track'
        },
        {
          department: 'Marketing',
          allocated: 180000,
          spent: 145000,
          progress: '81%',
          status: 'Warning'
        },
        {
          department: 'Operations',
          allocated: 170000,
          spent: 90000,
          progress: '53%',
          status: 'On Track'
        },
        {
          department: 'Human Resources',
          allocated: 100000,
          spent: 45000,
          progress: '45%',
          status: 'On Track'
        }
      ]
    },

    'purchase-requests': {
      title: 'Purchase Requests',
      subtitle: 'Create, track and manage purchase requests.',

      metrics: [
        { label: 'Total Requests', value: '128', note: 'All requests' },
        { label: 'Pending', value: '18', note: 'Awaiting review' },
        { label: 'Approved', value: '94', note: 'Successfully approved' },
        { label: 'Rejected', value: '16', note: 'Not approved' }
      ],

      columns: [
        { key: 'id', label: 'Request ID' },
        { key: 'requester', label: 'Requester' },
        { key: 'department', label: 'Department' },
        { key: 'date', label: 'Date' },
        { key: 'amount', label: 'Amount' },
        { key: 'status', label: 'Status' }
      ],

      fields: [
        { key: 'item', label: 'Item Name' },
        { key: 'quantity', label: 'Quantity', type: 'number' },
        { key: 'unitPrice', label: 'Estimated Unit Price', type: 'number' },
        {
          key: 'department',
          label: 'Department',
          options: [
            'Engineering',
            'Marketing',
            'Operations',
            'Human Resources',
            'Finance'
          ]
        },
        { key: 'justification', label: 'Justification' }
      ],

      rows: [
        {
          id: 'PR-1001',
          item: 'MacBook Pro',
          requester: 'Ahmed Ali',
          department: 'Engineering',
          date: 'Nov 01, 2026',
          amount: 85000,
          quantity: 1,
          unitPrice: 85000,
          justification: 'Developer equipment',
          status: 'Pending'
        },
        {
          id: 'PR-1002',
          item: 'Office Chairs',
          requester: 'Mona Samir',
          department: 'Operations',
          date: 'Nov 01, 2026',
          amount: 24000,
          quantity: 6,
          unitPrice: 4000,
          justification: 'Replace damaged chairs',
          status: 'Approved'
        },
        {
          id: 'PR-1003',
          item: 'Marketing Software',
          requester: 'Omar Hassan',
          department: 'Marketing',
          date: 'Oct 30, 2026',
          amount: 12500,
          quantity: 1,
          unitPrice: 12500,
          justification: 'Campaign management',
          status: 'Pending'
        },
        {
          id: 'PR-1004',
          item: 'Printer Supplies',
          requester: 'Sara Adel',
          department: 'Operations',
          date: 'Oct 29, 2026',
          amount: 4500,
          quantity: 3,
          unitPrice: 1500,
          justification: 'Office printing supplies',
          status: 'Rejected'
        }
      ]
    },

    approvals: {
      title: 'Approvals',
      subtitle: 'Review requests and manage approval decisions.',

      metrics: [],

      columns: [
        { key: 'id', label: 'Request ID' },
        { key: 'item', label: 'Request' },
        { key: 'requester', label: 'Requester' },
        { key: 'amount', label: 'Amount' },
        { key: 'department', label: 'Department' },
        { key: 'submittedDate', label: 'Submitted Date' },
        { key: 'status', label: 'Status' }
      ],

      fields: [],

      rows: [
        {
          id: 'PR-1001',
          item: 'MacBook Pro',
          requester: 'Ahmed Ali',
          department: 'Engineering',
          amount: 85000,
          submittedDate: 'Nov 01, 2026',
          status: 'Pending'
        },
        {
          id: 'PR-1003',
          item: 'Marketing Software',
          requester: 'Omar Hassan',
          department: 'Marketing',
          amount: 12500,
          submittedDate: 'Oct 30, 2026',
          status: 'Pending'
        },
        {
          id: 'PR-1005',
          item: 'Office Desks',
          requester: 'Mona Samir',
          department: 'Operations',
          amount: 18000,
          submittedDate: 'Oct 28, 2026',
          status: 'Approved'
        }
      ]
    },

    suppliers: {
      title: 'Suppliers',
      subtitle: 'Manage supplier information and business relationships.',

      metrics: [
        { label: 'Total Suppliers', value: '48', note: 'Registered suppliers' },
        { label: 'Active', value: '42', note: 'Currently active' },
        { label: 'Inactive', value: '6', note: 'Not active' }
      ],

      columns: [
        { key: 'name', label: 'Supplier Name' },
        { key: 'contactPerson', label: 'Contact Person' },
        { key: 'email', label: 'Email' },
        { key: 'phone', label: 'Phone' },
        { key: 'status', label: 'Status' }
      ],

      fields: [
        { key: 'name', label: 'Supplier Name' },
        { key: 'contactPerson', label: 'Contact Person' },
        { key: 'email', label: 'Email', type: 'email' },
        { key: 'phone', label: 'Phone' }
      ],

      rows: [
        {
          name: 'Tech Solutions',
          contactPerson: 'Karim Hassan',
          email: 'info@techsolutions.com',
          phone: '+20 100 123 4567',
          status: 'Active'
        },
        {
          name: 'Office Plus',
          contactPerson: 'Mariam Adel',
          email: 'sales@officeplus.com',
          phone: '+20 111 234 5678',
          status: 'Active'
        },
        {
          name: 'Global Logistics',
          contactPerson: 'Omar Nabil',
          email: 'hello@global.com',
          phone: '+20 122 345 6789',
          status: 'Inactive'
        }
      ]
    },

    inventory: {
      title: 'Inventory',
      subtitle: 'Monitor stock levels and inventory movements.',

      metrics: [],

      columns: [
        { key: 'item', label: 'Item' },
        { key: 'sku', label: 'SKU' },
        { key: 'quantity', label: 'Quantity' },
        { key: 'unit', label: 'Unit' },
        { key: 'warehouse', label: 'Warehouse' },
        { key: 'status', label: 'Stock Status' }
      ],

      fields: [
        { key: 'item', label: 'Item Name' },
        { key: 'sku', label: 'SKU' },
        { key: 'quantity', label: 'Quantity', type: 'number' },
        { key: 'unit', label: 'Unit' },
        { key: 'warehouse', label: 'Warehouse' }
      ],

      rows: [
        {
          item: 'Laptop Dell Latitude',
          sku: 'EL-1001',
          quantity: 24,
          unit: 'Pieces',
          warehouse: 'Main Warehouse',
          status: 'In Stock'
        },
        {
          item: 'Printer Paper A4',
          sku: 'OF-2001',
          quantity: 5,
          unit: 'Packs',
          warehouse: 'Main Warehouse',
          status: 'Low Stock'
        },
        {
          item: 'Wireless Mouse',
          sku: 'EL-1002',
          quantity: 0,
          unit: 'Pieces',
          warehouse: 'IT Store',
          status: 'Out of Stock'
        },
        {
          item: 'Office Chair',
          sku: 'FN-3001',
          quantity: 32,
          unit: 'Pieces',
          warehouse: 'Furniture Store',
          status: 'In Stock'
        }
      ]
    },

    invoices: {
      title: 'Invoices',
      subtitle: 'Track supplier invoices and payment status.',

      metrics: [
        { label: 'Total Invoices', value: '86', note: 'All invoices' },
        { label: 'Paid', value: '61', note: 'Completed payments' },
        { label: 'Pending', value: '19', note: 'Awaiting payment' },
        { label: 'Overdue', value: '6', note: 'Past due date' }
      ],

      columns: [
        { key: 'id', label: 'Invoice Number' },
        { key: 'supplier', label: 'Supplier' },
        { key: 'amount', label: 'Amount' },
        { key: 'due', label: 'Due Date' },
        { key: 'status', label: 'Status' }
      ],

      fields: [],

      rows: [
        {
          id: 'INV-2001',
          supplier: 'Tech Solutions',
          date: 'Oct 20, 2026',
          due: 'Nov 10, 2026',
          amount: 'EGP 45,000',
          status: 'Paid'
        },
        {
          id: 'INV-2002',
          supplier: 'Office Plus',
          date: 'Oct 25, 2026',
          due: 'Nov 05, 2026',
          amount: 'EGP 12,000',
          status: 'Pending'
        },
        {
          id: 'INV-2003',
          supplier: 'Global Logistics',
          date: 'Oct 10, 2026',
          due: 'Oct 25, 2026',
          amount: 'EGP 32,000',
          status: 'Overdue'
        }
      ]
    },

    payments: {
      title: 'Payments',
      subtitle: 'Review payment records. No real money transfers are made in this demo.',

      metrics: [
        { label: 'Total Payments', value: 'EGP 340K', note: 'Recorded payments' },
        { label: 'Completed', value: '42', note: 'Recorded as paid' },
        { label: 'Pending', value: '8', note: 'Awaiting processing' }
      ],

      columns: [
        { key: 'id', label: 'Payment Reference' },
        { key: 'supplier', label: 'Supplier' },
        { key: 'amount', label: 'Amount' },
        { key: 'date', label: 'Date' },
        { key: 'status', label: 'Status' }
      ],

      fields: [],

      rows: [
        {
          id: 'PAY-3001',
          supplier: 'Tech Solutions',
          date: 'Oct 28, 2026',
          method: 'Bank Transfer',
          amount: 'EGP 45,000',
          status: 'Succeeded'
        },
        {
          id: 'PAY-3002',
          supplier: 'Office Plus',
          date: 'Nov 01, 2026',
          method: 'Cash',
          amount: 'EGP 12,000',
          status: 'Pending'
        },
        {
          id: 'PAY-3003',
          supplier: 'Global Logistics',
          date: 'Oct 25, 2026',
          method: 'Bank Transfer',
          amount: 'EGP 32,000',
          status: 'Failed'
        }
      ]
    },

    users: {
      title: 'User Management',
      subtitle: 'Manage team members and their access roles.',

      metrics: [
        { label: 'Total Users', value: '32', note: 'Registered users' },
        { label: 'Active Users', value: '29', note: 'Currently active' },
        { label: 'Inactive Users', value: '3', note: 'Disabled accounts' }
      ],

      columns: [
        { key: 'name', label: 'Full Name' },
        { key: 'email', label: 'Email' },
        { key: 'role', label: 'Role' },
        { key: 'department', label: 'Department' },
        { key: 'status', label: 'Status' }
      ],

      fields: [
        { key: 'name', label: 'Full Name' },
        { key: 'email', label: 'Email', type: 'email' },
        {
          key: 'role',
          label: 'Role',
          options: ['Admin', 'Manager', 'Requester', 'Accountant', 'Auditor']
        },
        {
          key: 'department',
          label: 'Department',
          options: [
            'IT',
            'Engineering',
            'Operations',
            'Marketing',
            'Finance',
            'Human Resources'
          ]
        }
      ],

      rows: [
        {
          name: 'Ahmed Ali',
          email: 'ahmed@nuvanta.com',
          role: 'Admin',
          department: 'IT',
          status: 'Active'
        },
        {
          name: 'Mona Samir',
          email: 'mona@nuvanta.com',
          role: 'Manager',
          department: 'Operations',
          status: 'Active'
        },
        {
          name: 'Omar Hassan',
          email: 'omar@nuvanta.com',
          role: 'Requester',
          department: 'Marketing',
          status: 'Active'
        },
        {
          name: 'Sara Adel',
          email: 'sara@nuvanta.com',
          role: 'Accountant',
          department: 'Finance',
          status: 'Inactive'
        }
      ]
    },

    settings: {
      title: 'Settings',
      subtitle: 'Manage your profile, company information and preferences.',
      metrics: [],
      columns: [],
      fields: [],
      rows: []
    }
  };

  constructor(private route: ActivatedRoute) {
    this.routeSubscription = this.route.data.subscribe(data => {
      this.loadModule(data['module'] ?? 'suppliers');
    });
  }

  ngOnDestroy(): void {
    this.routeSubscription.unsubscribe();
  }

  private loadModule(moduleName: string): void {
    this.module = moduleName;

    const config = this.data[this.module] ?? this.data['suppliers'];

    this.title = config.title;
    this.subtitle = config.subtitle;
    this.metrics = config.metrics;
    this.columns = config.columns;
    this.fields = config.fields;

    this.rows = config.rows.map((row: any) => ({ ...row }));

    this.search = '';
    this.filter = 'All';
    this.roleFilter = 'All';

    this.formData = {};
    this.selected = null;
    this.showForm = false;

    this.refreshMetrics();
  }

  get filteredRows(): any[] {
    return this.rows.filter(row => {

      const searchText = this.search.toLowerCase();

      const matchesSearch = Object.values(row).some(value =>
        String(value).toLowerCase().includes(searchText)
      );

      const matchesFilter =
        this.filter === 'All' ||
        row.status === this.filter ||
        (
          this.filter === 'Low Stock' &&
          ['Low Stock', 'Out of Stock'].includes(row.status)
        );

      const matchesRole =
        this.module !== 'users' ||
        this.roleFilter === 'All' ||
        row.role === this.roleFilter;

      return matchesSearch && matchesFilter && matchesRole;
    });
  }

  get statuses(): string[] {
    const values = this.rows
      .map(row => row.status)
      .filter(
        (value): value is string =>
          typeof value === 'string' && value.length > 0
      );

    return ['All', ...new Set(values)];
  }

  get roles(): string[] {
    const values = this.rows
      .map(row => row.role)
      .filter(
        (value): value is string =>
          typeof value === 'string' && value.length > 0
      );

    return ['All', ...new Set(values)];
  }

  get pendingCount(): number {
    return this.rows.filter(row => row.status === 'Pending').length;
  }

  get approvedCount(): number {
    return this.rows.filter(row => row.status === 'Approved').length;
  }

  get rejectedCount(): number {
    return this.rows.filter(row => row.status === 'Rejected').length;
  }

  get allocationTotal(): number {
    return this.allocations.reduce(
      (sum, item) => sum + Number(item.amount || 0),
      0
    );
  }

  get calculatedRequestAmount(): number {
    return (
      Number(this.formData['quantity'] || 0) *
      Number(this.formData['unitPrice'] || 0)
    );
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-EG', {
      style: 'currency',
      currency: 'EGP',
      maximumFractionDigits: 0
    }).format(value);
  }

  displayValue(row: any, key: string): string {
    const value = row?.[key];

    if (value === null || value === undefined || value === '') {
      return '—';
    }

    if (
      ['amount', 'allocated', 'spent'].includes(key) &&
      typeof value === 'number'
    ) {
      return this.formatCurrency(value);
    }

    return String(value);
  }

  refreshMetrics(): void {

    if (this.module === 'budget') {

      const total = Number(this.budgetTotalAmount || 1000000);

      const allocated = this.rows.reduce(
        (sum, row) => sum + Number(row.allocated || 0),
        0
      );

      this.metrics = [
        {
          label: 'Total Budget',
          value: this.formatCurrency(total),
          note: `Period: ${this.budgetPeriod}`
        },
        {
          label: 'Allocated',
          value: this.formatCurrency(allocated),
          note: `${total ? Math.round((allocated / total) * 100) : 0}% of total`
        },
        {
          label: 'Available',
          value: this.formatCurrency(total - allocated),
          note: 'Unallocated balance'
        }
      ];

    } else if (this.module === 'approvals') {

      this.metrics = [
        {
          label: 'Pending',
          value: String(this.pendingCount),
          note: 'Needs your attention'
        },
        {
          label: 'Approved',
          value: String(this.approvedCount),
          note: 'Approved requests'
        },
        {
          label: 'Rejected',
          value: String(this.rejectedCount),
          note: 'Rejected requests'
        }
      ];

    } else if (this.module === 'inventory') {

      this.metrics = [
        {
          label: 'Total Items',
          value: String(this.rows.length),
          note: 'Items in inventory'
        },
        {
          label: 'Low Stock Items',
          value: String(
            this.rows.filter(row =>
              ['Low Stock', 'Out of Stock'].includes(row.status)
            ).length
          ),
          note: 'Need replenishment'
        }
      ];
    }
  }

  openAddForm(): void {
    this.formData = {};
    this.showForm = true;
  }

  addAllocation(): void {
    this.allocations.push({
      department: '',
      amount: null
    });
  }

  removeAllocation(index: number): void {
    if (this.allocations.length > 1) {
      this.allocations.splice(index, 1);
    }
  }

  saveBudget(): void {

    if (
      !this.budgetPeriod ||
      !this.budgetTotalAmount ||
      this.budgetTotalAmount <= 0
    ) {
      return;
    }

    if (
      this.allocations.some(
        allocation =>
          !allocation.department.trim() ||
          !allocation.amount ||
          allocation.amount <= 0
      )
    ) {
      return;
    }

    if (this.allocationTotal > this.budgetTotalAmount) {
      alert('Allocations cannot exceed the total budget.');
      return;
    }

    this.rows = this.allocations.map(allocation => ({
      department: allocation.department,
      allocated: Number(allocation.amount),
      spent: 0,
      progress: '0%',
      status: 'On Track'
    }));

    this.refreshMetrics();

    this.showForm = false;

    this.allocations = [
      { department: '', amount: null }
    ];

    alert(`Budget for ${this.budgetPeriod} saved in demo mode.`);
  }

  statusClass(status: string): string {
    const value = (status ?? '').toLowerCase();

    if (
      [
        'approved',
        'active',
        'paid',
        'completed',
        'succeeded',
        'in stock',
        'on track'
      ].includes(value)
    ) {
      return 'bg-emerald-50 text-emerald-700';
    }

    if (
      ['pending', 'warning', 'low stock'].includes(value)
    ) {
      return 'bg-amber-50 text-amber-700';
    }

    if (
      [
        'rejected',
        'inactive',
        'overdue',
        'out of stock',
        'failed'
      ].includes(value)
    ) {
      return 'bg-rose-50 text-rose-700';
    }

    return 'bg-slate-100 text-slate-600';
  }

  addRecord(): void {

    if (!this.fields.length) {
      return;
    }

    const newRecord: any = {
      ...this.formData
    };

    if (this.module === 'purchase-requests') {

      const quantity = Number(newRecord.quantity);
      const unitPrice = Number(newRecord.unitPrice);

      newRecord.id = `PR-${1000 + this.rows.length + 1}`;
      newRecord.date = new Date().toLocaleDateString('en-US');
      newRecord.requester = 'Current Demo User';
      newRecord.amount = quantity * unitPrice;
      newRecord.status = 'Pending';

    } else if (
      this.module === 'suppliers' ||
      this.module === 'users'
    ) {

      newRecord.status = 'Active';

    } else if (this.module === 'inventory') {

      newRecord.quantity = Number(newRecord.quantity);

      newRecord.status =
        newRecord.quantity <= 0 ? 'Out of Stock' : 'In Stock';
    }

    this.rows.unshift(newRecord);

    this.formData = {};
    this.showForm = false;

    this.refreshMetrics();
  }

  updateStatus(row: any, status: string): void {
    row.status = status;

    this.refreshMetrics();

    this.selected = null;
  }

  openDetails(row: any): void {
    this.selected = row;
  }

  saveSettings(): void {
    alert('Settings saved successfully (Demo only).');
  }
}
