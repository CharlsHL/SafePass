using SafePass.Negocio;
using SafePass.Seguridad;
using SQLitePCL;

class Program
{
    private const string DatabaseFile = "passwords.db";
    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenido a SafePass de gestión de contraseñas.");
        // Inicializar el proveedor de SQLite
        Batteries.Init();
        // Configura la base de datos si es la primera vez
        ContextoDB.SetupDatabase(DatabaseFile);

        // Solicita login
        if (!Seguridad.Login(DatabaseFile))
        {
            Console.WriteLine("Acceso denegado.");
            return;
        }
        Console.Clear();
        // Bucle principal
        string input;
        do
        {
            Console.Write("Ingrese un comando (cls,add, list, delete, exit): ");
            input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "add": 
                    ContextoDB.AddPassword(DatabaseFile);
                    break;
                case "list":
                    ContextoDB.ListPasswords(DatabaseFile);
                    break;
                case "delete":
                    ContextoDB.DeletePassword(DatabaseFile);
                    break;
                case "exit":
                    Console.WriteLine("Saliendo...");
                    break;
                case "cls":
                    Console.WriteLine("Limpiando...");
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Comando no reconocido.");
                    break;
            }
        } while (input.ToLower() != "exit");
    }
}
