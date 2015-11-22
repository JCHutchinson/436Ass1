import java.io.*;
import java.net.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class Server2 extends JFrame {
	
	private JTextField userText;
	private JTextArea chatWindow;
	private DataOutputStream output;
	private DataInputStream input;
	private BufferedWriter output2;
	private BufferedReader input2;
	private ServerSocket server;
	private Socket connection;
	
	//Constructor
		public Server2(){
			super("CMPT 436 Instant Messenger");
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
			add(new JScrollPane(chatWindow));
			setSize(300,150);
			setVisible(true);
		}
		
		public void StartRunning(){
			try{
				server = new ServerSocket(44001, 100);
				while(true){
					try{
						waitForConnection();
						showMessage("\n going into setting up streams;");
						setupStreams();
						
						sendMessage("wait there");
						
						whileChatting();
					
					}catch(EOFException eofException){
						showMessage("\n Server Ended Connection!");
					}finally{
						closeCrap();
					}
				}
			}catch(IOException ioException){
				ioException.printStackTrace();
			}
		}
		
		//Wait for Connection
		private void waitForConnection() throws IOException{
			showMessage(" Waiting For peer to join ... \n");
			connection = server.accept();
			showMessage(" Now connected to "+connection.getInetAddress().getHostName());
		}
		
		//get stream to sned and recieve data
		private void setupStreams() throws IOException{
			output = new DataOutputStream(connection.getOutputStream());
			showMessage("\n streaming here 1");
			
			output.flush();
			showMessage("\n streaming here 2");
			
			output2 = new BufferedWriter(new OutputStreamWriter(connection.getOutputStream()));
			
			output2.flush();
			
			input = new DataInputStream(connection.getInputStream());
			
			 input2
	          = new BufferedReader(new InputStreamReader(connection.getInputStream()));
			showMessage("\n streaming here 3");
			
			showMessage("\n Streams are now active! \n");
		}
		
		private void whileChatting(){
			String message = " You are now connected!";
			sendMessage(message);
			ableToType(true);
			
			do{
				try{
					message = (String) input2.readLine();
					showMessage("\n" + message);
				}catch (IOException ioException){
					
				}
			}while(!message.equals("END"));
						
			
		}
		
		
		//Close Streams and Sockets when chat ends
		private void closeCrap(){
			showMessage("\n Closing connections...\n");
			ableToType(false);
			try{
				output.close();
				input.close();
				connection.close();
				
			}catch(IOException ioException){
				ioException.printStackTrace();
			}
		}
		
		//Send message to client
		private void sendMessage(String message){
			try{
				//output.writeObject(" SERVER - " + message);
				output2.write("Server says it - " + message);
				output2.newLine();
				output2.flush();
				showMessage("\n SERVER - " + message);
			}catch(IOException ioException){
				chatWindow.append("\n ERROR: UNABLE TO SEND MESSAGE");
			}
		}
		
		//Update ChatWindow
		private void showMessage(final String text){
			SwingUtilities.invokeLater(
				new Runnable(){
					public void run(){
						chatWindow.append(text);
					}
				}
			);
		}
		
		//let user type into their box
		private void ableToType(final boolean tof){
			SwingUtilities.invokeLater(
				new Runnable(){
					public void run(){
						userText.setEditable(tof);
					}
				}
			);
		}


}

/*
BufferedReader.readLine() method. Programs that use the DataInputStream class to read lines can be converted to use the BufferedReader class by replacing code of the form: 
     DataInputStream d = new DataInputStream(in);
 
 with: 
     BufferedReader d
          = new BufferedReader(new InputStreamReader(in));
 

See the general contract of the readLine method of DataInput. 
Bytes for this operation are read from the contained input stream.
Specified by:readLine in interface DataInputReturns:the next line of text from this input stream.Throws:IOException - if an I/O error occurs.See Also:BufferedReader.readLine(), FilterInputStream.in
*/