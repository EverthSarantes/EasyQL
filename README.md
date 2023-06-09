# **EasyQL**
**EasyQL** es una biblioteca de C# que simplifica la realización de consultas SQL en bases de datos.
## **Características**
- Proporciona metodos sencillos y fáciles de usar para ejecutar consultas SQL.
- Admite conexiones a bases de datos utilizando la autenticación de Windows y autenticación SQL.
- Utiliza modelos para mapear las tablas de tu base de datos.
## **Instalación**
1. Descarga la libreria en nuget.
## **Uso**
1. Importa el espacio de nombres siempre que sea necesario: 

    `using EasyQL;`

1. Conexiones

EasyQL proporsiona utilidades para crear conexiones con la base de datos, para realizar una conexion con la autentificación de SQL haz lo siguiente:

```csharp
using EasyQL;
//Añadimos los datos de la base de datos:
Conection.server = "nombre_servidor";
Conection.dataBase = "nombre_baseDatos";
Conection.user = "usuario";
Conection.password = "contraseña";

Conection.makeConnection(Conection.connectionString());
```

Conexión con autenticación de windows:
```csharp
using EasyQL;
//Añadimos los datos de la base de datos:
Conection.server = "nombre_servidor";
Conection.dataBase = "nombre_baseDatos";

Conection.makeConnection(Conection.winConectionString());
```


En ambos casos podemos acceder a la conexión creada atraves de: Conection.Con y abrimos la conexión de la siguiente manera: Conection.Con.Open()

1. Modelos

EasyQL utiliza modelos para mapear las tablas de tu base de datos, para crear un modelo hay que hacer un nuevo archivo de c# y hacer los siquiente:

```csharp
    //Importamos la libreria de SQL
    using Microsoft.Data.SqlClient;
    //Utilizamos el namespace de EasyQl
    namespace EasyQL
    {
        public class Estudiante : Model //Creamos una clase que herede de Model
        {
            //Creamos un constructor con un parametro tipo SqlConnection
            public Estudiante(SqlConnection conection) : base(conection)
            {
                //Opcionalmente podemos añadir los campos de la tabla utilizando Fields.Add();
                Fields.Add("nombres");
                Fields.Add("apellidos");
                Fields.Add("telefono");
                //Agregamos el nombre de la tabla
                Table = "Estudiante";
                //Opcionalmente podemos utilizar el campo para definir la primary key de la tabla
                //Este paso no es obligatorio hacerlo, por defecto la primary key es id
                Identifier = "id";
            }
        }
    }
```
Una vez creado podemos utilizar el modelo importanto la libreria e instanciando el modelo
```csharp
//Importamos la libreria
using EasyQL;
//Instanciamos el modelo y le pasamos como parametro uno conexi�n activa
Estudiante estudiante = new Estudiante(Conection.Con);
```

1. Métodos de los modelos

El modelo proporciona multiples métodos para realizar consultas SQL a la tabla que le designamos
##### **Métodos SELECT**
Todos los metodos SELECT devuelven un objeto del tipo SqlDataReader y ocupan una lista de string con los valores que queremos de la tabla
###### **Método find**
El método find devuelve un solo registro, que busca con la primary key y el valor que le pasamos como parametro

```csharp
//método find, pasamos un string como parametro y las lista con los valores, si se necesitan todos no se pasa ning�n parametro
estudiante.find("1", new List { "nombres" }); //retorna solo el nombre del estudiante
estudiante.find("1"); //retorna todos los campos del estudiante
```

###### **Método all**
El método all devuelve todos los registros de la tabla

```csharp
//método all
estudiante.all(); //retorna todos los campos de los estudiantes
estudiante.all(new List { "nombres", "apellidos" }); //retorna solo el nombre y apellido de los estudiantes
```

###### **Método where**
El método where busca los registros según los parametros que le pasemos

```csharp
//método where, como primer parametro pasamos el campo a buscar, segundo pasamos el comparador y tercero el valor a comparar
estudiante.where("nombres", "=", "Juan", new List { "nombres", "apellidos" }); //retorna solo el nombre y apellido del estudiante
estudiante.where("nombres", "=", "Juan"); //retorna todos los campos del estudiante
```

##### **Método INSERT**
El método insert devuelve un int con la cantidad de registros afectados

Inserta en la tabla según los campos que añadimos, ocupa como parametro dos listas de strings con los valores a insertar y los campos

```csharp
List<string> fields = new List<string>
{
    "nombres",
    "apellidos",
    "telefono",
};
//Creamos una lista con los valores
List<string> values = new List<string>
{
    "Juan",
    "Sanchez",
    "88888888",
};
//pasamos como parametro las listas
estudiante.insert(fields, values);
```

##### **Métodos Delete**
Todos los metodos Delete devuelven un int con los registros afectados
###### **método delete**
El método delete elimina registros, busca con la primary key y el valor que le pasamos como parametro

```csharp
//Método delete, pasamos un string como parametro
estudiante.delete("1");
```

###### **Método deleteWhere**
El método deleteWhere elimina registros seg�n los parametros que le pasemos

```csharp
//Método deleteWhere, primero pasamos el campo a buscar, segundo el comparador y tercero el valor a comparar
estudiante.delete("nombres", "=", "Juan");
```

##### **Método Update**
El método Update devuelve un int con los registros afectados

Actualiza registros, busca con la primary key y el valor que le pasamos como parametro, requiere una lista bidimensional de strings

```csharp
//Creamos una lista bidimensional con los campos y con los valores a actualizar
List<List<string>> info = new List<List<string>>
{
    new List<string>() { "nombres", nombreTxt.Text },
    new List<string>() { "apellidos", apellidosTxt.Text },
    new List<string>() { "telefono", telefonoTxt.Text },
};

//pasamos la lista como primer parametro y como segundo el id del registro a actualizar
estudiante.update(info, "1");
```

## **Contribuciones**
Las contribuciones son bienvenidas. Si deseas mejorar o agregar nuevas características al proyecto, puedes realizar un fork del repositorio, realizar tus cambios en una rama y enviar un pull request.
