using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client2 pet = new Client2("127.0.0.1");
        string inputMessage = "";

        public MainWindow()
        {
            InitializeComponent();
            readMessage2();

        }

        private void connectToServer(object sender, RoutedEventArgs e)
        {

            ChatOutput.Text += "\n Connecting You to the server at address: 127.0.0.1";

            try
            {
                pet.connectToServer();
                ChatOutput.Text += "\n Welcome You are in";
                pet.setupStreams();
                ChatOutput.Text += "\n You can now talk";

                pet.setConnected(true);
                System.Diagnostics.Debug.WriteLine("Have set true value");
 
                //ChatOutput.Text += "\n " + test;
            }
            catch (IOException socketException)
            {
                System.Diagnostics.Debug.WriteLine("Could not connect to Server");
                Environment.Exit(1);
            }

                try
                {
                    System.Diagnostics.Debug.WriteLine("trying second pahse");
                    string test = pet.readMessage();

                    System.Diagnostics.Debug.WriteLine("that aint right 88");
                    if (test == null)
                    {
                        System.Diagnostics.Debug.WriteLine("that aint right");
                    }
                    System.Diagnostics.Debug.WriteLine("Successflly read meassage");
                }
                catch (IOException inputexception)
                {
                    System.Diagnostics.Debug.WriteLine("Could not sucesffuly set up input stream");
                }

            readMessage2();

            //Thread inputThread = new Thread(readMessage);
            //inputThread.Start();

            //while (!inputThread.IsAlive) ;

        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {   

                pet.sendMessage(nameInput.Text);
                ChatOutput.Text += "\n" + nameInput.Text;
                ChatViewer.ScrollToBottom();
        }

        private void startTalking(object sender, RoutedEventArgs e)
        {

        }

        public void readMessage()
        {
            while (pet.getConnected())
            {
                string inputMessage = pet.input.ReadLine();
                ChatOutput.Text += inputMessage;
            }
        }

        public void readMessage2()
        {
            while (pet.getConnected())
            {
                try
                {
                    inputMessage = pet.input.ReadLine();

                    System.Diagnostics.Debug.WriteLine(inputMessage);
                    if (inputMessage == null) {
                        System.Diagnostics.Debug.WriteLine("problems ny");
                    }
                   
                }
                catch (IOException ioexception)
                {
                    System.Diagnostics.Debug.WriteLine("Final error if any");
                    pet.setConnected(false);
                }

               // ChatOutput.Text += inputMessage;
            }
        }
       

        public void readAMessage()
        {
            while (pet.getConnected())
            {
                string inputmessage = pet.input.ReadLine();
                System.Diagnostics.Debug.WriteLine("that aint right");
                ChatOutput.Text += inputmessage;
                System.Diagnostics.Debug.WriteLine("that aint right3");
            }
        }

    }
}
