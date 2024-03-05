# Proyecto APIRESTful para CRUD de Películas

Este proyecto consiste en la creación de un Api restful para gestionar peliculas usando el framework de .NET. La API permite realizar las siguientes operaciones Crear,Leer,Actualizar y Eliminar (Post,Get,Put y Delete)

### Paso a Paso para uso el API en tu PC

1. Creamos el database en MySql 
    Usamos el archivo peliculaApi.sql, contiene el codigo para la creación del Database y de la tabla peliculas. Después insertamos los datos para posteriormente utilizarlo en el CRUD

2. Tienes que ir a API/API.csproj/appsettings.Development.json y modificar la        conexión a tu base de datos, verifica: server,user,password y database(si cambiaste el nombre)
    ```sql
        "MySqlConex": "server=localhost;user=root;password=TUCLAVE;database=peliculaapi;"
    ```
    Tienes que ir a API/API.csproj/appsettings.json y modificar la conexión a tu base de datos, verifica: server,user,password y database(si cambiaste el nombre)
    ```sql
        "MySqlConex": "server=localhost;user=root;password=TUCLAVE;database=peliculaapi;"
    ```

### Swagger

Dado el caso quieres usar el Swagger para proba tu aplicación, debes utilizar el siguiente codigo

```dotnet
 dotnet watch run --project ./API/
```

Ahí podras encontrar los metodos como: GET,PUT,DELETE,GET{id}.