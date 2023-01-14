using System.Data;
using Npgsql;

namespace Backend.DBconnection.CheckUserDB;

public static class DBconnection
{
    private static string _dBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.

    public static async Task ChengetaInserter(long timeEpoch, long nodeid, string latitude, string longitude, string soundtype, int probability, string soundfile)
    {
        var id = idCounter();
        await using var conn = new NpgsqlConnection(_dBconstring);
        await conn.OpenAsync();
        
        // convert epoch/unix timestamp to Datetime
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timeEpoch);
        DateTime time = dateTimeOffset.UtcDateTime;
        time.AddHours(1);
        
        // soundtype first letter uppercase
        soundtype = TopUpperFirstLetter(soundtype);
        
         // Inserting data
         await using var cmd = new NpgsqlCommand("INSERT INTO chengeta.sounds(id,time, nodeid, latitude, longitude, soundtype, probability, soundfile, status) VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9)", conn)
         {
             Parameters =
             {
                 new() {Value = id},
                 new() { Value = time },
                 new() { Value = nodeid },
                 new() { Value = latitude },
                 new() { Value = longitude },
                 new() { Value = soundtype },
                 new() { Value = probability },
                 new() { Value = soundfile },
                 new() { Value = "Not started"}
             }
         };
         await using var reader = await cmd.ExecuteReaderAsync();
    }
    public static int idCounter()
    {
        try
        {
            string query = "SELECT count(*) FROM chengeta.sounds";
            using var conn = new NpgsqlConnection(_dBconstring);
            if (conn.State != ConnectionState.Open && conn.State != null)
            {
                conn.Close();
            }

            conn.Open();
            
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            var getcount = command.ExecuteScalar();
            conn.Close();
            // Console.WriteLine(getcount);
            return int.Parse(getcount.ToString())+ 1 ;
        }
        catch
        {
            return 1;
        }
    }

    public static string TopUpperFirstLetter(string a)
    {
        if (string.IsNullOrEmpty(a))
            return a;
        // convert to char array of the string
        char[] letters = a.ToCharArray();
        // upper case the first char
        letters[0] = char.ToUpper(letters[0]);
        // return the array made of the new char array
        return new string(letters);
    }
    
}