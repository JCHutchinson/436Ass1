using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ChatClient
{
    class Client2
    {
        private String message;
        private string serverIP;
        private IPAddress ipaddr;
        private StreamWriter output;
        public StreamReader input;
        private TcpClient connection;
        private bool Connected;

        //constructor
        public Client2(String ip)
        {
            serverIP = ip;
        }

        public void startRunning()
        {

            System.Diagnostics.Debug.WriteLine("We in at start running");

            try
            {
                connectToServer();
                setupStreams();
                System.Diagnostics.Debug.WriteLine("Setting up streams was successful");

                //whileChatting();

            }
            catch (IOException socketException)
            {
                System.Diagnostics.Debug.WriteLine("Could not connect to Server");
                Environment.Exit(1);
            }

        }

        public void connectToServer()
        {

            string ipAdress = "127.0.0.1";
            ipaddr = IPAddress.Parse(ipAdress);

            connection = new TcpClient();
            connection.Connect(ipaddr, 44001);

            Connected = true;
        }

        public void setConnected(bool set)
        {
            Connected = set;
        }

        public bool getConnected()
        {
            return Connected;
        }

        public void setupStreams()
        {
            NetworkStream ns = connection.GetStream();

            output = new StreamWriter(ns);

            input = new StreamReader(ns);

            //NetworkStream outstream = new NetworkStream(connection);
            //NetworkStream instream = new NetworkStream(connection);
            //System.Diagnostics.Debug.WriteLine("Made it here");


            //output = new BinaryWriter(outstream);
            //System.Diagnostics.Debug.WriteLine("Made it here2");

            //output.Flush();
            //System.Diagnostics.Debug.WriteLine("Made it here3");

            //input = new BinaryReader(instream);
            //System.Diagnostics.Debug.WriteLine("Made it here4");

        }


        public void whileChatting()        


        {
            System.Diagnostics.Debug.WriteLine("We are in chatting");

            output.Write("We therre");
            output.Flush();

            System.Diagnostics.Debug.WriteLine("We are in chatting again");
        }

        public void sendMessage(string message) {

            output.WriteLine(message);
            output.Flush();
        }

        public string readMessage()
        {

            //try {
            //    message = input.ToString();

            //} catch (NullReferenceException nullReference) {
            //    System.Diagnostics.Debug.WriteLine("Input reference is null");
            //}
            //return message;

            try {
                message = input.ReadLine();
                return message;
            }catch(IOException ioerror)
            {
                System.Diagnostics.Debug.WriteLine("final error from ");
                return "value is null from client2";
            }

            return "not quite";

        }

        public void closeConnection()
        {
            connection.Close();
        }

    }
}
