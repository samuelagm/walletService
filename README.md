
   

## WalletApi 
![.NET Core](https://github.com/samuelagm/walletApi/workflows/.NET%20Core/badge.svg)

---
#### Getting Started
.Net Core version: 3.1  

Install Deps: `dotnet restore`  

Run: `dotnet run -p WalletApi`  

Test: `dotnet test`  
#### Brief

A simple wallet service that performs the following functions

1.    Creating a wallet and yields an account number.
2.    Depositing funds to wallet.
3.    Withdrawing funds from wallet.
4.    Returning wallet transaction.

Deposit and Withdrawals (2&3) are handled by the `api/Wallet/CreateTransaction` endpoint. A Transaction with a negetive value is treated as a withdrawal and a positive value is a deposit. This was designed with event sourcing in mind where the state of an object can be recreated by replaying all events or in our case by summing up the values of the events.

Creating and Retrieving transactions are handled by the `api/Wallet` and `api/Wallet/GetTransactions` API endpoints respectively.   
   
      

#### Security
This API was designed to be run behind a gateway that would provide security and authentication, API security is a complex and specialised challenge that should be handled by a different service especially in a microservice environment. No security feature was implemented in this API.
