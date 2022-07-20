import {DEFAULT_CURRENCY_CODE, NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule } from '@angular/material/paginator';
import { InquiryComponent } from './components/inquiry/inquiry.component';
import { RoleComponent } from './components/role/role.component';
import { ProductsComponent } from './components/products/products.component';
import { ProductFormComponent } from './components/products/product-form/product-form.component';
import { OrdersFormComponent } from './components/orders/orders-form/orders-form.component';
import { OrdersComponent } from './components/orders/orders.component';
import { SupplierComponent } from './components/supplier/supplier.component';
import { SupplierFormComponent } from './components/supplier/supplier-form/supplier-form.component';
import { WorkersFormComponent } from './components/workers/workers-form/workers-form.component';
import { WorkersComponent } from './components/workers/workers.component';
import {AboutComponent} from "./components/about/about.component";
import {RoleFormComponent} from "./components/role/role-form/role-form.component";
import {HomeComponent} from "./components/home/home.component";
import {UsersComponent} from "./components/users-and-roles/users.component";
import {LoginComponent} from "./components/login/login.component";
import {CreateAndUpdateUserComponent} from "./components/users-and-roles/create-and-update-user/create-and-update-user.component";
import {CreateAndUpdateModalComponent} from "./components/inquiry/create-and-update-modal/create-and-update-modal.component";
import {PurchaseComponent} from "./components/purchase/purchase.component";
import {PurchaseFormComponent} from "./components/purchase/purchase-form/purchase-form.component";
import { CancelledOrdersListComponent } from './components/orders/cancelled-orders-list/cancelled-orders-list.component';
import {MatSnackBarModule} from "@angular/material/snack-bar";
import { CancelledPurchaseListComponent } from './components/purchase/cancelled-purchase-list/cancelled-purchase-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    LoginComponent,
    UsersComponent,
    CreateAndUpdateUserComponent,
    InquiryComponent,
    RoleComponent,
    CreateAndUpdateModalComponent,
    RoleFormComponent,
    ProductsComponent,
    ProductFormComponent,
    OrdersFormComponent,
    OrdersComponent,
    SupplierComponent,
    SupplierFormComponent,
    WorkersFormComponent,
    WorkersComponent,
    PurchaseComponent,
    PurchaseFormComponent,
    CancelledOrdersListComponent,
    CancelledPurchaseListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatTableModule,
    MatDialogModule,
    MatInputModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatPaginatorModule,
    MatSnackBarModule,
  ],
  providers: [{provide: DEFAULT_CURRENCY_CODE, useValue: 'INR' }],
  bootstrap: [AppComponent],
})
export class AppModule { }
