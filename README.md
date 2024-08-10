# Introduction 
This project was a backend challenge that implements a Vending Machine API using C# and .NET. It is designed to manage products and transactions in a vending machine. The API allows users with different roles—sellers and buyers—to interact with the vending machine according to their permissions. This project uses Clean Architecture, implements authentication best practices, and includes unit testing.


# Overview
- To run the API, you must first have the .NET Core 7 SDK installed, as I built it using .NET 7.
- Secondly, open the solution in Visual Studio. Then, in the Package Manager Console, run the two commands:
  ```bach
  add-migration init
  update-database

- Ensure that the API project is set as the startup project. In the Package Manager Console, make sure to set the Infrastructure project as the default project (Very Important).
![0](https://github.com/Mohamed-Warda/read-me2/assets/120992737/3df7cdbc-013f-4171-b011-a5d288c7889b)

- I have also seeded some data in the database with the initial migration. I added three users (Admin, Buyer, Seller) and ten products for the Seller.
- The project was structured using clean architecture and the repository pattern. I didn't use the unit of work pattern as I only had one repository. Additionally, I didn't use AutoMapper because my entities had a small number of properties that I could map manually, and AutoMapper wasn't recommended in this case as it would add an unnecessary layer of abstraction to my code. I also implemented exception handling middleware, logging with Serilog, and added some custom validation attributes. Furthermore, I included unit testing

# Let's begin
#### Allow me to lead you on a journey into the API.
- First, this is the data that was inserted into the database
- If you want to log in, you can use any of the three accounts.
  ```bach
  buyer@vm.com
  seller@vm.com
  admin@vm.com
  ```
  ###### All of them have the same password: Admin@123
  ![image](https://github.com/Mohamed-Warda/read-me2/assets/120992737/92f79b00-5d76-46c4-a2f4-3a95882c3c19)
 
1. First, I will create a new user with the role of 'Seller'.
![2](https://github.com/Mohamed-Warda/read-me2/assets/120992737/945ef634-adca-4052-b0b2-ab4e7c1805a6)
 - This would be the response
![22](https://github.com/Mohamed-Warda/read-me2/assets/120992737/837e720b-7857-48b2-8c02-d44cbbd18dd4)
- You can go to the login endpoint to login and obtain the token. However, I generate a token when a user registers, so you can use that. Don't forget to include the word 'Bearer' before the token.

2. Take the token and add it to the Swagger authorization
![3](https://github.com/Mohamed-Warda/read-me2/assets/120992737/422bd48c-b4b3-4a87-a2b8-42b0dbf100d8)
- **click and add the token then click Authorize**
![33](https://github.com/Mohamed-Warda/read-me2/assets/120992737/c107af8b-5899-4460-ab1b-422386f51b92)
###### now you are logged as as a seller
**And now You Can add Products**
![4](https://github.com/Mohamed-Warda/read-me2/assets/120992737/92bc3852-f7f2-4192-a9cb-5eb7a69125fa)

- **The response, and notice that you don't have to enter the SellerId as I obtain it from the token claims**
![44](https://github.com/Mohamed-Warda/read-me2/assets/120992737/701df7e0-05bf-43d3-9848-09cd1ff2327f)

### Now, for the Buyer user, I won't create a new account. Instead, I'll use the account that I previously set up with the Seeder (It will be inserted automatically when you run the migration commands)
- **So, log in with `buyer@vm.com`**

#### Let's buy some snacks from our vending machine
- **So first, I will deposit some coins. The machine only accepts coins with the following values: [5, 10, 20, 50, 100]**
- **So when I tried to deposit 30, the machine returned BadRequest**
![5](https://github.com/Mohamed-Warda/read-me2/assets/120992737/774a82ef-e65d-4786-9e95-c595394231f9)
- **So instead, I deposited 100 and I'm ready to buy my favorite snacks**
- **And again, you don't have to add the BuyerId; I set it from the token**
 ![55](https://github.com/Mohamed-Warda/read-me2/assets/120992737/3cb662ef-31ee-495c-9d28-21b740bb6265)

- **I will go to my GetAllProducts endpoint to see all the products (Access is only allowed to accounts with a buyer role)**
![7](https://github.com/Mohamed-Warda/read-me2/assets/120992737/ff0e6b99-bf30-4779-8ef2-6b3d4ff8a3fe)
- **i will buy 1 soda and 1 bag of chips chips (note that they both cost 150 and i deposited only 100)**
- **In my BuyProduct endpoint, I bought a soda (productId=1 and amount=1). Note that the response says that I have one coin with a value of 50 as change**
 ![77](https://github.com/Mohamed-Warda/read-me2/assets/120992737/9716379b-d686-4023-a9b6-f5b84770d875)
- **So I still have 50 in my deposit. I will now buy a bag of chips that costs 100**
- **And as expected, an error message appears saying that I don't have enough deposit and should deposit more.**
 ![8](https://github.com/Mohamed-Warda/read-me2/assets/120992737/6d898d8e-58f2-4f08-838c-83a079dc4212)
- **So, I will deposit another 100 and buy the bag of chips**
- **Finally, I will go to the ConfirmPurchase endpoint to confirm my purchases and take my change (should have coin with a value of 50) and my snacks.**
![1000](https://github.com/Mohamed-Warda/read-me2/assets/120992737/42e50c8d-fa1e-40bc-9bac-27bfca979347)

#### The response includes my change, which is 1 coin with the value of 50, and the product I bought with its amount. Now, I will enjoy my snacks
- **This is just the main scenario. You can test more if you want. You can reset the purchases after buying products and before confirming the purchases**


  









