# Login-api-in

Para Probar realizar los siguientes pasos.

1. Verifique que la Base de datos esta creada. hay un archivo llamado DBSCRIPT.sql, corra dicho archivo en el motor de base de datos de MYSQL
2. Entre en el archivo APPSETTINGS dentro del proyecto LOGINAPI, y cambie el connectionString con las caracteristicas del servidor mysql, en el mismo encontrara el apartado para modificar el REGEX de verificacion de la contraseña
3. Haga un dotnet restore, y luego un dotnet run, y abra la ruta, tiene SWAGGER, puede acceder agregando "/swagger" al final de la ruta que aparece cuando lanza el comando dotnet run.
4. Dentro de la carpeta del proyecto LOGIN API, se encuentra un archivo llamado [REQUEST](LoginAPI/requests.http) en este se encuentra las pruebas iniciales de la api, ahi puede cambiar el contenido de los elementos body de los requests y asi probar la API. Recuerde cambiar las rutas que estan ese archivo, si se genera un localhost diferente.