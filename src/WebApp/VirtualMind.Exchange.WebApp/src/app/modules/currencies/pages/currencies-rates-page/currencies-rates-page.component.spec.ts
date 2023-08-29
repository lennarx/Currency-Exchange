import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrenciesRatesPageComponent } from './currencies-rates-page.component';

describe('CurrenciesRatesPageComponent', () => {
  let component: CurrenciesRatesPageComponent;
  let fixture: ComponentFixture<CurrenciesRatesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrenciesRatesPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrenciesRatesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
