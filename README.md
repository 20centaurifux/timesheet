# Timesheet - README

**Timesheet** is the prototype of a progressive timesheet web app with support
for multiple user accounts. It's divided into to two parts:

## timesheet-api

**timesheet-api** is a .NET Core 2 REST service using Microsoft SQL server
as database backend. To setup the database please add your connectionstring to
the "appsettings" and run the following commands:

	$ cd timesheet-api
	$ dotnet ef database update

To insert demo data connect to the database and run the "InitialData.sql"
script. Then start the self-hosted web server:

	$ dotnet run

## timesheet-gui

The frontend is a single HTML page. Open the file in your web browser
(Firefox and Chrome work fine) and click on the "User" button. Then enter the
credentials of the previously created demo account:

* username: foxmulder
* password: trustno1

Click the "Reconnect" button to connect the app to the REST service.

The URL of the REST API can be configured in the "timesheet.rest.js" Javascript
file.
