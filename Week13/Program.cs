
using System.Data.SQLite;

ReadD(CreateConnection());
InsertCustomer(CreateConnection());
RemoveCustomer(CreateConnection());
static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection= new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True");

    try
    {
        connection.Open();
        Console.WriteLine("Db found.");
    }
    catch
    {
        Console.WriteLine("DB not found");
    }

    return connection;
}

static void ReadD(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, firstName, lastName, dateOfBirth FROM customer";

    try
    {
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            string readerRowId = reader["rowid"].ToString();
            string readerFirstName = reader["firstName"].ToString();
            string readerLastName = reader["lastName"].ToString();
            string readerDoB = reader["dateOfBirth"].ToString();

            Console.WriteLine($"{readerRowId}. Full name: {readerFirstName} {readerLastName}; DoB: {readerDoB}");
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error reading data: " + ex.Message);
    }

    myConnection.Close();
}
// Videos olev kood lõi rowid lisamises mingil põhjusel errori, seega tegin veidi omaloomingut
static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, DoB;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth");
    DoB = Console.ReadLine();  

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
    $"VALUES ('{fName}', '{lName}', '{DoB}') ";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($" Row inserted: {rowInserted}");


    ReadD(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an ID to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete} ";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer");

    ReadD(myConnection);
}