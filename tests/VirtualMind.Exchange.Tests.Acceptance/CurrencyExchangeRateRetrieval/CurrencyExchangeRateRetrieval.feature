Feature:  Get Currency exchange rate 

As a user, I want the ability to retrieve the currency exchange rate 

Scenario: Retrieval of currency exchange rate performed succesfully
	Given a valid '<isoCode>'
	When I try to retrieve the currency exchange rate
	Then I should see the exchange rate returned

Examples:
	| isoCode |
	|  USD  |
	|  BRL  |


Scenario: Retrieval of currency exchange rate fails due to invalid isoCode provided
	Given an invalid isoCode invalidIsoCode
	When I try to retrieve the currency exchange rate
	Then I should see an error
		And the error should include the message No currency was found

Scenario: Retrieval of currency exchange rate fails due failure response recieved from the external service
	Given a valid '<isoCode>'
		But the external service returns a failure message
	When I try to retrieve the currency exchange rate
	Then I should see an error
		And the error should include the message Request fail

Examples:
	| isoCode |
	|  USD  |
	|  BRL  |