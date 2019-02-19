# ClearBank

In the ‘PaymentService.cs’ file you will find a method for making a payment. At a high level the steps for making a payment are:
1.	Lookup the account the payment is being made from.
2.	Check that the account is in a valid state to make the payment.
3.	Deduct the payment amount from the account’s balance and update the account in the database.
What we’d like you to do is refactor the code with the following things in mind:
•	Adherence to SOLID principals
•	Testability
•	Readability
We’d also like you to add some unit tests to the ClearBank.DeveloperTest.Tests project to show how you would test the code that you’ve produced.
The only specific ‘rules’ are:
•	The solution should build
•	The tests should all pass
•	You should not change the method signature of the MakePayment method.
