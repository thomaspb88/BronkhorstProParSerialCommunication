using System;

namespace BronkhorstProParSerialCommunicationTest
{
    public class BronkhorstProParCommands
    {
        private const int MAXIMUM_SETPOINT_VALUE = 32000;
        private string _nodeAddress;

        public BronkhorstProParCommands(int nodeAddress)
        {
            _nodeAddress = nodeAddress.ToString("00");
        }
        public string ReadSetPoint()
        {
            return $":06{_nodeAddress}0421412143\r\n";
        }

        public string ReadMeasure()
        {
            return $":06{_nodeAddress}0421402140\r\n";
        }

        public string WriteSetPoint(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            var i = BitConverter.ToInt32(bytes, 0);
            var hexValue = i.ToString("X8");
            return $":08{_nodeAddress}012143{hexValue}\r\n";
        }
    }
}
