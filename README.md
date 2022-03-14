# T-ShirtECommerce
An T-Shirt ECommerce Project built on ASP.NET MVC, WEB API,MS SQL SERVER, BOOTSTRAP, JQUERY,AJAX,JAVASCRIPT

Folder Structure has
1.ScreenShots
2.Database
3.Project

Steps for Configuration
1. Open MS SQL Server, Create a Database and Run the Script inside the Database.
2. Open Project/TShirtecommerce.sln Solution File
3. Project has 1 MVC Project, 1 Web API and 2 Class Library
4. 1. APIAccessLib,DAL,BLL are Class Library Projects
5. 2.TShirtecommerce is a MVC Project
6. 3.WebApi is a ASP.NET Web API Project
7. Open WebAPI Project/ Web.Config
8. Change the Connection String 	
9. 	<connectionStrings>
		<add name="ConnectStr" connectionString="Data Source=localhost;Initial Catalog=NEWDB;Integrated Security=True " providerName="System.Data.SqlClient" />
	</connectionStrings>
10. For ex By default Windows Authentication is configured in the connection string <add name="ConnectStr" connectionString="Data Source=localhost;Initial Catalog=NEWDB;Integrated Security=True " providerName="System.Data.SqlClient" />
11. Please change the DataSource, Initial Catalog as per the Name given in creating new db and Sql Server
12. Open WebConfig of TShirtecommerce and update the API path
13. 	For ex: <add key="BaseUrlAPI" value="https://localhost:44327/" />, If you are running the project from Visual Studio be default no need to change the BaseUrlApi key
14. 	RUN the Project
