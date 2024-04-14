using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;

namespace Lab03
{
    public partial class Form4_ServerThread : Form
    {

        public Form4_ServerThread()
        {
            InitializeComponent();
            serverCertificate = new X509Certificate2("C:\\Users\\HungPhung\\cert.pfx", "Cr180908");

        }

        Thread TCPServer;
        TcpListener TCPserver;
        private X509Certificate2 serverCertificate;


        private void button1_Click(object sender, EventArgs e)
        {
            TCPServer = new Thread(new ThreadStart(ServerThread));
            TCPServer.IsBackground = true;
            TCPServer.Start();
        }

        private List<TcpClient> Clients = new List<TcpClient>(); // List to store connected clients

        // Method to handle communication with a client
        // Method to handle communication with a client
        private async Task<string> cliThread(SslStream ssl, TcpClient tcpClient)
        {
            StringBuilder messageData = new StringBuilder();
            Decoder decoder = Encoding.ASCII.GetDecoder();

            try
            {
                while (true)
                {
                    byte[] buffer = new byte[2048];
                    int bytesRead = await ssl.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    char[] chars = new char[decoder.GetCharCount(buffer, 0, bytesRead)];
                    decoder.GetChars(buffer, 0, bytesRead, chars, 0);

                    // Convert received characters to a string without adding new line characters
                    string message = new string(chars);

                    // Check if the message is an image (Base64 encoded)
                  //  if (!IsBase64String(message))
                    {
                        // Update UI with the received message
                        if (!IsDisposed && InvokeRequired)
                        {
                            Invoke(new Action(() =>
                            {
                                richTextBox1.AppendText(message + "\n"); // Add newline character after each message
                            }));
                        }
                        else if (!IsDisposed)
                        {
                            richTextBox1.AppendText(message + "\n"); // Add newline character after each message
                        }

                        messageData.AppendLine(message); // Append message with newline to StringBuilder

                        // Broadcast to clients excluding the sender
                        List<Task> writeTasks = new List<Task>();
                        lock (clientsLock)
                        {
                            foreach (TcpClient client in Clients)
                            {
                                if (client != tcpClient)
                                {
                                    NetworkStream clientNetStream = client.GetStream();
                                    if (clientNetStream.CanWrite)
                                    {
                                        writeTasks.Add(clientNetStream.WriteAsync(buffer, 0, bytesRead));
                                    }
                                }
                            }
                        }
                        await Task.WhenAll(writeTasks);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }

            return messageData.ToString();
        }

        // Helper method to check if a string is a Base64 encoded string
        private bool IsBase64String(string input)
        {
            try
            {
                Convert.FromBase64String(input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }














        // Method to handle new client connections
        private void ServerThread()
        {
            // Load the server certificate
            X509Certificate2 serverCertificate = new X509Certificate2("C:\\Users\\HungPhung\\cert.pfx", "Cr180908");

            // Create TCP listener
            TcpListener TCPserver = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);

            // Start the server
            TCPserver.Start();
            string connectInfo = "Server running on " + TCPserver.LocalEndpoint.ToString();
            if (!IsDisposed && InvokeRequired)
            {
                Invoke(new Action(() => richTextBox1.Text = connectInfo));
            }
            else if (!IsDisposed)
            {
                richTextBox1.Text = connectInfo;
            }

            while (true)
            {
                try
                {
                    // Accept incoming TCP connection
                    TcpClient TCPclient = TCPserver.AcceptTcpClient();

                    // Add the client to the list of connected clients
                    lock (clientsLock)
                    {
                        Clients.Add(TCPclient);
                    }

                    // Create SSL stream
                    SslStream sslStream = new SslStream(TCPclient.GetStream(), false);

                    // Authenticate the server
                    sslStream.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, true);

                    // Start a new thread to handle client communication
                    Thread cli = new Thread(() => cliThread(sslStream, TCPclient));
                    cli.Start();
                    threads[numOfThreads] = cli;
                    numOfThreads++;
                }
                catch (Exception e)
                {
                    // Handle exceptions
                }
            }
        }


        private object nLock = new object();
        private object clientsLock = new object();
        private int n = 0;

        private Thread[] threads = new Thread[100];
        private int numOfThreads = 0;

        

        private void Form4_ServerThread_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            for (int i = 0; i < numOfThreads; i++)
            {
                threads[i].Abort();
            }
            TCPserver.Stop();
            TCPServer.Abort();
        }
    }
}