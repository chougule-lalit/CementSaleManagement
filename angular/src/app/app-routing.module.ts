import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {InquiryComponent} from './components/inquiry/inquiry.component';
import {AuthGuard} from './shared/guards/auth.guard';
import {RoleGuard} from './shared/guards/role.guard';
import {ProductsComponent} from './components/products/products.component';
import {OrdersComponent} from './components/orders/orders.component';
import {SupplierComponent} from './components/supplier/supplier.component';
import {WorkersComponent} from './components/workers/workers.component';
import {PurchaseComponent} from "./components/purchase/purchase.component";
import {RoleComponent} from "./components/role/role.component";
import {HomeComponent} from "./components/home/home.component";
import {UsersComponent} from "./components/users-and-roles/users.component";
import {LoginComponent} from "./components/login/login.component";
import {AboutComponent} from "./components/about/about.component";
import {CancelledOrdersListComponent} from "./components/orders/cancelled-orders-list/cancelled-orders-list.component";
import {CancelledPurchaseListComponent} from "./components/purchase/cancelled-purchase-list/cancelled-purchase-list.component";

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'about', component: AboutComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'users', component: UsersComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'enquiry', component: InquiryComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'products', component: ProductsComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'purchase', component: PurchaseComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'orders', component: OrdersComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'supplier', component: SupplierComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'workers', component: WorkersComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'roles', component: RoleComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'cancelled-orders', component: CancelledOrdersListComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: 'cancelled-purchase', component: CancelledPurchaseListComponent, canActivate: [AuthGuard, RoleGuard], data: {expectedRoles: 1}},
  {path: '**', redirectTo: 'dashboard'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
