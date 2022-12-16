using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Npgsql;

namespace Frontend.DBquery;

public class DBquery
{
    private static string _dBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
    
    public static List<DBobjects> DbChecker()
    {
        // get computer cultureinfo
        var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        
        string query = "SELECT time,latitude, longitude, soundtype, probability, soundfile, status FROM chengeta.sounds";
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
                        Latitude = Convert.ToDouble(Regex.Replace(reader["latitude"].ToString(), "[.,]", separator)),
                        Longitude = Double.Parse(Regex.Replace(reader["longitude"].ToString(), "[.,]", separator)),
                        Soundtype = reader["soundtype"].ToString(),
                        Probability = int.Parse(reader["probability"].ToString()),
                        Soundfile = reader["soundfile"].ToString(),
                        Status = reader["status"].ToString()
                    }
                );
                // Console.WriteLine("test: lat:{0}, long:{1}", Convert.ToDouble(reader["latitude"].ToString()), Double.Parse(reader["longitude"].ToString()));
                i++;
            }
            return allevent;
        }
    }
}