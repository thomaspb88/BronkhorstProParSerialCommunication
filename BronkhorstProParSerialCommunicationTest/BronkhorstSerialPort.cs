using System;
using System.IO.Ports;
using System.Threading;

namespace BronkhorstProParSerialCommunicationTest
{
    public class BronkhorstSerialPort
    {
        private int _timeout;
        private SerialPort _mfcPort;

        private AutoResetEvent _respondNow;

        public BronkhorstSerialPort(string portName, int timeOut)
        {
            _timeout = timeOut;
            _mfcPort = new SerialPort()
            {
                PortName = portName,
                BaudRate = 38400,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                ReadTimeout = _timeout,
                WriteTimeout = _timeout
            };
        }

        public bool Open()
        {
            try
            {
                if(_mfcPort != null && !_mfcPort.IsOpen)
                {
                    _respondNow = new AutoResetEvent(false);
                    _mfcPort.Open();
                    _mfcPort.DataReceived += _mfcPort_DataReceived;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }

        public bool Close()
        {
            try
            {
                if (_mfcPort != null && _mfcPort.IsOpen)
                {
                    _mfcPort.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void _mfcPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    _respondNow.Set();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProParResponse SendCommand(BronkhorstProParCommand command)
        {
            _mfcPort.DiscardOutBuffer();
            _mfcPort.DiscardInBuffer();
            _respondNow.Reset();
            _mfcPort.Write(command.Command);

            string response = ReadResponse();

            return ParseResponse(response, command.Type);

            //return new ProParResponse() { IsSuccess = true, Result = response };
        }

        private string ReadResponse()
        {
            string response = string.Empty;
            try
            {
                if (_respondNow.WaitOne(_timeout,false))
                {
                    response = _mfcPort.ReadExisting();
                }
            }
            catch
            {
                response = string.Empty;
            }
            return response;
        }

        private ProParResponse ParseResponse(string input, BronkhorstProParCommand.CommandType command)
        {
            switch (command)
            {
                case BronkhorstProParCommand.CommandType.WriteSetpoint:
                    return null;
                case BronkhorstProParCommand.CommandType.ReadSetpoint:
                case BronkhorstProParCommand.CommandType.Measure:
                    return new ProParResponse() { IsSuccess = true, Result = ConvertHexToSingle(input.Substring(11, 8)).ToString() };
                default:
                    return new ProParResponse() { IsSuccess = false, Result = null };
            }
        }

        public float ConvertHexToSingle(string hexString)
        {
            var i = Convert.ToInt32(hexString, 16);
            var bytes = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
