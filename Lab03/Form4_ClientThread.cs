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
using EmojiCSharp;

namespace Lab03
{
    public partial class Form4_ClientThread : Form
    {
        private TcpClient tcpClient = new TcpClient();
        private SslStream sslStream;
        private NetworkStream networkStream;
        Dictionary<string, int> savecount = new Dictionary<string, int>();
        Dictionary<int, string> takebyte64 = new Dictionary<int, string>();
        int count = 0;
        Dictionary<string, string> fileUrlDictionary = new Dictionary<string, string>();
        List<string> sentUrls = new List<string>();
        public Form4_ClientThread()
        {
            InitializeComponent();
            richTextBox2.DetectUrls = true;
            EmojiManager.Init();

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
                byte[] data = Encoding.UTF8.GetBytes("\n" + yournameTxtBox.Text + ": " + EmojiParser.ParseToUnicode(richTextBox1.Text));
                sslStream.Write(data, 0, data.Length);
                sslStream.Flush();

                UpdateTextBox("\n" + yournameTxtBox.Text + ": " + EmojiParser.ParseToUnicode(richTextBox1.Text));

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
                        string receivedMessage = Encoding.UTF8.GetString(data, 0, dataSize).Trim();
                        receivedMessage = EmojiParser.ParseToUnicode(receivedMessage);
                        string[] parts = receivedMessage.Split(':');

                        if (IsBase64String(receivedMessage) == 1)
                        {
                            //    MessageBox.Show(parts.Length.ToString()); //biến check
                            //   MessageBox.Show(parts[0]);    ///biến check
                            //   MessageBox.Show(parts[1]);   ///biến check
                            
                            takebyte64.Add(count, parts[1]);
                            UpdateTextBox("\n" + parts[0] + ":" + count);
                            count++;
                        }
                        else
                        {
                            // Update UI with the received message
                            UpdateTextBox($"\n {receivedMessage}");
                        }

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

        private int IsBase64String(string base64)
        {
            try
            {
                if (base64.Length < 100)
                {
                    return 0;
                    MessageBox.Show("0");
                }

                return 1;
            }
            catch
            {
                return 0;
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
                sslStream.AuthenticateAsClient("MYSSL");

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
                    string imageName = $"image_{imageCount++}";
                    
                    // Gửi chuỗi Base64 đến máy chủ
                    byte[] data = Encoding.UTF8.GetBytes("\n" + yournameTxtBox.Text + " (Image): " + base64Image);
                    sslStream.Write(data, 0, data.Length);
                    sslStream.Flush();
                    fileUrlDictionary.Add(imageName, imagePath);

                    richTextBox2.Text += $"\n Me: {imageName}{Environment.NewLine}\n";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending image: {ex.Message}");
            }
        }

        private void richTextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            
            string selectedText = richTextBox2.SelectedText.Trim();
            
                // Kiểm tra xem tin nhắn được chọn có phải là tin nhắn ảnh không
                if (selectedText.Contains("(Image):"))
                {
                    // Trích xuất số lượng từ tin nhắn
                    string[] parts = selectedText.Split(':');
                    string temp = parts[1].Trim();
                    int index = Convert.ToInt32(temp);
                    string base64Image = takebyte64[index];
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        pictureBoxImage.Image = Image.FromStream(ms);
                    }
                    

                }
                int indexx = richTextBox2.GetLineFromCharIndex(richTextBox2.SelectionStart);
                string line = richTextBox2.Lines[indexx];

                string[] part = line.Split(':');
                if (part.Length > 1  && (!selectedText.Contains("(Image):")))
                {
                    string fileName = part[1].Trim();
                    if (fileUrlDictionary.ContainsKey(fileName))
                    {
                        string url = fileUrlDictionary[fileName];
                        // Hiển thị ảnh từ URL
                        MessageBox.Show($"Display image from URL: {url}");
                        DisplayImageFromFile(url);

                    }
                }
            
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

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            string a = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAACxAAAAsQHGLUmNAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAA3FJREFUSImd1W1olWUYB/DfOWfH0jmdzZdOpuvFkMSZuMjSiQRGhh+CCkp7ASEwiwySJEhCovKDWJAIQl/6IvSCFCbYi6a2F1/SmblN3dwmq5bL3ObYdra5nacPz5muec40/3DBcz//6/5f131dN/cVcX1E8QhWYAK60IxfUYGWG9DIiAX4HG0IstgAyrASt2QSiWT4V4AtWI6B/DynlpS4+MD9Yk897s57prv9TIPm3fuc3/6toLrWnCCQhya8gt0jZT0DdQimJRyp+dG5oEEwkiVPSa5ariIS0ZE+1fps4gn8jp5Nbyu/nvBwaz7swthcNekga4eXKIpSzP/0Q8defs5DIx0zG1rbtRcucr6zy70owZFomnsVC55eqvRmxeG2fPllXxiVXm4aPEEc5+JxA51VpoyKX3G4aTy6woH9hyzGw1Eswx2vv6Qxk3h1HV3dmYVq6ujMwG1YY1L6cxlsRar5sL+HN+6rreF9X7r42qZ+vS3kliy8luuv0x+JaMPeKObF4/5ITLoS9QomjCeew+SCa7MciYvFxHLH+AuJCOonjNfVelxRpjJ0JxkzOnOJhnOpFNH0tSmY50Rru4lRxGJRqUGnikreeO/qpmziw7lvfmBiMf+0hevL/XLQHUV7V1LuoOPOPXzyGecvZBfOhL0VtF1iYCBc9/Qah4tRnE72KEz2SELh1NChsvrGxXv7wsTuu4spE0kmdV++LIHaKA4hvnOPGnhiMbEY27bfeIAPttLUzGsvhuuyYxqQg5MwHQNzZiobvGYrnwmv4MZ1glR99venv07w/trQd/5cQV9t+P/JJfYJ36TiwSR2o6/2J01Bg+DSb4Li2eHGB4vCQAd3COr3Cxp/FlR9L/h4vaBoZuhTPFvQ8kso3ntG36i4JtQL3zjSkVKJKY6m6qWCBkFXtWDdKsH4vKzDJhg3VvDuGkHP6aunWv2C/Wn+Lf47cDbjzYXFSku/VBKJhFyyh/JjnDpLcwsdnWEj587isRJG33pVoPyo04ueNTUItKAIPUN7lZMuVTC5QGXVdxr/zzzYssGhSEQrOsj+IsewUThr+2bNULb5HeWVu9R1nNQ5VLD1uEsVO5xd/byD+Xmq0mX5E4uyiQ/FHOxKB8ragyHWio+QP1wo09AfigQW4m5MQx5y0YtONOIEDhhW70H8Cxlp9d8hRpxPAAAAAElFTkSuQmCC";
            byte[] imageBytes = Convert.FromBase64String(a);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                pictureBoxImage.Image = Image.FromStream(ms);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEmoji = comboBox1.SelectedItem.ToString();

            // Thêm emoji đã chọn vào TextBox txtsent
            richTextBox1.Text += selectedEmoji;
        }
    }
}