# Introduction 
Hello FlapKap's team,
I hope you're all doing well. I'm excited to present my API solution to you. I've put a lot of effort into crafting it, and I sincerely hope you find it to your liking. If you have any questions or feedback, please don't hesitate to reach out. I'm looking forward to hearing your thoughts.


# Overview
- To run the API, you must first have the .NET Core 7 SDK installed, as I built it using .NET 7. Secondly, open the solution in Visual Studio Code. Then, in the Package Manager Console, run the two commands:
  ```bach
  add-migration init
  update-database

- I have also seeded some data in the database with the first migration. I added three users (Admin, Buyer, Seller) and ten products for the Seller.

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