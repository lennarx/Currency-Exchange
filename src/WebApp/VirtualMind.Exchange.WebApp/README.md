# VirtualMindExchangeWebApp

For running this app, run the command npm start on the WebApp directory

# VirtualMindExchangeApi
Just open the solution with visual studio, and hit the run button. 

Check the port the api is running on, and replace that port in file /src/WebApp/src/app/shared/services/currency.service.ts line 14

There are 2 known bugs on the frontend that I didn't have time to fix:
1-The spinner loads on the corner left of the screen and not in the middle
2-In the purchase form the input fields (located on the left of the screen below the quotes table) are not visible until you click on them
