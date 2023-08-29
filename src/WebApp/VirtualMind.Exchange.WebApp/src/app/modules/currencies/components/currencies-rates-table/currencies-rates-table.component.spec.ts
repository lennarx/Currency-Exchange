import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrenciesRatesTableComponent } from './currencies-rates-table.component';

describe('CurrenciesRatesTableComponent', () => {
  let component: CurrenciesRatesTableComponent;
  let fixture: ComponentFixture<CurrenciesRatesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrenciesRatesTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrenciesRatesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
