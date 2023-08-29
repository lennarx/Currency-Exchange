import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IsoCodes } from 'src/app/core/enums/iso-code.enum';
import { PurchaseFormFieldName, PurchaseFormGroup } from 'src/app/core/types/purchase-form.types';
import { CurrencyService } from 'src/app/shared/services/currency.service';
import { MatDialog } from '@angular/material/dialog';
import { CurrencyPurchase } from 'src/app/core/models/currency-purchase.model';
import { PurchaseDialogComponent } from '../../components/dialog/purchase-dialog/purchase-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-currencies-purchase',
  templateUrl: './currencies-purchase.component.html',
  styleUrls: ['./currencies-purchase.component.css']
})
export class CurrenciesPurchaseComponent implements OnInit {
  isoCodes: string[] = Object.keys(IsoCodes);
  purchaseForm!: PurchaseFormGroup
  isLoading: boolean = false;

  constructor(private formBuilder: FormBuilder, private currencyService: CurrencyService, private dialog: MatDialog, private snackBar : MatSnackBar) {
    this.purchaseForm = this.formBuilder.group({
      [PurchaseFormFieldName.userId]: ['', Validators.required],
      [PurchaseFormFieldName.currency]: ['', Validators.required],
      [PurchaseFormFieldName.amountInPesos]: ['', Validators.required]
    }) as PurchaseFormGroup;
  }

  ngOnInit(): void {

  }

  onSubmit() {
    this.isLoading = true;
    this.currencyService.purchaseCurrency(this.purchaseForm).subscribe({
      next: (currencyPurchase) => {
        this.isLoading = false;
        this.openDialog(currencyPurchase);
      },
      error: (error) => {
        this.isLoading = false;
        this.openSnackBar(error.message, 'close');
      }
    })
  }

  openDialog(purchase: CurrencyPurchase) {
    this.dialog.open(PurchaseDialogComponent, {
      data: {
        purchaseAmountInPesos : purchase.purchaseAmountInPesos,
        currencyExchangeRate  : purchase.currencyExchangeRate,
        amountPurchased : purchase.amountPurchased
      },
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action);
  }
}
