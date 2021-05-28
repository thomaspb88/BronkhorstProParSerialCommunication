using System;
using System.IO;
using System.IO.Ports;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using BronkhorstProParSerialCommunicationTest;

namespace BronkhorstConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            //var mfcPort = new BronkhorstSerialPort("COM10", 200);
            //var commands = new BronkhorstProParCommands(3);
            //mfcPort.Open();

            //var readMeasure = new BronkhorstProParCommand() { Command = commands.ReadMeasure(), Type = BronkhorstProParCommand.CommandType.Measure };
            //var readSetpoint = new BronkhorstProParCommand() { Command = commands.ReadSetPoint(), Type = BronkhorstProParCommand.CommandType.ReadSetpoint };
            //var writeSetpoint = new BronkhorstProParCommand() { Command = commands.WriteSetPoint(0.7f), Type = BronkhorstProParCommand.CommandType.WriteSetpoint };

            ////var response = mfcPort.SendCommand(writeSetpoint);
            ////Console.WriteLine(response.Result);
            //var response = mfcPort.SendCommand(readMeasure);
            //Console.WriteLine(response.Result);
            //response = mfcPort.SendCommand(readSetpoint);
            //Console.WriteLine(response.Result);

            //Console.ReadLine();

            //try
            //{
            //    var drives = Directory.GetLogicalDrives();
            //    var drive = System.IO.DriveInfo.GetDrives();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            //var result = NewMethod(@"V:\Passive\AA CABLES\Development\VoltageToCelcius.xlsx");
            throw
            var result = string.Empty;
            result = Path.GetDirectoryName(@"V:\Passive\AA CABLES\Development\VoltageToCelcius.xlsx");
            result = Path.GetPathRoot(@"V:\Passive\AA CABLES\Development\VoltageToCelcius.xlsx");
        }

        private static bool NewMethod(string path)
        {
            Func<bool> func = () => File.Exists(path);
            Task<bool> task = new Task<bool>(func);
            task.Start();
            if (task.Wait(100))
            {
                return true;
            }
            else
            {
                // Didn't get an answer back in time be pessimistic and assume it didn't exist
                return false;
            }
        }
    }
}
