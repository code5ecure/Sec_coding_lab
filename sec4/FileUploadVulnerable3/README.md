<h2>📁 Secure File Upload Examples in ASP.NET Core </h2> <br><br>
This repository demonstrates three file upload implementations in ASP.NET Core, 
progressing from insecure to secure, for educational purposes.
<br>
<h3>🔍 Project Structure </h3>
<br>
/FileUploadVulnerable1/   --> Basic upload with header check only <br>
/FileUploadVulnerable2/   --> Vulnerable to %00 null byte exploit<br>
/FileUploadSecureTus/     --> Secure resumable uploads via tusdotnet with validation<br>
<h3> 🚨 Version 1 – Insecure Header-Only Check </h3>
Path: FileUploadVulnerable1
<br>
<h3> ✅ Features:</h3><br> 
Accepts uploaded files via form.
<br>
Checks MIME type using the Content-Type HTTP header.

<h3> ❌ Vulnerabilities: </h3>
<br> Trusting User Input: Relies solely on Content-Type header, which can be faked.<br> 

Content Mismatch: Allows non-image files with .jpg extension (e.g., uploading PHP or executable files disguised as images).
<br> 
Example Exploit:
<br> 

<h3>❌ Vulnerabilities: </h3>
Null Byte Injection: Accepts filenames like shell.aspx%00.jpg<br> 

<h3> ✅ Version 3 – Secure Upload with tusdotnet </h3> <br>  
Path: FileUploadSecureTus
<br>  

<h3> 🔐 Security Measures: </h3><br> 
Does not trust the filename or extension.
<br> 
Validates actual file content type using byte inspection.
<br> 
Automatically renames files with a safe GUID + extension format.
<br> 
Serves files from a secure location (wwwroot/uploads) after validation.
<br> 

The tusdotnet implementation provides both security and resumability, making it the most reliable and robust upload method for modern web apps.
<br> 
<h3> ⚙️ Requirements </h3><br>
.NET 7 or later
<br>
tusdotnet NuGet package
<br>
MimeDetective NuGet package
<br>
<h3> 🚀 Usage </h3>
Clone the repo.
<br> 
Run the desired project:<br> 


dotnet run --project FileUploadSecureTus <br>  
Visit https://localhost:48084 and upload an image.<br> 

![Capture](https://github.com/user-attachments/assets/4c11a8b1-ffc9-4077-ae2a-0fa3260ae47c)

<br> 
