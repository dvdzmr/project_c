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
    
    
}