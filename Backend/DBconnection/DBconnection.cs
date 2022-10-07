using Npgsql;


namespace Backend.DBconnection;

public static class DBconnection
{
    public static async Task ChengetaInserter(DateTime time, int nodeid, int latitude, int longitude, string soundtype, int probability, string soundfile)
    {
        string DBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
        var connString = DBconstring;

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();
         
         // Inserting data
         await using var cmd = new NpgsqlCommand("INSERT INTO chengeta.sounds(time, nodeid, latitude, longitude, soundtype, probability, soundfile) VALUES ($1, $2, $3, $4, $5, $6, $7)", conn)
         {
             Parameters =
             {
                 new() { Value = time },
                 new() { Value = nodeid },
                 new() { Value = latitude },
                 new() { Value = longitude },
                 new() { Value = soundtype },
                 new() { Value = probability },
                 new() { Value = soundfile }
             }
         };
         await using var reader = await cmd.ExecuteReaderAsync();
    }
    
    public static async Task AddUserDB(string username, string password)
    {
        string DBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
        var connString = DBconstring;

        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();
         
        // Inserting data
        await using var cmd = new NpgsqlCommand("INSERT INTO userdata.logins(name, password) VALUES ($1, $2)", conn)
        {
            Parameters =
            {
                new() { Value = username },
                new() { Value = password }
            }
        };
        await using var reader = await cmd.ExecuteReaderAsync();
    }
    
    public static bool CheckUserDB(string username, string password)
    {
        string DBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
        var connString = DBconstring;

        using var conn = new NpgsqlConnection(connString);
        conn.OpenAsync();
         
        // Reading data

        string query = "SELECT name,password FROM userdata.logins";

        using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
        {
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (username == reader["name"].ToString() && password == reader["password"].ToString())
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    
    
}