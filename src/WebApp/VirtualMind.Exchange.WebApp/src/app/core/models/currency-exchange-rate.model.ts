import { IsoCodes } from "../enums/iso-code.enum";

export interface CurrencyExchangeRateModel {
    isoCode : IsoCodes,
    saleExchangeRate : Number,
    purchaseExchangeRate : Number
}