using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing.Imaging;

namespace StreamingServer
{
    public partial class Form1 : Form
    {
        
        private String logMessage = "";
        private String host = "127.0.0.1";
        private int camera_port_1 = 11101;
        private int camera_port_2 = 11102;
        private int camera_port_3 = 11103;
        private int camera_portS_1 = 8080;
        private int camera_portS_2 = 8081;
        private int camera_portS_3 = 8082;
        private int width = 256;
        private int heigh = 256;

        private Server server_cam_1;
        private Server server_cam_2;
        private Server server_cam_3;
        
        private bool isWorkClient = true;
        private bool isWorkClient_1 = true;
        private bool isWorkClient_2 = true;
        private bool isWorkClient_3 = true;

        private bool frameRecieved1 = false;
        private bool frameRecieved2 = false;
        private bool frameRecieved3 = false;

        public int frameCount = 0; // счетчик принятых кадров

        public byte[] cam1_byte; // массивы для буфера с камер
        public byte[] cam2_byte;
        public byte[] cam3_byte;

        public byte[] cam1_render;
        public byte[] cam2_render;
        public byte[] cam3_render;

        public FormShowImage f;
        public Image returnImage;
        public Image image1FromForm;
        public Image image2FromForm;
        public Image image3FromForm;

        public Form1()
        {
            InitializeComponent();
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            //cam1_render = new byte[8196 * 10];
            //cam2_render = new byte[8196 * 10];
            //cam3_render = new byte[8196 * 10];
            server_cam_1 = new Server();
            server_cam_2 = new Server();
            server_cam_3 = new Server();
            pictureBox1.Image = Image.FromFile("2.png");
            pictureBox2.Image = Image.FromFile("2.png");
            pictureBox3.Image = Image.FromFile("2.png");
            textbox_host_1.Text = host;
            textBox_port_1.Text = camera_port_1.ToString();
            textBox_port_2.Text = camera_port_2.ToString();
            textBox_port_3.Text = camera_port_3.ToString();
            textBox_portS_1.Text = camera_portS_1.ToString();
            textBox_portS_2.Text = camera_portS_2.ToString();
            textBox_portS_3.Text = camera_portS_3.ToString();
            btn_stop_client_1.Enabled = false;
            btn_stop_client_2.Enabled = false;
            btn_stop_client_3.Enabled = false;
            btn_stop_stream_1.Enabled = false;
            btn_stop_stream_2.Enabled = false;
            btn_stop_stream_3.Enabled = false;


            //Thread tcpClientThread_1 = new Thread(tcpClient_1);
            //tcpClientThread_1.Start();
            server_cam_1.ImagesSource = ImageRecieve(cam1_byte);
            server_cam_1.Start();

        }

        private void tcpClient_1()
        {
            //recieveImage(Convert.ToInt32(textBox_port_1.Text));
            //recieveImage(11104);
            recieve1(Convert.ToInt32(textBox_port_1.Text), 1);
            
        }
        private void tcpClient_2()
        {
            recieve2(Convert.ToInt32(textBox_port_2.Text), 2);
        }
        private void tcpClient_3()
        {
            recieve3(Convert.ToInt32(textBox_port_3.Text), 3);
        }

        /// <summary>
        /// Функция передачи изображения для создания видеопотока
        /// </summary>
        /// <param name="byte_image">массив изображения</param>
        /// <returns></returns>
        private IEnumerable<Image> ImageRecieve(byte[] byte_image) // 1280x720
        {
            while (true)
            {
                MemoryStream ms = new MemoryStream(byte_image);
                returnImage = Image.FromStream(ms);
                yield return returnImage;
            }
        }

