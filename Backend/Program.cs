

namespace Backend
{
    class Program
    {
        static async Task Main(string[] args)
        {

            await Events.eventlistener.mqtt();
            
            // EXAMPLE how to add new user to database
            // await DBconnection.DBconnection.AddUserDB("David", "test1234"); // adding user to database
          //  await DBconnection.DBconnection.AddUserDB("Peter", "pan8311"); // adding user to database
            await DBconnection.CheckUserDB.DBconnection.AddUserDB("Peter","pan8311");
            // await DBconnection.DBconnection.AddUserDB("Cena", "wwwechamp"); // adding user to database
            
            //EXAMPLE how to check if password and username are correct / matching
           // Console.WriteLine(DBconnection.CheckUserDB.DBconnection.CheckUserDB("Peter", "wwwechamp")); //false
            // Console.WriteLine(DBconnection.DBconnection.CheckUserDB("Cena", "wwwechamp")); //true
            
            //backend is responsible for receiving data from the API and putting it in the database
            //frontend gets updated by the database passively and the backend directly for push notifications
        }
    }
}