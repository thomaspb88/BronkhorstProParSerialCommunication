namespace BronkhorstProParSerialCommunicationTest
{
    public class ProParResponse
    {
        public enum ResponseType
        {
            WriteSetpoint,
            Measure
        }

        public string Result { get; set; }
        public bool IsSuccess { get; set; }
    }
}
