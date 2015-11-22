import java.io.*;
import java.net.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

///// try using dataINputStream instead of objectInputStream

public class Server extends JFrame {
	
	private JTextField userText;
	private JTextArea chatWindow;
	private ObjectOutputStream output;
	private ObjectInputStream input;
	private ServerSocket server;
	private Socket connection;
	
	//Constructor
	public Server(){
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
	
	//Run Server
	public void StartRunning(){
		try{
			server = new ServerSocket(44001, 100);
			while(true){
				try{
					waitForConnection();
					showMessage("\n going into setting up streams;");
					setupStreams();
					showMessage("\n going into while chattings");
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
		output = new ObjectOutputStream(connection.getOutputStream());
		showMessage("\n streaming here 1");
		
		output.flush();
		showMessage("\n streaming here 2");
		
		input = new ObjectInputStream(connection.getInputStream());
		showMessage("\n streaming here 3");
		
		showMessage("\n Streams are now active! \n");
	}
	
	//during the chat conversation
	private void whileChatting() throws IOException{
		String message = " You are now connected!";
		sendMessage(message);
		ableToType(true);
		
		do{
			try{
				message = (String) input.readObject();
				showMessage("\n" + message);
				
			}catch(ClassNotFoundException classNotFoundException){
				showMessage("\n idk what the user sent!");
			}
			
		}while(!message.equals("CLIENT - END"));
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
			output.writeObject(" SERVER - " + message);
			output.flush();
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




