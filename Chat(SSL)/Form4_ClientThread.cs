using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Lab03
{
    public partial class Form4_ClientThread : Form
    {
        private TcpClient tcpClient = new TcpClient();
        private SslStream sslStream;
        private NetworkStream networkStream;
       

        public Form4_ClientThread()
        {
            InitializeComponent();
            richTextBox2.DetectUrls = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!tcpClient.Connected)
            {
                MessageBox.Show("Chưa kết nối đến máy chủ.");
                return;
            }

            try
            {
                // Construct the message with sender's name and message content
                

                // Convert the message to bytes and send it to the server
                byte[] data = Encoding.ASCII.GetBytes("\n"+yournameTxtBox.Text + ": " + richTextBox1.Text);
                sslStream.Write(data, 0, data.Length);
                sslStream.Flush();
                
                UpdateTextBox("\n" + yournameTxtBox.Text + ": " + richTextBox1.Text);

                // Clear the message box after sending
                richTextBox1.Clear();

                // Optionally, append the sent message to the local UI for display
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra khi gửi tin nhắn: " + ex.Message);
            }

        }
        
        private void Form4_ClientThread_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void UpdateTextBox(string text)
        {
            // Check if the calling thread is the same as the thread that created the TextBox
            if (richTextBox2.InvokeRequired)
            {
                // If not, use cross-thread invocation to call the UpdateTextBox method on the main UI thread
                richTextBox2.Invoke((MethodInvoker)delegate {
                    UpdateTextBox(text);
                });
            }
            else
            {
                // If so, set the text of the TextBox directly
                richTextBox2.Text += text;
            }
        }

        private void ReceiveResponses()
        {
            try
            {
                while (tcpClient.Connected)
                {
                    if (networkStream.DataAvailable)
                    {
                        // Read data from the network stream
                        byte[] data = new byte[2048];
                        int dataSize = networkStream.Read(data, 0, data.Length);

                        // Convert received data to string
                        string receivedMessage = Encoding.ASCII.GetString(data, 0, dataSize).Trim();

                        // Update UI with the received message
                        UpdateTextBox($"\n {receivedMessage}");
                    }
                }
            }
            catch (IOException)
            {
                // Connection closed by server or lost
                UpdateTextBox("Kết nối đã bị đóng.\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra trong quá trình nhận dữ liệu từ máy chủ: " + ex.Message);
            }
        }



        private Thread responseThread;

    private void btnInit_Click(object sender, EventArgs e)
    {
            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
                tcpClient.Connect(ipep);

                networkStream = tcpClient.GetStream();
                sslStream = new SslStream(networkStream, false);
                sslStream.AuthenticateAsClient("MySslSocketCertificate");

                responseThread = new Thread(ReceiveResponses);
                responseThread.Start();

                MessageBox.Show("Kết nối thành công!");
            }
            catch (AuthenticationException authEx)
            {
                MessageBox.Show("Xác thực SSL/TLS thất bại: " + authEx.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Hãy nhập đúng địa chỉ IP và port.");
            }
            catch (SocketException)
            {
                MessageBox.Show("Kết nối tới máy chủ thất bại. Hãy kiểm tra lại địa chỉ IP và port.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra khi thiết lập kết nối: " + ex.Message);
            }
        }


        private void Form4_ClientThread_FormClosed(object sender, FormClosedEventArgs e)
        {
            responseThread.Abort();
        }

        private void btn_picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                SendImage(imagePath);
            }
        }
        private int imageCount = 0;

        private void SendImage(string imagePath)
        {
            try
            {
                if (tcpClient.Connected)
                {
                    // Đọc dữ liệu của ảnh từ đường dẫn tệp tin
                    byte[] imageData = File.ReadAllBytes(imagePath);

                    // Chuyển đổi dữ liệu ảnh sang chuỗi Base64
                    string base64Image = Convert.ToBase64String(imageData);

                    // Gửi chuỗi Base64 đến máy chủ
                    byte[] data = Encoding.ASCII.GetBytes("\n" + yournameTxtBox.Text + " (Image): " + base64Image);
                    sslStream.Write(data, 0, data.Length);
                    sslStream.Flush();

                    // Hiển thị ảnh lên PictureBox
                    DisplayImageFromFile(imagePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending image: {ex.Message}");
            }
        }

        private void richTextBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void DisplayImageFromFile(string filePath)
        {
            try
            {
                // Kiểm tra xem tệp tin có tồn tại không
                if (File.Exists(filePath))
                {
                    // Tạo một đối tượng Image từ tệp tin
                    Image image = Image.FromFile(filePath);

                    // Hiển thị ảnh trên PictureBox
                    pictureBoxImage.Image = image;
                }
                else
                {
                    MessageBox.Show("File not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}");
            }

        }

        

   
    }
}
