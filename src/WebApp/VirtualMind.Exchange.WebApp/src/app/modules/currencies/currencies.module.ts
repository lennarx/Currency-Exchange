import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CurrenciesRoutingModule } from './currencies-routing.module';
import { CurrenciesRatesPageComponent } from './pages/currencies-rates-page/currencies-rates-page.component';
import { CurrenciesRatesTableComponent } from './components/currencies-rates-table/currencies-rates-table.component';
import { CurrenciesPurchaseComponent } from './pages/currencies-purchase/currencies-purchase.component';
import { MatTableModule } from '@angular/material/table';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { ProgressSpinnerComponent } from '../currencies/components/progress-spinner/progress-spinner.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PurchaseDialogComponent } from './components/dialog/purchase-dialog/purchase-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    CurrenciesRatesPageComponent,
    CurrenciesRatesTableComponent,
    CurrenciesPurchaseComponent,
    ProgressSpinnerComponent,
    PurchaseDialogComponent
  ],
  imports: [
    CommonModule,
    CurrenciesRoutingModule,
    MatTableModule,
    ReactiveFormsModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatSnackBarModule
  ]
})
export class CurrenciesModule { }
