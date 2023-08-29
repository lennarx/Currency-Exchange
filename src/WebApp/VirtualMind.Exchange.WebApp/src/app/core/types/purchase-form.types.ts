import { FormControl, FormGroup } from "@angular/forms";

export enum PurchaseFormFieldName{
    userId = 'userId',
    currency = 'currency',
    amountInPesos = 'amountInPesos'
}

interface PurchaseFormModel{
    [PurchaseFormFieldName.userId] : Number,
    [PurchaseFormFieldName.currency] : string,
    [PurchaseFormFieldName.amountInPesos] : Number
}

type PurchaseFormControls = Record<keyof PurchaseFormModel, FormControl>;
export type PurchaseFormGroup = FormGroup & {
    value: PurchaseFormModel;
    controls: PurchaseFormControls;
}