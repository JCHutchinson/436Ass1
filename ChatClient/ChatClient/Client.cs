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
    class Client
    {
        private String message;
        private String serverIP;
        private BinaryWriter output;
        private BinaryReader input;
        private Socket connection;
        private bool Connected;


        //constructor
        public Client(String ip){
            serverIP = ip;
        }

        public void startRunning() {

            System.Diagnostics.Debug.WriteLine("We in at start running");

            try
            {
                connectToServer();
                setupStreams();
                System.Diagnostics.Debug.WriteLine("Setting up streams was successful");

                whileChatting();

            }
            catch(IOException socketException)
            {
                System.Diagnostics.Debug.WriteLine("Could not connect to Server");
                Environment.Exit(1);
            }
            
        }

        public void connectToServer() 
        {
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIP), 44001);

            //connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // connection.Connect(serverAddress);

            string str = "got it ";

            TcpClient client = new TcpClient(serverIP, 44001);
            NetworkStream ns = client.GetStream();

            output = new BinaryWriter(client.GetStream());
            System.Diagnostics.Debug.WriteLine("Made it here2");

            output.Write(str);

            output.Flush();
            System.Diagnostics.Debug.WriteLine("Made it here3");

            input = new BinaryReader(client.GetStream());
            System.Diagnostics.Debug.WriteLine("Made it here4");

            Connected = true;
        }

        public void setupStreams() {
           
           // NetworkStream outstream = new NetworkStream(connection);
           // NetworkStream instream = new NetworkStream(connection);
            System.Diagnostics.Debug.WriteLine("Made it here");


            //output = new BinaryWriter();
            //System.Diagnostics.Debug.WriteLine("Made it here2");

            //output.Flush();
            //System.Diagnostics.Debug.WriteLine("Made it here3");

            //input = new BinaryReader(instream);
            //System.Diagnostics.Debug.WriteLine("Made it here4");
        }

        public void whileChatting()
        {
            string str = "got it ";
            output.Write(str);
            output.Flush();
            System.Diagnostics.Debug.WriteLine("We are in chatting");


            //output.Write("get it");
            //output.Flush();

            //while (Connected = true)
            //{
            //    if (message == "end")
            //    {
            //        Connected = false;
            //        closeConnection();
            //    }
            //}

            System.Diagnostics.Debug.WriteLine("We are in chatting again");


            // do {
            // MainWindow p = new MainWindow();

            //message = input.ReadLine();
            // p.ChatOutput.Text = message;
            /* String tom = "someting here";
             output.WriteLine(tom);
             output.Flush(); */
            // } while (!message.Equals("END"));
        }

        public void closeConnection() {
            connection.Close();
        }

    }
}

/* private JTextField userText;
	private JTextArea chatWindow;
	private ObjectOutputStream output;
	private ObjectInputStream input;
	private String message = "";
	private String serverIP;
	private Socket connection;
	
	//constructor
	public Client(String host){
		super("Client baby!");
		serverIP = host;
		userText = new JTextField();
		userText.setEditable(false);
		userText.addActionListener(
			new ActionListener(){
				public void actionPerformed(ActionEvent event){
					sendMessage(event.getActionCommand());
					userText.setText("");
				}
			}
		);
		add(userText, BorderLayout.NORTH);
		chatWindow = new JTextArea();
		add(new JScrollPane(chatWindow), BorderLayout.CENTER);
		setSize(300,150);
		setVisible(true);
	}*/
