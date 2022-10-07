

namespace Backend
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            // EXAMPLES FOR USING DATABASE:
            
            // DateTime time = DateTime.Now;
            // int nodeid = 10;
            // int latitude = 100000;
            // int longitude = 90000;
            // string soundtype = "Gun";
            // int probability = 50;
            // string soundfile = "www.sound.com";
            // await DBconnection.DBconnection.ChengetaInserter(time,nodeid,latitude,longitude, soundtype,probability,soundfile); // using these types, you can easily add things to database
            //
            // await DBconnection.DBconnection.AddUserDB("David", "test1234"); // adding user to database
            // await DBconnection.DBconnection.AddUserDB("Peter", "pan8311"); // adding user to database
            // await DBconnection.DBconnection.AddUserDB("Cena", "wwwechamp"); // adding user to database
            //
            // Console.WriteLine(DBconnection.DBconnection.CheckUserDB("Peter", "wwwechamp")); //false
            // Console.WriteLine(DBconnection.DBconnection.CheckUserDB("Cena", "wwwechamp")); //true
            
            
            //backend is responsible for receiving data from the API and putting it in the database
            //frontend gets updated by the database passively and the backend directly for push notifications
        }
    }
}