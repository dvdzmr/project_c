using System.Data;
using Npgsql;

namespace Backend.DBconnection.CheckUserDB;

public static class DBconnection
{
    private static string _dBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
    
    public static async Task ChengetaInserter(long timeEpoch, long nodeid, double latitude, double longitude, string soundtype, int probability, string soundfile)
    {
        await using var conn = new NpgsqlConnection(_dBconstring);
        await conn.OpenAsync();
        
        // convert epoch/unix timestamp to Datetime
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeEpoch);
        DateTime time = dateTimeOffset.UtcDateTime;
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
    
    public static async Task AddUserDb(string username, string password)
    {
        string encryptedpass = Encryption.Encryptor(password); // stores password with SHA256 encryption to database.
        await using var conn = new NpgsqlConnection(_dBconstring);
        await conn.OpenAsync();

        // Inserting data
        await using var cmd = new NpgsqlCommand("INSERT INTO userdata.logins(name, password) VALUES ($1, $2)", conn)
        {
            Parameters =
            {
                new() { Value = username },
                new() { Value = encryptedpass }
            }
        };
        await using var reader = await cmd.ExecuteReaderAsync();
    }
    
    
    public static bool CheckUserDb(string username, string password)
    {
        
        string query = "SELECT name,password FROM userdata.logins";
        string encryptedpass = Encryption.Encryptor(password); // Hashes work only 1 way, so we convert the given password to see if the hash matches the saved hash.
        
        using var conn = new NpgsqlConnection(_dBconstring);
        conn.OpenAsync();
         
        // Reading data
        using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
        {
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (username == reader["name"].ToString() && encryptedpass == reader["password"].ToString())
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    public static List<DBobjects> DbChecker()
    {
        string query = "SELECT time,latitude, longitude, soundtype, probability, soundfile FROM chengeta.sounds";
        
        using var conn = new NpgsqlConnection(_dBconstring);
        if (conn.State != ConnectionState.Open && conn.State != null)
        {
            conn.Close();
        }
        conn.Open();
        
        var allevent = new List<DBobjects>();
        using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
        {
            int i = 0;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                allevent.Add(new DBobjects()
                    {
                        Id = i,
                        Time = DateTime.Parse(reader["time"].ToString()),
                        Latitude = Double.Parse(reader["latitude"].ToString()),
                        Longitude = Double.Parse(reader["latitude"].ToString()),
                        Soundtype = reader["soundtype"].ToString(),
                        Probability = int.Parse(reader["probability"].ToString()),
                        Soundfile = reader["soundfile"].ToString()
                    }
                );
                i++;
            }
            return allevent;
        }
    }
}