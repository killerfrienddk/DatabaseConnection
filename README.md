# Installation
First you need to add this in to your project folder.

After that you need to add it as a project.

Finally you need to add it as a Project Reference to your main project.

You need to need to add the Services you create and the RContext in your Startup.cs or your Program.cs if it is a discord bot, they need to be set as a singleton like this:
## Normal .net 5 or 3.1 core projects in Startup.cs 
```csharp
public void ConfigureServices(IServiceCollection services) {
	services.AddSingleton<RContext>();
	services.AddSingleton<MemberConnectionService>();
```
## Discord in Program.cs
```csharp
private ServiceProvider ConfigureServices() {
	ServiceCollection serviceCollection = new();
			
		serviceCollection
			.AddSingleton<RContext>()
			.AddSingleton<MemberConnectionService>() // For Services.
}
```
# Setting DBContext Server Connection String
You need to set the server connection under the debug tab under properties when you right click on your project.

# Fluent Api
Docs for Fluent Api: https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

If you look in the DBContext under the Data folder you will find some examples that I have taken from my bot.

# Service Creation
Under the folder called Services you will find a few examples look at the MemberConnectionService.cs there is some examples on how to make one.

You can also copy the EmptyTamplateService.cs and use it.

# RContext Information
The RContext.cs is located under the Data folder.

The RContext.cs is used to contact the database, using predefined functions that is described in the Repository.cs.

You need to Dependency Inject the RContext by doing this in the places where you want to contact your database here is how:
```csharp
private readonly RContext _rContext;

//In order to make a ctor just write in ctor and hit tap twice.
//Ctor stands for constructor.
public CTOR(RContext rContext) {
	_rContext = rContext;
}
```

If you have made services then you can Dependency Inject them like this.
```csharp
private readonly MemberConnectionService _memberConnectionService;

public CTOR(MemberConnectionService memberConnectionService) {
	_memberConnectionService = memberConnectionService;
}
```

You can Dependency Inject both of them or more like this.
```csharp
private readonly MemberConnectionService _memberConnectionService;
private readonly RContext _rContext;

public CTOR(MemberConnectionService memberConnectionService, RContext rContext) {
	_memberConnectionService = memberConnectionService;
	_rContext = rContext;
}
```
