import { Component, Input, OnInit } from '@angular/core';
import { CurrencyExchangeRateModel } from 'src/app/core/models/currency-exchange-rate.model';

@Component({
  selector: 'app-currencies-rates-table',
  templateUrl: './currencies-rates-table.component.html',
  styleUrls: ['./currencies-rates-table.component.css']
})
export class CurrenciesRatesTableComponent implements OnInit {
  @Input() currencyExchangeRates : CurrencyExchangeRateModel [] = [];
  displayedColumns: string[] = ['iso Code', 'saleExchangeRate', 'purchaseExchangeRate'];
  constructor() { }

  ngOnInit(): void {

  }

}
