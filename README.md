# aurelia-.netcore-app

.NetCore3.1 rest api app for crud operations of applicants
Database creational scripts are in CreateBaseScripts folders and just need to be runned on mssql
FrontEnd has been made in Aurelia 

To run the front end first we need to run : 
npm install 
and then 
npm start

The aurelia app is currently set up for the iis express domain 

To run the kestrel, we just need to go with cmd in folder Hahn.ApplicationProcess.Web and run but before that you need to make sure to change the domain of the fetch in aurelia 

dotnet start
