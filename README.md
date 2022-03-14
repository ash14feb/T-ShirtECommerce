# T-ShirtECommerce
An T-Shirt ECommerce Project built on ASP.NET MVC, WEB API,MS SQL SERVER, BOOTSTRAP, JQUERY,AJAX,JAVASCRIPT

SOFTWARE:
Microsoft Visual Studio 2019
MS SQL Server 2018

Folder Structure has
1.ScreenShots
2.Database
3.Project

DATABASE SETUP
------------------------------------------------------------------------------------
1. CREATE A blank database in MS SqlServer and Run below script on top of the newly created DB
2. DB_Schema_TShirtecom.SQL

APP SETUP
------------------------------------------------------------------------------------
WEB.CONFIG Change at WEBAPI Project
1. Open WebAPI Project/ Web.Config
2. Change the Connection String 	

		<add name="ConnectStr" connectionString="Data Source=localhost;Initial Catalog=NEWDB;Integrated Security=True " providerName="System.Data.SqlClient" />

3. For ex By default Windows Authentication is configured in the connection string <add name="ConnectStr" connectionString="Data Source=localhost;Initial Catalog=NEWDB;Integrated Security=True " providerName="System.Data.SqlClient" />
11. Please change the DataSource, Initial Catalog as per the name given in creating new db and Sql Server

WEB.CONFIG in MVC PROJECT
------------------------------------------------------------------------------------
1. Open WebConfig of TShirtecommerce and update the API path
2. For ex: <add key="BaseUrlAPI" value="https://localhost:44327/" />, If you are running the project from Visual Studio be default no need to change the BaseUrlApi key
3. RUN the Project

PROJECT STRUCTURE
-------------------------------------------------------------------------------------------
1. Open Project/TShirtecommerce.sln Solution File, Solution has all 5 projects
2. Project has 1 MVC Project, 1 Web API and 2 Class Library
3. APIAccessLib,DAL,BLL are Class Library Projects
4. TShirtecommerce is a MVC Project
5. WebApi is a ASP.NET Web API Project

STEPS TO RUN
----------------------------------------------------------------------------
Once All configurations are done, 
1. RUN WEBAPI Project
2. RUN MVC Project
3. Note: WEBAPI URL should be given in the MVC Project WEB Config
