using System.Data;
using Npgsql;

namespace Frontend.DBquery;

public class DBquery
{
    private static string _dBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
    
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
                        Longitude = Double.Parse(reader["longitude"].ToString()),
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