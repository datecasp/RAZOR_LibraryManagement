## RAZOR-LibraryManagement

## Web project

This is the outter layer of the solution. The main entry for the app is the Index.cshtml that shows a login. If you are not logged in, no URL is accesible and the navbar is empty.  
You can loggin as **Admin** or as **SuperAdmin**. SuperAdmin has access to a special menu in the NavBar. Once logged, navbar shows the navigation options and a button to logout and user name.  
**Razor pages** is used to render the info in the client browser. In the code behind files there is the logic to map the data from viewModel to model, send it to service and recive notification if necessary.  
There are some `Automapper` profiles defined to convert `viewModels` From or To `Models`. This layer shows ViewModels in the UI but sends Models to the service layer.  
In the <Pages/Shared> folder there is a partial view `_Notification` used to pass info between pages with a `Success`, `Info` or `Error` flag. These states are a enumartion defined in < Lang > project.  
Also there is defined a controller to manage the upload of images to a cloud service (Cloudinary). 


