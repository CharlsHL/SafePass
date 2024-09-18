using Microsoft.Data.Sqlite;
using SafePass.Seguridad;

namespace SafePass.Negocio
{
    public static  class ContextoDB
    {
        public static void SetupDatabase(string DatabaseFile)
        {
            // No es necesario verificar ni crear el archivo manualmente.
            // Al abrir la conexión, el archivo se creará automáticamente si no existe.
            using (var connection = new SqliteConnection($"Data Source={DatabaseFile}"))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY,
                    Username TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Passwords (
                    Id INTEGER PRIMARY KEY,
                    Service TEXT NOT NULL,
                    Username TEXT NOT NULL,
                    Password TEXT NOT NULL
                );";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Crea un usuario administrador por defecto si no existe
                CreateUser("admin", "admin123", connection);
            }
        }

        public  static void CreateUser(string username, string password, SqliteConnection connection)
        {
            // Primero, verifica si el usuario ya existe
            string checkUserQuery = "SELECT COUNT(*) FROM Users";
            using (var checkCommand = new SqliteCommand(checkUserQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@username", username);
                long userCount = (long)checkCommand.ExecuteScalar();

                // Si el usuario no existe, solicita el nombre de usuario y contraseña y luego intenta insertarlo
                if (userCount == 0)
                {
                    Console.WriteLine("Ingrese un usuario y contraseña:");
                    Console.Write("Username: ");
                    string newUsername = Console.ReadLine(); // Usa una variable local para evitar sobrescribir el parámetro de entrada
                    Console.Write("Password: ");
                    string newPassword = Seguridad.Seguridad.ReadPassword();

                    string hashedPassword = Seguridad.Seguridad.HashPassword(newPassword);
                    string insertUserQuery = "INSERT INTO Users (Username, PasswordHash) VALUES (@username, @passwordHash);";
                    try
                    {
                        using (var insertCommand = new SqliteCommand(insertUserQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@username", newUsername);
                            insertCommand.Parameters.AddWithValue("@passwordHash", hashedPassword);
                            insertCommand.ExecuteNonQuery();
                        }
                        Console.WriteLine("Usuario creado exitosamente.");
                    }
                    catch (SqliteException ex)
                    {
                        // Maneja el caso en que el usuario ya existe en otro contexto
                        Console.WriteLine("Error al crear el usuario: " + ex.Message);
                    }
                }
            }
        }

        public static void AddPassword(string DatabaseFile)
        {
            Console.Write("Service: ");
            string service = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Seguridad.Seguridad.ReadPassword();

            using (var connection = new SqliteConnection($"Data Source={DatabaseFile}"))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Passwords (Service, Username, Password) VALUES (@service, @username, @password);";
                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@service", service);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", Seguridad.Seguridad.EncryptPassword(password));
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Contraseña guardada exitosamente.");
        }

        public static void ListPasswords(string DatabaseFile)
        {
            using (var connection = new SqliteConnection($"Data Source={DatabaseFile}"))
            {
                connection.Open();
                string selectQuery = "SELECT Service, Username, Password FROM Passwords;";
                using (var command = new SqliteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Contraseñas guardadas:");
                    while (reader.Read())
                    {
                        string service = reader.GetString(0);
                        string username = reader.GetString(1);
                        string password = Seguridad.Seguridad.DecryptPassword(reader.GetString(2));

                        Console.WriteLine($"Service: {service}, Username: {username}, Password: {password}");
                    }
                }
            }
        }

        public static void DeletePassword(string DatabaseFile)
        {
            Console.Write("Servicio a borrar: ");
            string service = Console.ReadLine();

            using (var connection = new SqliteConnection($"Data Source={DatabaseFile}"))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Passwords WHERE Service = @service;";
                using (var command = new SqliteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@service", service);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Contraseña eliminada.");
        }

    }
}
