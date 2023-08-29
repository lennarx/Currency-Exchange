import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [ 
  {
    path: '',
    redirectTo: 'currencies',
    pathMatch: 'full'
  },
  {
    path: 'currencies',
    loadChildren: () => import(`./modules/currencies/currencies.module`).then(m => m.CurrenciesModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
