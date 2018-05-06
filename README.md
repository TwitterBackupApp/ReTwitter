# ReTwitter 
ASP.NET Core MVC Graduation Project for Telerik Academy Alpha

ReTwitter is a web application where you can register or use your twitter credentials.
You can search for people and follow them through the app, check out their recent tweets,
save your favourites and, of course, you can unfollow people. You can also browse through your personal collection of followees.
The administrative part gives permission to change Users' roles. There you can also check out app's statistics such as the number of people registered (active and deleted), saved and deleted Twitter accounts and Tweets. Also you can delete followees and/or tweets if you find some uncensored content.

# Back-end
The back-end part of the app is divided into several assemblies in the Business folder. The approach for building the database was CodeFirst. Repository pattern and Unit of Work are also used in order to better separate the DB layer from the Business one and to insure transactions.

# Front-end
In the front-end part are used Bootstrap, CSS and JavaScript in order to create a better user experiance.

# Unit Tests
We use Unit Tests to test the Business layer.
