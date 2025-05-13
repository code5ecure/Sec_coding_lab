# EnvVarDemo - Secure API Key Demo (.NET 9)

This solution demonstrates a secure setup for passing an API key from a web frontend to a backend API in ASP.NET Core 9.

## 🔒 Key Security Points

### 1. Sending Parameters via POST is More Secure
- The frontend sends the API key using the `X-API-KEY` header via **POST**.
- **Why POST is safer**: GET requests expose data in URLs (browser history, server logs), while POST keeps headers and body hidden from such logging.

### 2. Avoid `AllowAnyOrigin()` in CORS
- Using `AllowAnyOrigin()` allows *any* website to call your API, which is insecure.
- Instead, explicitly define allowed origins in the backend:
  ```.csharp
  builder.Services.AddCors(options =>
  {
      options.AddDefaultPolicy(policy =>
      {
          policy.WithOrigins("http://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod();
      });
  });
  ```

### 3. How to Set API Key in Environment Variables

#### 🪟 On Windows (PowerShell or CMD)
```powershell
$env:MY_API_KEY = "your-api-key-here"
```
Or persist it for the session:
```powershell
[System.Environment]::SetEnvironmentVariable("MY_API_KEY", "your-api-key-here", "User")
```
To view:
```powershell
[System.Environment]::GetEnvironmentVariable("MY_API_KEY", "User")
```

#### 🐧 On Linux/macOS (bash)
```bash
export MY_API_KEY="your-api-key-here"
```
To make it permanent, add the above line to your `~/.bashrc`, `~/.zshrc`, or `~/.profile`.

---

## ⚙️ appsettings.json Configuration

Both Web and API projects can use `appsettings.json` to configure the default port.

### 🔧 Example for `EnvVarDemo.Api/appsettings.json`:
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5001"
      }
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 🔧 Example for `EnvVarDemo.Web/appsettings.json`:
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      }
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Ensure each project includes this file and is configured to use it in `Program.cs`.

---

## 🚀 Running the Projects

### 1. API Project
```bash
cd EnvVarDemo.Api
dotnet run
```
The API runs on port 5001 by default (as set in `appsettings.json`).

### 2. Web (Frontend) Project
```bash
cd EnvVarDemo.Web
dotnet run
```
The frontend UI runs on port 5000.

Ensure both projects are running, and that CORS and ports are aligned correctly.

## ✅ Test Scenario
1. Run the backend (`EnvVarDemo.Api`) on port 5001.
2. Run the frontend (`EnvVarDemo.Web`) on port 5000.
3. Visit `http://localhost:5000`, enter your API key, and click send.
4. If valid, you'll see a success message. If not, you'll receive a 400 error.

---

© 2025 - Secure Coding Demo
