namespace Frontend.Models;

public class Map{

    public int Id { get; set; }
    public DateTime Time { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Soundtype { get; set; }
    public int Probability { get; set; }
    public string Soundfile { get; set; }
    // public string imgtype { get; set; }
}
// public class Map
// {
//     public List<Test> mylist { get; set; }
// }