import { Routes } from '@angular/router';
import { AuthGuard } from '../injectable/auth-guard.injectable';

export const routes: Routes = [
    {
        path: '',
        canActivate: [AuthGuard],
        loadComponent: () => import('../pages/todo/todo.component').then((item) => item.TodoComponent),
    },
    {
        path: 'Login',
        loadComponent: () => import('../pages/login/login.component').then((item) => item.LoginComponent),
    },
    {
        path: 'Todo',
        canActivate: [AuthGuard],
        loadComponent: () => import('../pages/todo/todo.component').then((item) => item.TodoComponent),
    },
    {
        path: 'TodoMore',
        canActivate: [AuthGuard],
        loadComponent: () => import('../pages/todo-more/todo-more.component').then((item) => item.TodoMoreComponent),
    },
    {
        path: 'Form',
        canActivate: [AuthGuard],
        loadComponent: () => import('../pages/form/form.component').then((item) => item.FormComponent),
    },
    {
        path: 'Query',
        canActivate: [AuthGuard],
        loadComponent: () => import('../pages/query/query.component').then((item) => item.QueryComponent),
    },
    {
        path: 'SwitchUser',
        loadComponent: () => import('../pages/switch-user/switch-user.component').then((item) => item.SwitchUserComponent),
    },
    {
        path: 'SampleInputCsv',
        loadComponent: () => import('../pages/sample-input-csv/sample-input-csv.component').then((item) => item.SampleInputCsvComponent),
    }
];
