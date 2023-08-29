import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Observable, catchError, of, throwError } from "rxjs";
import { CurrencyExchangeRateModel } from "src/app/core/models/currency-exchange-rate.model";
import { BadRequestError, InternalServerError, NotFoundError } from "../exceptions/exceptions";
import { PurchaseFormGroup } from "src/app/core/types/purchase-form.types";
import { CurrencyPurchase } from "src/app/core/models/currency-purchase.model";

@Injectable({
    providedIn: 'root'
})

export class CurrencyService {
    private apiBaseURL : string = 'https://localhost:58824/api/currency';
    constructor (private httpClient: HttpClient) {        
    }

    public getCurrencyExchangeRate(isoCode : string) : Observable<CurrencyExchangeRateModel>{
        const url = `${this.apiBaseURL}/${isoCode}/exchangeRate`;
        return this.httpClient.get<CurrencyExchangeRateModel>(url)
            .pipe(catchError((httpError: HttpErrorResponse) => {
                const baseErrorMessage = 'Error getting currency exchange rate';
                const errorMap = new Map<HttpStatusCode, Error>([
                   [HttpStatusCode.BadRequest, new BadRequestError(`${baseErrorMessage}. No valid iso Code provided`)],
                   [HttpStatusCode.NotFound, new NotFoundError(`${baseErrorMessage}. No currency exchange rate was found`)]
                ]);
                const error = errorMap.get(httpError.status) ?? new Error(`An unexpected error occurred while sending the request. "${httpError.error}"`);
                return throwError(() => error);
        }));
    }

    public purchaseCurrency(purchaseForm : PurchaseFormGroup) : Observable<CurrencyPurchase>{
        const url = `${this.apiBaseURL}/${purchaseForm.controls.currency.value}/purchase`;
        const params = {
            userId : purchaseForm.controls.userId.value,
            purchaseAmountInPesos : purchaseForm.controls.amountInPesos.value
        }
        return this.httpClient.post<CurrencyPurchase>(url, params)
           .pipe(catchError((httpError : HttpErrorResponse) => {
                const baseErrorMessage = 'Error while attempting to perform the currency purchase';
                const errorMap = new Map<HttpStatusCode, Error>([
                    [HttpStatusCode.BadRequest, new BadRequestError(`${baseErrorMessage}. The purchase exceeds your monthly limit`)],
                   [HttpStatusCode.NotFound, new NotFoundError(`${baseErrorMessage}. No currency exchange rate was found`)]
                ]);
                const error = errorMap.get(httpError.status) ?? new Error(`An unexpected error occurred while sending the request. "${httpError.error}"`);
                return throwError(() => error);
           }));
    }
}