@CurrencyExchangeRateRetrieval
@CurrencyPurchase
@Shared
Feature:  Currency purchase

As a user, I want the ability buy the currency I want

Scenario: Currency purchase performed succesfully
	Given the user <userId> has not bought currency <isoCode> in the last month
		And I want to buy the currency <isoCode> for an amount of 10000 pesos
		And the exchange rate is successfully retrieved
	When I try to perform the currency purchase
	Then I should see the purchase performed sucessfully

Examples:
	| isoCode | userId |
	| USD     | 1      |
	| BRL     | 2      |

Scenario: USD Currency purchase fails due to limit exceeded
	Given I bought 80 percent of the monthly limit for currency <isoCode> in the last month
		And I want to buy the currency <isoCode> for an amount of 100000000 pesos
		And the exchange rate is successfully retrieved
	When I try to perform the currency purchase
	Then I should see an error thrown
	    And the error thrown should include the message you're trying to buy exceeds your month limit

Examples:
	| isoCode |
	| USD     |
	| BRL     |

Scenario: Previous month exceeded currency purchase limit, and this month I buy currency again
	Given Last month I exceeded the monthly limit for currency <isoCode>
		And I want to buy the currency <isoCode> for an amount of 10000 pesos
		And the exchange rate is successfully retrieved
	When I try to perform the currency purchase
	Then I should see the purchase performed sucessfully

Examples:
	| isoCode |
	| USD     |
	| BRL     |