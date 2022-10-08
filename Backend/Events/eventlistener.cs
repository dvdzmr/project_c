using System.Text;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Backend.Events;

public static class eventlistener
{
    static MqttClient mqttClient = new ("65.108.249.175");
    
    public static async Task mqtt()
    {
        mqttClient.MqttMsgPublishReceived += Mqtt_MsgPublishReceived;
        mqttClient.Connect(Guid.NewGuid().ToString(), "chengeta2022", "chengetaALTENHR2022");
        Console.WriteLine("Subscriber: chengeta/notifications");
        mqttClient.Subscribe(new string[] { "chengeta/notifications" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        if (mqttClient.IsConnected)
        {
            Console.WriteLine("MQTT broker is connected");
        }
        
    }
    
    static void Mqtt_MsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Console.WriteLine("executing");
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(Encoding.ASCII.GetString(e.Message));
        long time = 0;
        long nodeId = 0;
        double latitude = 0;
        double longitude = 0;
        string soundtype = "";
        int probability = 0;
        string soundfile = "";
        foreach (KeyValuePair<string, string> Types in data)
        {
            if (Types.Key == "time")
            {
                time = Int64.Parse(Types.Value);
            }
            if (Types.Key == "nodeId")
            {
                nodeId = Int64.Parse(Types.Value);
            }
            if (Types.Key == "latitude")
            {
                latitude = Convert.ToDouble(Types.Value);
            }
            if (Types.Key == "longitude")
            {
                longitude = Convert.ToDouble(Types.Value);
            }
            if (Types.Key == "sound_type")
            {
                soundtype = Types.Value;
            }
            if (Types.Key == "probability")
            {
                probability = Int32.Parse(Types.Value);
            }
            if (Types.Key == "sound")
            {
                soundfile = Types.Value;
            }
        }
        DBconnection.DBconnection.ChengetaInserter(time,nodeId,latitude,longitude, soundtype,probability,soundfile); // using these types, you can easily add things to database
    }
}