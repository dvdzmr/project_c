using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Npgsql;
using NpgsqlTypes;

namespace Frontend.DBquery;

public class DBquery
{
    private static string _dBconstring = "Host=localhost;Username=postgres;Password=postgres;Database=postgres"; // This will be hidden in the future in ENV / dotnet secrets.
    
    public static List<DBobjects> DbChecker()
    {
        // get computer cultureinfo
        var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        
        string query = "SELECT id, time, latitude, longitude, soundtype, probability, soundfile, status FROM chengeta.sounds ORDER BY id";
        using var conn = new NpgsqlConnection(_dBconstring);
        if (conn.State != ConnectionState.Open && conn.State != null)
        {
            conn.Close();
        }
        conn.Open();
        
        var allevent = new List<DBobjects>();
        using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
        {
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                allevent.Add(new DBobjects()
                    {
                        Id = int.Parse(reader["id"].ToString()),
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
            }
            return allevent;
        }
    }

    public static List<DBobjects> DbStatusPush(int pkey, string tochange)
    {
        Console.WriteLine(pkey+ tochange);
        string query = "";
        if (tochange == "Not started")
        {
            query = "UPDATE chengeta.sounds SET status = 'In progress' WHERE id = "+pkey ;
        }
        else if (tochange == "In progress")
        {
            query = "UPDATE chengeta.sounds SET status = 'Completed' WHERE id = "+pkey ;
        }
        else
        {
            query = "UPDATE chengeta.sounds SET status = 'Not started' WHERE id = "+pkey ;
        }
        
        using var conn = new NpgsqlConnection(_dBconstring);
        if (conn.State != ConnectionState.Open && conn.State != null)
        {
            conn.Close();
        }
        conn.Open();
        NpgsqlCommand command = new NpgsqlCommand(query, conn);
        command.ExecuteNonQuery();
        conn.Close();
        // command.Parameters.Add(new NpgsqlParameter("tochange", NpgsqlTypes.NpgsqlDbType.Text));
        // command.Parameters[0].Value = tochange.Text;
        // NpgsqlParameter p = new NpgsqlParameter("pkey", NpgsqlDbType.Integer);
        // p.Value = pkey;
        // command.Parameters.Add(p);
        return null;
    }
}