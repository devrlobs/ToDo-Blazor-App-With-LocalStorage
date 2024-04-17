# ToDo-Blazor-App-With-LocalStorage
a simple to-do app using Microsoft ASP.NET Blazor with TypeScript and Tailwind CSS. Also using browser LocalStorage to store data.  Added service layer in the architecture and made sure to use proper componentization. 



1:48:27 - setting up home.razor

home.razor will be the only page that will directly access the service layer

stopped at 2:55:47 to modify class into record




to integrate typescript 
-go to wwwroot 
> scripts 
> then run command tsc --init (to generate tsconfig.json) 
> then modify config go to outdir and replace with location to dist folder 
>run tsc to compile ts to js


to integrate tailwindcss
-go to wwwroot
>css
>run command bunx tailwindcss init (to generate tailwind.config.js)
>modify that config accordingly (look for the documentation)
>run bunx tailwindcss -i app.css -o ../dist/css/app.css --watch




some notes:

1.   <TodoWrapper Items="TodoItems" OnChanged="OnTodoListStateChanged">

Items here is the parameter in the TodoWrapper
TodoItems here are the data from the parent component