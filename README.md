# Code Crate<br>By Data Forge (SCCC CPT-200, Team-1)

[Click here to watch our introductory video](https://youtu.be/Dy07fkVlzNw)

Code Crate is a fullstack, web-based password tracker created as a capstone project by a four person team from St Charles Community College in Cottleville, MO. The web application is designed to be accessable on the majority of PC's, tablets, and smartphones on the market today.

## How To Use
Complilation of the source code requires installation of the listed libraries in ["Under The Hood."](#under-the-hood) Once the software is running, Code Crate can be connected to locally by connecting to localhost:7089 in your web browser.

### Account Creation
1. Once on front page of site, click the "Register" button.
2. Input desired username, password, and email. These will be checked to see if the username or email are already in use.
3. Click "Register".
4. You will be returned to the front page once successful, with a small pop up at the top confirming the success of the account creation. Otherwise, you will get a notification that either your password doesn't match itself for confirmation or that the username or email are already in use. You will then have to correct the info, and try again.
5. You are now free to Log in to Code Crate with your account info.

### Creating Credentials to Save
1. Once logged in, you will see our interface on the main page. There will be a "+ New" button near the top left.
2.  A window will pop up and you can begin adding in information that you are wanting to save. Application will refer to the name of the service you are wanting to save. You can then add the username and password of the service.
  a. If you do not yet have a password in mind to use, there is a password generator below the password field you can use to generate a random strong password.
3. Clicking the "Save" button then saves the information and returns you to the main page, with the added service now in the table.
4. Once added, you can edit and delete any information that has been added.
5. The "Pencil" icon button allows you to update any information saved, such as when you need to update a password, or if you have changed your username.
6. The "Trash Bin" icon button allows you to fully delete any saved services. There is a confirmation page, so no worries of accidental deletion.
7. The "Clipboard" icon button will copy the data onto the devices temporary memory (Clipboard) to be pasted where you need it.
8. Once you are finished using our application, you can log out using the "Sign out" button, located at the top right of the screen.

## Under The Hood
The backend is written in C# and makes use of the following libraries:
### [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
&#9878; Apache Licence 2.0  
A .NET API solution from Microsoft. Recomended by Bailey.

### [QuickGrid](https://aspnet.github.io/quickgridsamples/)
&#9878; MIT Licence  
Developed by Microsoft. Utilized in the Code Crate front end UI.

### [CsvHelper](https://joshclose.github.io/CsvHelper/)
&#9878; Apache Licence 2.0 & Microsoft Public Licence (MS-PL)  
Written by Josh Close. Used in backend database management

Please note that these dependencies are not included in this repository and must be installed on your system in order to compilee.

## Meet The Team
### Connor Clawson (Project Manager / Developer)
- Co-manages repository with Bailey Ducommun
- Developed encryption solution
- Coordinates tasks with entire team

### Bailey Ducommun (Full-Stack Developer)
- Co-manages repository with Connor Clawson
- Lead API implementation

### Phuong Anh Nguyen (Back-End Developer)
- Developed server backend
- Co-developed back end with Bailey Ducommun
- Co-developed back end with Connor Clawson

### Ryan Schoonover (Front-End Developer)
- Designed custom graphics for front end
- Co-developed front end with Bailey Ducommun

## Planned Features
- Two-factor authentication

This repository is intended for the academic use of its contributors.
