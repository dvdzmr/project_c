using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Backend.Events;

public static class Eventlistener
{
    static MqttClient _mqttClient = new ("65.108.249.175");
    
    public static async Task Mqtt()
    {
        _mqttClient.MqttMsgPublishReceived += Mqtt_MsgPublishReceived;
        _mqttClient.Connect(Guid.NewGuid().ToString(), "chengeta2022", "chengetaALTENHR2022");
        Console.WriteLine("Subscriber: chengeta/notifications");
        _mqttClient.Subscribe(new string[] { "chengeta/notifications" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        if (_mqttClient.IsConnected)
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
        string latitude = "";
        string longitude = "";
        string soundtype = "";
        int probability = 0;
        string soundfile = "";
        string jsonStr = Encoding.UTF8.GetString(e.Message);
        Console.WriteLine(jsonStr);
        foreach (KeyValuePair<string, string> types in data)
        {
            // Console.WriteLine("Key: {0}, Value: {1}", types.Key, types.Value);
            if (types.Key == "time")
            {
                time = Int64.Parse(types.Value);
            }
            if (types.Key == "nodeId")
            {
                nodeId = Int64.Parse(types.Value);
            }
            if (types.Key == "latitude")
            {
                latitude = types.Value;
            }
            if (types.Key == "longitude")
            {
                longitude = types.Value;
            }
            if (types.Key == "sound_type")
            {
                soundtype = types.Value;
            }
            if (types.Key == "probability")
            {
                probability = Int32.Parse(types.Value);
            }
            if (types.Key == "sound")
            {
                soundfile = types.Value;
            }
        }
        DBconnection.CheckUserDB.DBconnection.ChengetaInserter(time,nodeId,latitude,longitude, soundtype,probability,soundfile); // using these types, you can easily add things to database
    }
}