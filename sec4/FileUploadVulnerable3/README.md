📁 Secure File Upload Examples in ASP.NET Core
This repository demonstrates three file upload implementations in ASP.NET Core, 
progressing from insecure to secure, for educational purposes.

🔍 Project Structure

/FileUploadVulnerable1/   --> Basic upload with header check only
/FileUploadVulnerable2/   --> Vulnerable to %00 null byte exploit
/FileUploadSecureTus/     --> Secure resumable uploads via tusdotnet with validation
🚨 Version 1 – Insecure Header-Only Check
Path: FileUploadVulnerable1

✅ Features:
Accepts uploaded files via form.

Checks MIME type using the Content-Type HTTP header.

❌ Vulnerabilities:
Trusting User Input: Relies solely on Content-Type header, which can be faked.

Content Mismatch: Allows non-image files with .jpg extension (e.g., uploading PHP or executable files disguised as images).

Example Exploit:


❌ Vulnerabilities:
Null Byte Injection: Accepts filenames like shell.aspx%00.jpg

✅ Version 3 – Secure Upload with tusdotnet
Path: FileUploadSecureTus


🔐 Security Measures:
Does not trust the filename or extension.

Validates actual file content type using byte inspection.

Automatically renames files with a safe GUID + extension format.

Serves files from a secure location (wwwroot/uploads) after validation.


The tusdotnet implementation provides both security and resumability, making it the most reliable and robust upload method for modern web apps.

⚙️ Requirements
.NET 7 or later

tusdotnet NuGet package

MimeDetective NuGet package

🚀 Usage
Clone the repo.

Run the desired project:


dotnet run --project FileUploadSecureTus
Visit http://localhost:5000 and upload an image.

![Capture](https://github.com/user-attachments/assets/4c11a8b1-ffc9-4077-ae2a-0fa3260ae47c)

