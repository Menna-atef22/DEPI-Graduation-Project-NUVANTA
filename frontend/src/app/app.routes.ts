import { Routes } from '@angular/router';

import { Login } from './features/auth/login/login';
import { Dashboard } from './features/dashboard/dashboard';
import { ModulePage } from './shared/components/module-page/module-page';
import { MainLayout } from './layout/main-layout/main-layout';

export const routes: Routes = [
  {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
  },

  {
    path: 'login',
    component: Login
  },
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: 'dashboard',
        component: Dashboard
      },
      {
        path: 'budget',
        component: ModulePage,
        data: { module: 'budget' }
      },
      {
        path: 'purchase-requests',
        component: ModulePage,
        data: { module: 'purchase-requests' }
      },
      {
        path: 'approvals',
        component: ModulePage,
        data: { module: 'approvals' }
      },
      {
        path: 'suppliers',
        component: ModulePage,
        data: { module: 'suppliers' }
      },
      {
        path: 'inventory',
        component: ModulePage,
        data: { module: 'inventory' }
      },
      {
        path: 'invoices',
        component: ModulePage,
        data: { module: 'invoices' }
      },
      {
        path: 'payments',
        component: ModulePage,
        data: { module: 'payments' }
      },
      {
        path: 'users',
        component: ModulePage,
        data: { module: 'users' }
      },
      {
        path: 'settings',
        component: ModulePage,
        data: { module: 'settings' }
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];