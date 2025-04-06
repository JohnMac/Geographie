import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { FormComponent } from './pages/form/form.component';

export const routes: Routes = [
    { path: '', component: DashboardComponent },
    { path: 'form', component: FormComponent },
    { path: 'form/:id', component: FormComponent }
];
