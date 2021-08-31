namespace MegaTFLT.Models.MQ
{
    public class MqModel
    {
        public string MqManagerName {get; set;}
        public string MqServerIp {get; set;}
        public int MqServerPort {get; set;}
        public string MqChannelName {get; set;}
        public string MqManagerCcsId {get; set;}
        public int Encoding {get; set;}
        public string ApplicationName {get; set;}
        public int CharacterSet {get; set;}
        public string LocalQueueName {get; set;}
        public string RemoteQueueName {get; set;}
        public string ErrorQueueName {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}

    }
}