        private void renderPicture1()
        {
            if (frameRecieved1 == true)
            {
                ImageConverter ic = new ImageConverter();
                image1FromForm = (Image)ic.ConvertFrom(cam1_byte);

                //MemoryStream ms1 = new MemoryStream(cam1_render);
                //image1FromForm = Image.FromStream(ms1);
                //MemoryStream ms2 = new MemoryStream(cam2_render);
                //image2FromForm = Image.FromStream(ms2);
                //MemoryStream ms3 = new MemoryStream(cam3_render);
                //image3FromForm = Image.FromStream(ms3);
                pictureBox4.Image = image1FromForm;
                //f.picturebox1.Image = image;
                frameRecieved1 = false;
            }
        }
        private void renderPicture2()
        {
            if (frameRecieved2 == true)
            {
                
                MemoryStream ms2 = new MemoryStream(cam2_render);
                image2FromForm = Image.FromStream(ms2);
                frameRecieved2 = false;
            }
        }
        private void renderPicture3()
        {
            if (frameRecieved3 == true)
            {
                
                MemoryStream ms3 = new MemoryStream(cam3_render);
                image3FromForm = Image.FromStream(ms3);
                frameRecieved3 = false;
            }
        }
        private void recieveImage(int port)
        {
            BeginInvoke(new MethodInvoker(delegate {
                btn_start_client_1.Enabled = false;
            }));
            
            int i = 0;
            int request = 0;
            NetworkStream stream = null;
            TcpClient client = new TcpClient();
            ImageConverter ic = new ImageConverter();
            try
            {
                // соединяемся с сервером
                client.Connect(textbox_host_1.Text, port);

                BeginInvoke(new MethodInvoker(delegate
                {
                    if (client.Connected)
                    {
                        btn_start_client_1.Enabled = false;
                        btn_stop_client_1.Enabled = true;
                        logMessage += ("Connected! " + Environment.NewLine);
                    }
                    else
                    {
                        btn_start_client_1.Enabled = true;
                    }
                }));

                stream = client.GetStream();

                cam1_byte = new byte[8192 * 100];
                cam2_byte = new byte[8192 * 10];
                cam3_byte = new byte[8192 * 10];
                cam1_render = new byte[8192*10];
                

                BeginInvoke(new MethodInvoker(delegate
                {
                    logMessage += ("получили поток! " + Environment.NewLine);
                    pictureBox1.Image = Image.FromFile("1.png");
                }));

                while (isWorkClient)
                {


                    request = stream.Read(cam1_byte, 0, client.ReceiveBufferSize);
                    //request = stream.Read(cam2_byte, 0, client.ReceiveBufferSize);
                    //request = stream.Read(cam3_byte, 0, client.ReceiveBufferSize);
                    //cam1_render = cam1_byte;
                    //cam2_render = cam2_byte;
                    //cam3_render = cam3_byte;
                    //cam1_render = cam1_byte;

                    frameRecieved1 = true;

                    //pictureBox4.Image = (Image)ic.ConvertFrom(cam1_render);
                    //File.WriteAllBytes("cam/camera_image_" + i + ".jpg", cam1_byte);

                    frameCount++;
                    //Thread.Sleep(1000);
                }
                stream.Close();

                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile("2.png");
                    btn_start_client_1.Enabled = true;
                    btn_stop_client_1.Enabled = false;
                    isWorkClient = false;
                }));
            }
            catch (SocketException ex)
            {
                BeginInvoke(new MethodInvoker(delegate {

                    
                    logMessage += ex.ErrorCode;
                    logMessage += ex.ToString();
                }));
            }
            finally
            {
                BeginInvoke(new MethodInvoker(delegate {
                    pictureBox1.Image = Image.FromFile("2.png");
                    btn_start_client_1.Enabled = true;
                }));
                client.Close();
            }
        }
        private void recieve(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                if (cam_number == 1) btn_start_client_1.Enabled = false;
                if (cam_number == 2) btn_start_client_2.Enabled = false;
                if (cam_number == 3) btn_start_client_3.Enabled = false;
            }));

            //if (cam_number == 1) bool isWork = isWorkClient;

            IPHostEntry ipHost = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //cam1_byte = new byte[12000];
                // соединяемся с сервером
                sender.Connect(ipEndPoint);

                BeginInvoke(new MethodInvoker(delegate
                {
                    if (sender.Connected)
                    {
                        if (cam_number == 1)
                        {
                            btn_start_client_1.Enabled = false;
                            btn_stop_client_1.Enabled = true;
                        }
                        if (cam_number == 2)
                        {
                            btn_start_client_2.Enabled = false;
                            btn_stop_client_2.Enabled = true;
                        }
                        if (cam_number == 3)
                        {
                            btn_start_client_3.Enabled = false;
                            btn_stop_client_3.Enabled = true;
                        }
                        logMessage += ("Cam " + cam_number + " connecting..." + Environment.NewLine);
                    }
                    else
                    {
                        btn_start_client_1.Enabled = true;
                    }
                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                string reply = "ok";
                byte[] size = new byte[1024];
                int _size = 0;
                byte[] msg = Encoding.UTF8.GetBytes(reply);
                while (isWorkClient_1)
                {
                    int bytesRec = sender.Receive(size);
                    _size = BitConverter.ToInt32(size,0);
                    cam1_byte = new byte[sender.Available];
                    bytesRec = sender.Receive(cam1_byte);
                    //sender.Send(msg);
                    cam1_render = cam1_byte;
                    frameRecieved1 = true;

                    MemoryStream ms1 = new MemoryStream(cam1_render);
                    image1FromForm = Image.FromStream(ms1);
                    //File.WriteAllBytes("cam/cam.jpg", cam1_byte);
                    //renderPicture1();
                    cam1_byte = null;
                    //pictureBox4.Image = Image.FromStream(ms1);

                    frameCount++;
                }
                
                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile("2.png");
                    logMessage += ("Cam " + cam_number + " is not connected!" + Environment.NewLine);
                    btn_start_client_1.Enabled = true;
                    btn_stop_client_1.Enabled = false;
                    isWorkClient_1 = false;
                }));
            }
            catch (SocketException ex)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    if (ex.ErrorCode == 10061) logMessage += "Компьютер отверг запрос на подключение!" + Environment.NewLine;
                    else logMessage += ex.ToString();
                }));
            }
            finally
            {
                BeginInvoke(new MethodInvoker(delegate {
                    pictureBox1.Image = Image.FromFile("2.png");
                    btn_start_client_1.Enabled = true;
                }));
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
        }

        private void recieve1(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                if (cam_number == 1) btn_start_client_1.Enabled = false;
                if (cam_number == 2) btn_start_client_2.Enabled = false;
                if (cam_number == 3) btn_start_client_3.Enabled = false;
            }));

            TcpClient client = new TcpClient();

            client.ReceiveTimeout = 5000;

            try
            {
                // соединяемся с сервером
                client.Connect(textbox_host_1.Text, port);

                BeginInvoke(new MethodInvoker(delegate
                {
                    if (client.Connected)
                    {
                        if (cam_number == 1)
                        {
                            btn_start_client_1.Enabled = false;
                            btn_stop_client_1.Enabled = true;
                        }
                        if (cam_number == 2)
                        {
                            btn_start_client_2.Enabled = false;
                            btn_stop_client_2.Enabled = true;
                        }
                        if (cam_number == 3)
                        {
                            btn_start_client_3.Enabled = false;
                            btn_stop_client_3.Enabled = true;
                        }
                        logMessage += ("Cam " + cam_number + " connecting..." + Environment.NewLine);
                    }
                    else
                    {
                        btn_start_client_1.Enabled = true;
                    }
                }));

                
                //client.SendBufferSize = 1024;
                //byte[] cam1_byte = new byte[8196 * 10];
                cam1_render = new byte[8196 * 10];
                byte[] size = new byte[1024];
                string request_str = "ok";
                //byte[] request_byte = Encoding.UTF8.GetBytes(request_str);
                byte[] request_byte = new byte[1024];
                int request = 0;
                NetworkStream stream = client.GetStream();
                int offset = 0;
                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                while (isWorkClient_1)
                {

                    byte[] cam1_byte_recieve = new byte[client.ReceiveBufferSize];
                    int bytes = stream.Read(cam1_byte_recieve, offset, client.ReceiveBufferSize);
                    //byte[] cam1_byte = new byte[bytes];
                    //Array.Copy(cam1_byte_recieve, cam1_byte, bytes);

                    offset += bytes;

                    frameRecieved1 = true;
                    frameCount++;
                }
                stream.Close();

                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile("2.png");
                    logMessage += ("Cam " + cam_number + " is not connected!" + Environment.NewLine);
                    btn_start_client_1.Enabled = true;
                    btn_stop_client_1.Enabled = false;
                    isWorkClient_1 = false;
                }));
            }
            catch (SocketException ex)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    if (ex.ErrorCode == 10061) logMessage += "Компьютер отверг запрос на подключение!" + Environment.NewLine;
                    else logMessage += ex.ToString();
                }));
            }
            finally
            {
                BeginInvoke(new MethodInvoker(delegate {
                    pictureBox1.Image = Image.FromFile("2.png");
                    btn_start_client_1.Enabled = true;
                }));
                client.Close();
            }
        }
        private void recieve2(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                if (cam_number == 1) btn_start_client_1.Enabled = false;
                if (cam_number == 2) btn_start_client_2.Enabled = false;
                if (cam_number == 3) btn_start_client_3.Enabled = false;
            }));

            //if (cam_number == 1) bool isWork = isWorkClient;

            int request = 0;

            TcpClient client = new TcpClient();
            try
            {
                // соединяемся с сервером
                client.Connect(textbox_host_1.Text, port);

                BeginInvoke(new MethodInvoker(delegate
                {
                    if (client.Connected)
                    {
                        if (cam_number == 1)
                        {
                            btn_start_client_1.Enabled = false;
                            btn_stop_client_1.Enabled = true;
                        }
                        if (cam_number == 2)
                        {
                            btn_start_client_2.Enabled = false;
                            btn_stop_client_2.Enabled = true;
                        }
                        if (cam_number == 3)
                        {
                            btn_start_client_3.Enabled = false;
                            btn_stop_client_3.Enabled = true;
                        }
                        logMessage += ("Cam " + cam_number + " connecting..." + Environment.NewLine);
                    }
                    else
                    {
                        btn_start_client_2.Enabled = true;
                    }
                }));

                NetworkStream stream = client.GetStream();

                cam2_byte = new byte[8192 * 10];
                cam2_render = new byte[8192 * 10];

                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox2.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                while (isWorkClient_2)
                {
                    request = stream.Read(cam2_byte, 0, client.ReceiveBufferSize);
                    //MemoryStream ms = new MemoryStream(cam2_byte);
                    //image2FromForm = Image.FromStream(ms);
                    frameRecieved2 = true;
                    renderPicture2();
                    frameCount++;
                }
                stream.Close();

                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox2.Image = Image.FromFile("2.png");
                    logMessage += ("Cam " + cam_number + " is not connected!" + Environment.NewLine);
                    btn_start_client_2.Enabled = true;
                    btn_stop_client_2.Enabled = false;
                    isWorkClient_2 = false;
                }));
            }
            catch (SocketException ex)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    if (ex.ErrorCode == 10061) logMessage += "Компьютер отверг запрос на подключение!" + Environment.NewLine;
                    else logMessage += ex.ToString();
                }));
            }
            finally
            {
                BeginInvoke(new MethodInvoker(delegate {
                    pictureBox2.Image = Image.FromFile("2.png");
                    btn_start_client_2.Enabled = true;
                }));
                client.Close();
            }
        }
        private void recieve3(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                if (cam_number == 1) btn_start_client_1.Enabled = false;
                if (cam_number == 2) btn_start_client_2.Enabled = false;
                if (cam_number == 3) btn_start_client_3.Enabled = false;
            }));

            //if (cam_number == 1) bool isWork = isWorkClient;

            int request = 0;

            TcpClient client = new TcpClient();
            try
            {
                // соединяемся с сервером
                client.Connect(textbox_host_1.Text, port);

                BeginInvoke(new MethodInvoker(delegate
                {
                    if (client.Connected)
                    {
                        if (cam_number == 1)
                        {
                            btn_start_client_1.Enabled = false;
                            btn_stop_client_1.Enabled = true;
                        }
                        if (cam_number == 2)
                        {
                            btn_start_client_2.Enabled = false;
                            btn_stop_client_2.Enabled = true;
                        }
                        if (cam_number == 3)
                        {
                            btn_start_client_3.Enabled = false;
                            btn_stop_client_3.Enabled = true;
                        }
                        logMessage += ("Cam " + cam_number + " connecting..." + Environment.NewLine);
                    }
                    else
                    {
                        btn_start_client_3.Enabled = true;
                    }
                }));

                NetworkStream stream = client.GetStream();

                cam3_byte = new byte[8192 * 10];
                cam3_render = new byte[8192 * 10];

                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                while (isWorkClient_3)
                {
                    request = stream.Read(cam3_byte, 0, client.ReceiveBufferSize);
                    //MemoryStream ms = new MemoryStream(cam3_byte);
                    //image3FromForm = Image.FromStream(ms);
                    frameRecieved3 = true;
                    renderPicture3();
                    frameCount++;
                }
                stream.Close();

                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox3.Image = Image.FromFile("2.png");
                    logMessage += ("Cam " + cam_number + " is not connected!" + Environment.NewLine);
                    btn_start_client_3.Enabled = true;
                    btn_stop_client_3.Enabled = false;
                    isWorkClient = false;
                }));
            }
            catch (SocketException ex)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    if (ex.ErrorCode == 10061) logMessage += "Компьютер отверг запрос на подключение!" + Environment.NewLine;
                    else logMessage += ex.ToString();
                }));
            }
            finally
            {
                BeginInvoke(new MethodInvoker(delegate {
                    pictureBox3.Image = Image.FromFile("2.png");
                    btn_start_client_3.Enabled = true;
                }));
                client.Close();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            logBox.Text = logMessage;
            toolStripStatusLabel1.Text = "Frame recieved: " + frameCount;
            //renderPicture1();
            //if (cam1_byte != null)
            //{
            //    MemoryStream ms = new MemoryStream(cam1_render);
                
            //    pictureBox4.Image = Image.FromStream(ms);
            //}
        }

        /// <summary>
        /// Проверка правильности ввода порта в TextBox
        /// </summary>
        /// <param name="e">KeyPressEventArgs</param>
        private void checkPortBox(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        //----------------------Text-box-check-input--------------------//
        private void textbox_host_KeyPress(object sender, KeyPressEventArgs e)
        {
            //checkPortBox(e);
        }
        private void textBox_port_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkPortBox(e);
        }
        //-----------------------End-of-check-textbox-------------------//

        //-------------------------Buttons-------------------------------//
        private void btn_start_client_1_Click(object sender, EventArgs e)
        {
            Thread tcpClientThread_1 = new Thread(tcpClient_1);
            tcpClientThread_1.Start();
        }
        private void btn_stop_client_Click(object sender, EventArgs e)
        {
            isWorkClient_1 = false;
        }
        private void btn_start_client_2_Click(object sender, EventArgs e)
        {
            Thread tcpClientThread_2 = new Thread(tcpClient_2);
            tcpClientThread_2.Start();
        }
        private void btn_stop_client_2_Click(object sender, EventArgs e)
        {
            isWorkClient_2 = false;
        }
        private void btn_start_client_3_Click(object sender, EventArgs e)
        {
            Thread tcpClientThread_3 = new Thread(tcpClient_3);
            tcpClientThread_3.Start();
        }
        private void btn_stop_client_3_Click(object sender, EventArgs e)
        {
            isWorkClient_3 = false;
        }
        private void btn_show_form2_Click(object sender, EventArgs e)
        {
            f = new FormShowImage();
            f.Tag = this;
            f.Show();
            
        }
        private void btn_clear_log_Click(object sender, EventArgs e)
        {
            logMessage = "";
        }
        private void btn_start_stream_1_Click(object sender, EventArgs e)
        {
            server_cam_1.ImagesSource = ImageRecieve(cam1_byte);
            server_cam_1.Start(Convert.ToInt32(textBox_portS_1.Text));
            btn_start_stream_1.Enabled = false;
            btn_stop_stream_1.Enabled = true;
        }
        private void btn_stop_stream_1_Click(object sender, EventArgs e)
        {
            server_cam_1.Stop();
            btn_start_stream_1.Enabled = true;
            btn_stop_stream_1.Enabled = false;
        }
        private void btn_start_stream_2_Click(object sender, EventArgs e)
        {
            server_cam_2.ImagesSource = ImageRecieve(cam2_byte);
            server_cam_2.Start(Convert.ToInt32(textBox_portS_2.Text));
            btn_start_stream_2.Enabled = false;
            btn_stop_stream_2.Enabled = true;
        }
        private void btn_stop_stream_2_Click(object sender, EventArgs e)
        {
            server_cam_2.Stop();
            btn_start_stream_2.Enabled = true;
            btn_stop_stream_2.Enabled = false;
        }
        private void btn_start_stream_3_Click(object sender, EventArgs e)
        {
            server_cam_3.ImagesSource = ImageRecieve(cam3_byte);
            server_cam_3.Start(Convert.ToInt32(textBox_portS_3.Text));
            btn_start_stream_3.Enabled = false;
            btn_stop_stream_3.Enabled = true;
        }
        private void btn_stop_stream_3_Click(object sender, EventArgs e)
        {
            server_cam_3.Stop();
            btn_start_stream_3.Enabled = true;
            btn_stop_stream_3.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cam1_byte != null)
            {
                MemoryStream ms = new MemoryStream(cam1_render);
                pictureBox4.Image = Image.FromStream(ms);
            }
        }

        //-----------------------End-of-buttons--------------------------//
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
