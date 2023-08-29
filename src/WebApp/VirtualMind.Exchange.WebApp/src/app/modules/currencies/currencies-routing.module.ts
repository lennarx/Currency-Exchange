import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CurrenciesRatesPageComponent } from './pages/currencies-rates-page/currencies-rates-page.component';
import { CurrenciesPurchaseComponent } from './pages/currencies-purchase/currencies-purchase.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'quotes',
    pathMatch: 'full'
  },
  {
    path: 'quotes',
    component: CurrenciesRatesPageComponent
  },
  {
    path: 'purchase',
    component: CurrenciesPurchaseComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CurrenciesRoutingModule { }
