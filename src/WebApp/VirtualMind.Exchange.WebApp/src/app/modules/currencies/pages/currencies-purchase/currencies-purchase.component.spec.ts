import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrenciesPurchaseComponent } from './currencies-purchase.component';

describe('CurrenciesPurchaseComponent', () => {
  let component: CurrenciesPurchaseComponent;
  let fixture: ComponentFixture<CurrenciesPurchaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrenciesPurchaseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrenciesPurchaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
