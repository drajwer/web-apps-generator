# Introduction 
The program created within the confines of this project generates a fully functional multilayer web application which enables users, via a browser, to perform operations on a database.
The data model is defined in a data description language specially designed for that purpose.
The goal of this project is to save time of software developers by automating the most tedious part of creating a web application.

The generator is a .NET Core console application written in C#.
It creates a client and server applications along with a database.
Client application is a graphical user interface facilitating communication with the server.
It was built using React and Redux libraries.
Server application processes user's requests and performs operations on the data stored in the database in CRUD manner.
It uses ASP.NET Core and utilizes Entity Framework package to communicate with the database.


# Getting Started
Please take a look at [wiki](https://github.com/drajwer/web-apps-generator/wiki).
It describes how to run the generator and how to prepare model of web application to be generated.

# Contribute
Please feel free to add pull requests.
The most needed feature is to enable users to provide model to the generator via other languages (e.g. C# POCO classes).
