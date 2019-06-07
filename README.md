(
https://www.nuget.org/packages/ServiceRegisterCore/# ServiceRegisterCore 
## Class Annotation Based Service Registration for ASP.NET Core

When registring a lot of services in a ASP.NET Core Project ConfigureServices Method often get filled with service add methods.

![ConfigureServices Method](https://raw.githubusercontent.com/Vake93/ASP.NET-Core-Simple-Service-Register/master/readme/ConfigureServices.PNG)
 
This allows you move the declaring of the scope of the service as a class annotation and have a clutter free ConfigureServices Method.

![ConfigureServices Method Updated](https://raw.githubusercontent.com/Vake93/ASP.NET-Core-Simple-Service-Register/master/readme/ConfigureServicesUpdate.PNG)

![TodoItemService Class with annotation](https://raw.githubusercontent.com/Vake93/ASP.NET-Core-Simple-Service-Register/master/readme/TodoItemService.PNG)

![EmailSender Class with annotation](https://raw.githubusercontent.com/Vake93/ASP.NET-Core-Simple-Service-Register/master/readme/EmailSender.PNG)


## Sample Project

Sample Project using ServiceRegisterCore can be found [here!](https://github.com/Vake93/little-aspnetcore-todo)

## Get It on Nuget

[https://www.nuget.org/packages/ServiceRegisterCore/](https://www.nuget.org/packages/ServiceRegisterCore/)
