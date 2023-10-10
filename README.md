# fn-bank-account-simulator

## Instrucciones

1. Tener instalado SQL Server (SQLEXPRESS) para correr la base de datos local. Server type: Database Engine, Server name: (local)\SQLEXPRESS, Authentication: Windows Authentication.
   ![image](https://github.com/luissotelo96/fn-bank-account-simulator/assets/82717865/91bebbda-1c90-4b9a-a65d-90fdfab4226d)
2. Ejecutar el archivo DDL.sql que se encuentra adjunto en el repositorio. Luego, verificar que se hayan creado todos los objetos en la base de datos, así mismo importante que se hayan creado los registros de fábrica para las tablas CustomerType y ProductType.
3.  Abrir la solución en el Visual Studio, limpiar, compilar y ejecutar. Cada una de las api expuestas están escuchando por el puerto 7284. Si desea cambiar este puerto, lo puede hacer desde el archivo local.settings: 
    ![image](https://github.com/luissotelo96/fn-bank-account-simulator/assets/82717865/653d9a46-9d99-4448-98aa-7040b2865760)

    (Tener en cuenta que Visual Studio debe haberse instalado con la opción "Desarrollo en Azure" para poder correr el proyecto. Así mismo, si se modifica el puerto, este también debe ser modificado en el environmet del proyecto del Frontend).
