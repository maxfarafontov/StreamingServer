using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
using System.Runtime.Serialization.Formatters.Binary;

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

        public Byte[] cam1_byte; // массивы для буфера с камер
        public Byte[] cam2_byte;
        public Byte[] cam3_byte;

        public FormShowImage f;
        public Snapshot sn;
        public receiveServer srv;
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
            server_cam_1 = new Server();
            server_cam_2 = new Server();
            server_cam_3 = new Server();

            sn = new Snapshot();
            srv = new receiveServer();

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
            //server_cam_1.ImagesSource = ImageRecieve(cam1_byte);
            //server_cam_1.Start();

        }

        private void udpClient()
        {
            client_1(Convert.ToInt32(textBox_port_1.Text), 1);
        }
        private void tcpClient_2()
        {
            //recieve2(Convert.ToInt32(textBox_port_2.Text), 2);
            client_2(Convert.ToInt32(textBox_port_1.Text), 2);
        }
        private void tcpClient_3()
        {
            //recieve3(Convert.ToInt32(textBox_port_3.Text), 3);
            client_3(Convert.ToInt32(textBox_port_1.Text), 2);
        }

        /// <summary>
        /// Функция передачи изображения для создания видеопотока
        /// </summary>
        /// <param name="byte_image">массив изображения</param>
        /// <returns></returns>
        private IEnumerable<Image> ImageRecieve(Byte[] byte_image) // 1280x720
        {
            while (true)
            {
                using (MemoryStream ms = new MemoryStream(byte_image))
                {
                    returnImage = Image.FromStream(ms);
                    yield return returnImage;
                }
                //MemoryStream ms = new MemoryStream(cam1_byte);
                //returnImage = Image.FromStream(ms);
                //yield return returnImage;
            }
        }

        private void renderPicture1()
        {
            try
            {
                MemoryStream ms = new MemoryStream(cam1_byte);
                //image1FromForm = Image.FromStream(ms);
                pictureBox4.Image = Image.FromStream(ms);

            }
            catch (Exception)
            {
                
            }
                
        }
        private void renderPicture2()
        {
            try
            {
                MemoryStream ms = new MemoryStream(cam2_byte);
                //image2FromForm = Image.FromStream(ms);
                pictureBox2.Image = Image.FromStream(ms);
            }
            catch (Exception)
            {
                //throw;
            }
                
            
        }
        private void renderPicture3()
        {
            try
            {
                MemoryStream ms = new MemoryStream(cam3_byte);
                //image3FromForm = Image.FromStream(ms);
                pictureBox3.Image = Image.FromStream(ms);
            }
            catch (Exception)
            {
                //throw;
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

        private void client_1(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                btn_start_client_1.Enabled = false;
            }));

            UdpClient client = new UdpClient(port);
            IPEndPoint ip = null;
            try
            {
                
                BeginInvoke(new MethodInvoker(delegate
                {
                    btn_start_client_1.Enabled = false;
                    btn_stop_client_1.Enabled = true;
                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam 1 connected!" + Environment.NewLine);
                }));

                while (true)
                {
                    cam1_byte = client.Receive(ref ip);

                    
                    //MemoryStream ms = new MemoryStream(cam1_byte);
                    //pictureBox4.Image = Image.FromStream(ms);
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
                client.Close();
            }
        }

        private void client_2(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                btn_start_client_1.Enabled = false;
            }));

            UdpClient client = new UdpClient(port);
            IPEndPoint ip = null;
            try
            {

                BeginInvoke(new MethodInvoker(delegate
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

                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                while (true)
                {
                    cam1_byte = client.Receive(ref ip);


                    //MemoryStream ms = new MemoryStream(cam1_byte);
                    //pictureBox4.Image = Image.FromStream(ms);
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
                client.Close();
            }
        }
        private void client_3(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                btn_start_client_3.Enabled = false;
            }));

            UdpClient client = new UdpClient(port);
            IPEndPoint ip = null;
            try
            {

                BeginInvoke(new MethodInvoker(delegate
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

                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                while (true)
                {
                    cam1_byte = client.Receive(ref ip);


                    //MemoryStream ms = new MemoryStream(cam1_byte);
                    //pictureBox4.Image = Image.FromStream(ms);
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
                //cam1_byte = new byte[8196];
                //string reply = "ok";
                //int _size = 0;
                byte[] temp = new byte[100000];
                object locker = new object();
                byte[] msg = BitConverter.GetBytes(9);
                while (isWorkClient_1)
                {
                    if (sender.Available > 0)
                    {
                        byte[] size = new byte[4];
                        
                        int rec = sender.Receive(size);
                        int len = BitConverter.ToInt32(size, 0);

                        if(len > 0 && len < 10000)
                        {
                            cam1_byte = new byte[len];
                            sender.Send(msg);
                            rec = sender.Receive(cam1_byte);
                            sender.Send(msg);
                            //cam1_byte.CopyTo(cam1_byte, 0);
                        }
                        else
                        {
                            sender.Send(msg);
                            sender.Receive(temp);
                            sender.Send(msg);
                        }
                        
                        //sender.Send(msg);

                        frameRecieved1 = true;
                        frameCount++;
                        //renderPicture1();
                    }
                    
                    //Thread.Sleep(50);
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
        private void newmethod(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                if (cam_number == 1) btn_start_client_1.Enabled = false;
                if (cam_number == 2) btn_start_client_2.Enabled = false;
                if (cam_number == 3) btn_start_client_3.Enabled = false;
            }));

            TcpClient client = new TcpClient();
            try
            {
                // соединяемся с сервером
                client.Connect(textbox_host_1.Text, port);

                cam1_byte = new byte[8196];
                int request = 0;

                NetworkStream stream = client.GetStream();
                
                do
                {
                   request = stream.Read(cam1_byte, 0, client.ReceiveBufferSize);

                    frameRecieved1 = true;
                    frameCount++;

                } while (stream.CanRead);
                renderPicture1();
                stream.Close();
               
            }
            catch (SocketException ex)
            {
            }
            finally
            {
                client.Close();
            }
        }
        private void reconnect(int port, int cam_number)
        {
            BeginInvoke(new MethodInvoker(delegate {
                if (cam_number == 1) btn_start_client_1.Enabled = false;
                if (cam_number == 2) btn_start_client_2.Enabled = false;
                if (cam_number == 3) btn_start_client_3.Enabled = false;
            }));

            try
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                byte[] message = Encoding.UTF8.GetBytes("ok");
                    
                //while (isWorkClient_1)
                //{
                    using (TcpClient client = new TcpClient())
                    {
                        client.Connect(textbox_host_1.Text, port);
                        NetworkStream stream = client.GetStream();
                        byte[] size = new byte[4];
                        stream.Read(size, 0, size.Length);
                        int aval = client.Available;
                        int length = BitConverter.ToInt32(size, 0);
                        int buf = client.ReceiveBufferSize;
                        
                        cam1_byte = new byte[length];
                        
                        stream.Write(message, 0, message.Length);
                        stream.Read(cam1_byte, 0, length);
                        frameRecieved1 = true;
                        frameCount++;

                        renderPicture1();

                        stream.Close();
                    }
                //}
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
                
                
                //cam1_render = new byte[8196];

                
                BeginInvoke(new MethodInvoker(delegate
                {
                    pictureBox1.Image = Image.FromFile("1.png");
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                byte[] message = Encoding.UTF8.GetBytes("ok");
                byte[] buffer;
                object locker = new object();
                NetworkStream stream = client.GetStream();
                
                BinaryReader br_Reader = new BinaryReader(stream);
                BinaryWriter bw_Writer = new BinaryWriter(stream);
                while (isWorkClient_1)
                {
                    byte[] mms = BitConverter.GetBytes(9);

                    bw_Writer.Write(mms);
                    bw_Writer.Flush();
                    Thread.Sleep(10);
                    byte[] size = new byte[4];

                    stream.Read(size, 0, size.Length);

                    int buf = client.ReceiveBufferSize;
                    int length = BitConverter.ToInt32(size, 0);
                    //cam1_byte = new byte[length];
                    buffer = new byte[length];

                    stream.Write(mms, 0, 4);
                    stream.Read(buffer, 0, length);

                    //cam1_byte = null;
                    //byte[] size = new byte[4];
                    //int b_rec = br_Reader.Read(size, 0, 4);
                    //int len = BitConverter.ToInt32(size, 0);
                    //cam1_byte = new byte[len];
                    //b_rec = br_Reader.Read(cam1_byte, 0, len);

                    //frameRecieved1 = true;
                    //frameCount++;

                    //buffer.CopyTo(cam1_byte, 0);
                    //MemoryStream ms = new MemoryStream(buffer);
                    //pictureBox4.Image = Image.FromStream(ms);

                    //buffer = null;
                    //cam1_byte = null;
                    //pictureBox4.Image = image1FromForm;
                    //renderPicture1();
                    //}
                    Thread.Sleep(50);
                }
                //stream.Close();

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
                    //renderPicture2();
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
            //pictureBox4.Image = image1FromForm;
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
            Thread tcpClientThread_1 = new Thread(udpClient);
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
        public static byte[] Serialize(Snapshot snap)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, snap);
            return stream.ToArray();
        }
        public static Snapshot Deserialize(byte[] binaryData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(binaryData);
            return (Snapshot)formatter.Deserialize(ms);
        }

        public static string GetMd5Hash(byte[] input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(input);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        public static bool VerifyMd5Hash(byte[] input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash)) return true;
            else return false;
        }
    }

    public class receiveServer : Form1
    {
        TcpListener tcp_Listener;
        TcpClient tcp_Client;
        Thread thr_Server;
        NetworkStream ns_Stream;
        BinaryReader br_Reader;
        BinaryWriter bw_Writer;
        delegate void del_function(Byte[] img);
        Boolean b_Connected = false;
        Boolean b_ServerStarted = false;

        public void Start(int port)
        {
            thr_Server = new Thread(new ParameterizedThreadStart(StartServerListening));
            thr_Server.Start(port);
        }
        public void Start()
        {
            thr_Server = new Thread(new ParameterizedThreadStart(StartServerListening));
            thr_Server.Start(11100);
        }
        public void Stop()
        {
            tcp_Listener.Stop();
            thr_Server.Abort();
        }
        private void StartServerListening(object port)
        {
            b_ServerStarted = true;
            tcp_Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), (int)port);
            tcp_Listener.Start();
            tcp_Client = tcp_Listener.AcceptTcpClient();
            ns_Stream = new NetworkStream(tcp_Client.Client);
            del_function ndel_CS = new del_function(v_Connect);
            
            
            b_Connected = true;

            Int32 lala = 7779;
            Byte[] bbb = BitConverter.GetBytes(lala);
            int le = bbb.Length;

            while (b_Connected == true)
            {
                if (tcp_Client.Available > 0)
                {
                    //cam1_byte = new Byte[tcp_Client.Available];

                    ns_Stream = tcp_Client.GetStream();

                    Byte[] ba_size = new Byte[4];
                    ns_Stream.Read(ba_size, 0, 4);
                    ns_Stream.Write(ba_size, 0, 4);
                    Byte[] ba_receive = new Byte[BitConverter.ToInt32(ba_size,0)];
                    ns_Stream.Read(ba_receive, 0, BitConverter.ToInt32(ba_size, 0));
                    ns_Stream.Flush();

                    Invoke(ndel_CS, new object[] { ba_receive });

                    //this.Invoke(ndel_CS, new object[] { "", true });

                    //br_Reader = new BinaryReader(ns_Stream);
                    //Byte[] bm_Size = br_Reader.ReadBytes(4);
                    //Int32 i_Size = BitConverter.ToInt32(bm_Size,0);
                    //Byte[] bm_Data = br_Reader.ReadBytes(i_Size);
                    //sn.Camera_length = i_Size;
                    //sn.Camera_byte = bm_Data;
                    //cam1_byte = new Byte[i_Size];
                    //cam1_byte = bm_Data;
                }
                
                
                Thread.Sleep(5);
            }
        }

        void v_Connect(Byte[] img)
        {
            MemoryStream ms1 = new MemoryStream(img);
            pictureBox4.Image = Image.FromStream(ms1);
        }
    }

    public class Snapshot : Form1
    {
        private byte[] camera_byte;
        private int camera_length;
        public bool isEndOfRender; // окончен ли рендеринг

        private string md5Hash;

        // set get
        public int Camera_length
        {
            get { return camera_length; }
            set { camera_length = value; }
        }
        public byte[] Camera_byte
        {
            get { return camera_byte; }
            set
            {
                lock (this)
                {
                    camera_byte = value;
                    md5Hash = GetMd5Hash(camera_byte); // считаем хэш
                }
            }
        }
        public bool Avaliable
        {
            get
            {
                return isEndOfRender;
            }
            set
            {
                isEndOfRender = value;
            }
        }

        // конструктор
        public Snapshot()
        {
            camera_length = 0;
            camera_byte = null;
            isEndOfRender = true;
            md5Hash = "";
        }
    }
}

