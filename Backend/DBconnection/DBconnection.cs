using System.Data;
using System.Security.Cryptography;
using Npgsql;

namespace Backend.DBconnection.CheckUserDB;

public static class DBconnection
{
    private static string DBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
    
    public static async Task ChengetaInserter(long timeEpoch, long nodeid, double latitude, double longitude, string soundtype, int probability, string soundfile)
    {
        await using var conn = new NpgsqlConnection(DBconstring);
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
    
    public static async Task AddUserDB(string username, string password)
    {
        string encryptedpass = encryption.encryptor(password); // stores password with SHA256 encryption to database.
        await using var conn = new NpgsqlConnection(DBconstring);
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
    
    
    public static bool CheckUserDB(string username, string password)
    {
        
        string query = "SELECT name,password FROM userdata.logins";
        string encryptedpass = encryption.encryptor(password); // Hashes work only 1 way, so we convert the given password to see if the hash matches the saved hash.
        
        using var conn = new NpgsqlConnection(DBconstring);
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

    public static List<DBobjects> DBChecker()
    {
        string query = "SELECT time,latitude, longitude, soundtype, probability, soundfile FROM chengeta.sounds";
        
        using var conn = new NpgsqlConnection(DBconstring);
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
                        id = i,
                        time = DateTime.Parse(reader["time"].ToString()),
                        latitude = Double.Parse(reader["latitude"].ToString()),
                        longitude = Double.Parse(reader["latitude"].ToString()),
                        soundtype = reader["soundtype"].ToString(),
                        probability = int.Parse(reader["probability"].ToString()),
                        soundfile = reader["soundfile"].ToString()
                    }
                );
                i++;
            }

            return allevent;
            // foreach (var item in allevent)
            // {
            //     if (item.soundtype == "animal")
            //     {
            //         item.imgtype = "animal.jpg";
            //     }
            //     else if (item.soundtype == "gun")
            //     {
            //         item.imgtype = "gun.jpg";
            //     }
            //     else if (item.soundtype == "vehicle")
            //     {
            //         item.imgtype = "vehicle.jpg";
            //     }
            //     else if (item.soundtype == "thunder")
            //     {
            //         item.imgtype = "thunder.jpg";
            //     }
            //     else
            //     {
            //         item.imgtype = "unknown.jpg";
            //     }
            // }
        }
    }
}