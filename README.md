# SafePass

SafePass es una aplicación de consola desarrollada en C# para la administración segura de contraseñas. Permite a los usuarios almacenar y gestionar sus contraseñas de manera segura utilizando la base de datos SQLite. SafePass asegura que las contraseñas estén protegidas mediante encriptación, ofreciendo un enfoque sencillo y eficaz para la gestión de contraseñas.

Características
Almacenamiento seguro de contraseñas: SafePass encripta las contraseñas antes de guardarlas en la base de datos.
Verificación de usuario: La aplicación permite la creación de un único usuario y asegura que solo este usuario pueda acceder a las contraseñas almacenadas.
Operaciones básicas:
Crear usuario: Si no existe un usuario, la aplicación solicita un nombre de usuario y contraseña para crearlo.
Guardar contraseñas: Permite agregar nuevas contraseñas asociadas a servicios o aplicaciones.
Ver contraseñas: Acceso seguro para visualizar las contraseñas almacenadas.
Eliminar contraseñas: Posibilidad de eliminar las contraseñas que ya no sean necesarias.
Requisitos
.NET 6.0 SDK
SQLite (integrado en la aplicación)
Instalación
Clona este repositorio en tu máquina local:

sh
Copiar código
git clone https://github.com/CharlsHL/safepass.git
cd safepass
Restaura los paquetes NuGet:

sh
Copiar código
dotnet restore
Compila la aplicación:

sh
Copiar código
dotnet build
Ejecuta la aplicación:

sh
Copiar código
dotnet run
Uso
Crear un usuario:

Al ejecutar la aplicación por primera vez, se te pedirá que ingreses un nombre de usuario y una contraseña para crear un usuario único.
Este usuario será el único con acceso a la aplicación.
Agregar una contraseña:

Una vez autenticado, puedes agregar nuevas contraseñas para diferentes servicios.
Se te pedirá el nombre del servicio y la contraseña.
Ver contraseñas:

Puedes visualizar las contraseñas almacenadas para tus servicios.
Eliminar una contraseña:

Puedes eliminar una contraseña de la base de datos.
Manejo de Errores
Error de usuario duplicado: Si intentas crear un usuario con un nombre ya existente, la aplicación te notificará y no permitirá la duplicación de usuarios.
Excepciones de SQLite: La aplicación maneja errores de base de datos como fallos en la conexión o violaciones de restricciones únicas.
Seguridad
SafePass utiliza encriptación para almacenar las contraseñas de manera segura. Es importante mantener la contraseña de acceso a la aplicación en un lugar seguro, ya que es la clave para acceder a todas las contraseñas almacenadas.

Contribuciones
Las contribuciones son bienvenidas. Si encuentras un bug o tienes una idea para mejorar SafePass, siéntete libre de abrir un issue o enviar un pull request.

Licencia
Este proyecto está licenciado bajo la Licencia MIT.

Contacto
Para más información o preguntas sobre el proyecto, puedes contactarme a través de carlosermeti@hotmail.com

