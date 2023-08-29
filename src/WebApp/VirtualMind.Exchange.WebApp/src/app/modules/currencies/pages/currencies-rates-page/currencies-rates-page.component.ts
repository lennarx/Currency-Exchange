import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { IsoCodes } from 'src/app/core/enums/iso-code.enum';
import { CurrencyExchangeRateModel } from 'src/app/core/models/currency-exchange-rate.model';
import { CurrencyService } from 'src/app/shared/services/currency.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-currencies-rates-page',
  templateUrl: './currencies-rates-page.component.html',
  styleUrls: ['./currencies-rates-page.component.css']
})
export class CurrenciesRatesPageComponent implements OnInit {
  currenciesExchangeRates : CurrencyExchangeRateModel[] = [];
  isLoading : boolean = false;
  
  constructor(private currencyService : CurrencyService, private snackBar : MatSnackBar) { }
  

  ngOnInit(): void {
    this.isLoading = true;
    const codes = Object.keys(IsoCodes);
    const currencyExchangeRatesApiCalls$ = codes.map((code) => this.currencyService.getCurrencyExchangeRate(code));
    forkJoin(currencyExchangeRatesApiCalls$).subscribe({
      next: (currencyExchangeRatesApiCalls) => {
         this.currenciesExchangeRates = currencyExchangeRatesApiCalls;
         this.isLoading = false
      },
      error:(err) => {    
        this.openSnackBar(err.message, 'close')    
      }
    })
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }
}
