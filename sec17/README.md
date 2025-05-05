# UserProfileApi

An ASP.NET Core Web API project that implements user registration, authentication, session-based login, password reset, and profile management using **ASP.NET Core Identity**. Ideal for use behind an API Gateway (e.g., Kong).

---

## ✨ Features

- 🔐 **User Authentication**: Register, login, logout via ASP.NET Core Identity
- 📄 **User Profile CRUD**: View, update, and delete your profile
- 🕒 **Session Management**: Server-side session-based login
- 🔁 **Password Reset**: Request reset token and set new password
- 🧪 **Swagger UI**: For API testing and exploration
- 🚪 **Kong-Ready Routing**: Clean base path setup for reverse proxying

---

## 🧰 Technologies

- ASP.NET Core 8 Web API
- ASP.NET Core Identity
- Entity Framework Core (Code-First)
- SQL Server
- Swagger / Swashbuckle
- Session-based authentication

---

## 🚀 Getting Started

### 1. Clone the Repository
<br><br>

git clone [https://github.com/yourusername/UserProfileApi.git](https://github.com/code5ecure/Sec_coding_lab.git)
cd UserProfileApi <br>
2. Configure Database <br>
Update your appsettings.json:<br>
"ConnectionStrings": {<br>
  "DefaultConnection": "Server=localhost;Database=UserProfileDb;User Id= ;Password=<>;MultipleActiveResultSets=true"<br>
}<br>
3. Run EF Core Migrations
<br>
dotnet tool install --global dotnet-ef  # if not already installed br>
dotnet ef migrations add Init<br>
dotnet ef database update  <br>
4. Run the API  <br>

dotnet run
<br>
