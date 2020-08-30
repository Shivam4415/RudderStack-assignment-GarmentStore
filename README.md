# Assignment_Shopping cart

Prerequisites:
1. JS
2. Css
3. Html
4. Asp .net
5. Sql Server
6. C#
7. Jquery
8. Uikit
9. Transact Sql
10. Dapper ORM
 

Setup Instructions : 

-- Database Design(SQL Server, Stored Procedure):
  1. [UserSessions] -> Track no. of users logged In and save Save their session in this table.
  2. [USERS] -> Number of users registered
  3. [Product] -> List of products we provide
  4. [ProductSize] -> Mapping of products vs Size
  5. [ProductColor] -> Mapping of Products vs Color.
  6. [ProductPrice] -> Mapping of products vs Price
  7. [Price] -> List of price we provide
  8. [UserProduct] -> List of products assignd to user.
  
-- Backend Design (C#):
  1. Services layer(Businnes Logic)
  2. Repository Layer(Dapper ORM)
  3. Entities(Model Design)
  4. Global(Global Reference Class)

-- Web API Controller:
  1. ProductsController : Get, Post, Put, Delete for a product
  2. UsersController : Login and Register Action
  
-- Authentication:
  1. Asp .Net Forms Authentication
  
-- FrontEnd Design:
  1. Aspx File
  2. Js File
  3. Uikit(Front End framework)
  4. Jquery


  
  
