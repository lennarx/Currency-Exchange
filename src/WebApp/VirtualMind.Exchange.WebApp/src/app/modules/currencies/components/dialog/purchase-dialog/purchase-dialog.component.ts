import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CurrencyPurchase } from 'src/app/core/models/currency-purchase.model';

@Component({
  selector: 'app-purchase-dialog',
  templateUrl: './purchase-dialog.component.html',
  styleUrls: ['./purchase-dialog.component.css']
})
export class PurchaseDialogComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public data: CurrencyPurchase) { }
}
