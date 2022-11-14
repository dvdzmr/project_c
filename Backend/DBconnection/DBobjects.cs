namespace Backend.DBconnection.CheckUserDB;

public class DBobjects
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Soundtype { get; set; }
    public int Probability { get; set; }
    public string Soundfile { get; set; }
    // public string imgtype { get; set; }
}