# RAZOR_LibraryManagement

## WIP state

ASP.NET 6 Core application to manage a library  developed using C# and Visual Studio 2022.   
Represents the back office of a library where the users of the app can manage the state of the books, the users of the library
and all the aspects needed to make their work.

- ## Arquitecture:
	- Four layers arquitecture separated in four projects.
	- A fifth project for Lang resources
	- Projects/layers:
	  - Infra -> Using repository pattern and Unit of work. The interaction with Database is made with **Entity FrameWork Core 6**
	  - Domain -> Services layer. Works with data and send it to upper layer. Also has all the interfaces for dependency injection
	  - Web -> Manage the data form services and render it to the user. The technology used is **Razor pages**
      - Models -> Holds the whole entities, models and viewModels for the Solution. Also has the Automapper mapping files.
      - Lang -> Here there is defined the resources for the user interfaces and more

- ## Technologies:
	- ASP.NET Core 6
	- Visual Studio 2022 and C#
	- Entity FrameWork Core 6
	- Razor pages
	- Automapper
	- Dependency injection and Inversion of dependency
	
- ## Data flow:
    - The repository works with database using `Entities`. When the repo gets the data, transfoms it into `Models` and send it to Domain layer
	- The service layer recives the data, make the bussynes logic necesary and pass it to the web layer.
	- The web layer recives the data, transform it into `ViewModels` and render it in the UI
	- For all these transformations it´s used `Automap` with Profiles defined in the Models project.

- ## Autentication and Authorization
    - Microsoft Identity with Roles
	- For autentication there is a exclusive context and database
	- Roles:
	  - SuperAdmin -> Just one. Special features and menus only accesible to SA role. Can create Admins and Users and use the app without restriction.
	  - Admin -> Represent who is going to work with the app. Can create users but not others admins. Can`t access to SA menu.
	  - User -> Represents who is going to borrows books in the linrary. 


### For more info about more concret aspects, visit the README.md files in the prjects 
