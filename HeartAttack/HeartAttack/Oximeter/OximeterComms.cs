using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;

namespace HeartAttack
{
    public class OximeterComms
    {
        public delegate void Activated ();

        public Activated OximeterActivated;

        public delegate void Deactivated();

        public Deactivated OximeterDeactivated;

        public delegate void Pulse(int newPulse);

        public Pulse OximeterPulse;

        SerialPort port = null;

        public bool Setup(string portName)
        {
            string [] names = SerialPort.GetPortNames();
            if (names.Length == 0)
                return false;

            if ( portName == "" )
                portName = names[0];

            bool found=false;

            foreach (string s in names)
                if (s.ToUpper() == portName.ToUpper()) found=true;

            if (!found)
                throw new Exception ("Port not available");

            port = new SerialPort(portName, 9600, System.IO.Ports.Parity.None, 8);

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            port.Open();

            return found;
        }

        public void Close()
        {
            port.Close();
            port.Dispose();
        }

        void processMessage ( char command, string message )
        {
            Console.WriteLine( "Command: " + command.ToString() + " " + message);

            switch (char.ToUpper(command))
            {
                case 'A':
                    if (OximeterActivated != null)
                    {
                        OximeterActivated();
                    }
                    break;

                case 'D':
                    if (OximeterDeactivated != null)
                    {
                        OximeterDeactivated();
                    }
                    break;

                case 'P':
                    if (OximeterPulse != null)
                    {
                        int messageVal = 0;

                        if (!int.TryParse(message, out messageVal))
                            return;

                        OximeterPulse(messageVal);
                    }
                    break;

            }
        }

        enum protState { awaitingStart, awaitingCommand, awaitingEnd };

        protState state;
        char command;
        StringBuilder message = new StringBuilder();

        void processChar(char ch)
        {
            switch (state)
            {
                case protState.awaitingStart:
                    if ( ch == '<' )
                    {
                        message.Clear();
                        state = protState.awaitingCommand;
                    }
                    break;

                case protState.awaitingCommand:
                    command = ch;
                    state = protState.awaitingEnd;
                    break;

                case protState.awaitingEnd:
                    if (ch == '>')
                    {
                        processMessage(command, message.ToString());
                        state = protState.awaitingStart;
                    }
                    else
                    {
                        message.Append(ch);
                    }
                    break;
            }
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string message = port.ReadExisting();
            foreach (char ch in message)
                processChar(ch);
        }

        public OximeterComms()
        {
        }
    }
}
