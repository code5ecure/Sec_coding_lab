# Sec_Coding_Lab
Training Files for Secure Coding Course

<h2> #first_section </h2>
In the first section, a web form application has been developed using .NET Framework version 4.8.1, which includes a page for user registration followed by user login. The connection to the database is specified through the web.config file.

Using the command,  "aspnet_regiis.exe" -pef "connectionStrings" "<physical_Drive> " 

the connection string is encrypted. (It is necessary to configure the values according to your own database.)

Create a database named "webapp1" and a table named "users" in SQL Server, and then execute the following command in SQL Server:

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);
![image](https://github.com/user-attachments/assets/f3456315-a376-4347-a19d-dd4c101e56ef)

<b>The goal of this section is to make configurations in the web.config file that provide resistance against DOS attacks at the application level. </b>

<h2> #11'st_section </h2>
1. Create a new ASP.NET Core Web API project.
        dotnet new webapi -n SerializationSecurity

2. install packages
dotnet add package Newtonsoft.Json
dotnet add package Google.Protobuf
dotnet add package System.Xml

3. add files to your project.
find vulnerabilities and fix them in sec 11 
