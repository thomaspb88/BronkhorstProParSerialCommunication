namespace BronkhorstProParSerialCommunicationTest
{
    public class BronkhorstProParCommand
    {
        public enum CommandType
        {
            WriteSetpoint,
            ReadSetpoint,
            Measure
        }

        public string Command { get; set; }
        public CommandType Type { get; set; }
    }
}
