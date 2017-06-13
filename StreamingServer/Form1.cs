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

        private bool frameRecieved = false;

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
            server_cam_1 = new Server();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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

            server_cam_1.ImagesSource = ImageRecieve();
            server_cam_1.Start();
        }

        private void tcpClient()
        {
            recieveImage(Convert.ToInt32(textBox_port_1.Text));
        }

        /// <summary>
        /// Функция передачи изображения для создания видеопотока
        /// </summary>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <returns></returns>
        private IEnumerable<Image> ImageRecieve() // 1280x720
        {
            while (true)
            {
                MemoryStream ms = new MemoryStream(cam1_byte);
                returnImage = Image.FromStream(ms);
                yield return returnImage;
            }
        }

        private void renderPicture()
        {
            if (frameRecieved == true)
            {
                MemoryStream ms1 = new MemoryStream(cam1_render);
                image1FromForm = Image.FromStream(ms1);
                MemoryStream ms2 = new MemoryStream(cam2_render);
                image2FromForm = Image.FromStream(ms2);
                MemoryStream ms3 = new MemoryStream(cam3_render);
                image3FromForm = Image.FromStream(ms3);
                //pictureBox1.Image = image1FromForm;
                //f.picturebox1.Image = image;
                frameRecieved = false;
            }
        }

        private void recieveImage(int port)
        {
            BeginInvoke(new MethodInvoker(delegate {
                toolStripProgressBar1.Value = 50;
                btn_start_client_1.Enabled = false;
            }));
            
            int i = 0;
            int request = 0;
            NetworkStream stream = null;
            TcpClient client = new TcpClient();
            try
            {
                // соединяемся с сервером
                client.Connect(textbox_host_1.Text, Convert.ToInt32(textBox_port_1.Text));

                BeginInvoke(new MethodInvoker(delegate
                {
                    if (client.Connected)
                    {
                        btn_start_client_1.Enabled = false;
                        btn_stop_client_1.Enabled = true;
                        statusConnect.Text = "Подключен к серверу";
                        logMessage += ("Connected! " + Environment.NewLine);
                        toolStripProgressBar1.Value = 100;
                    }
                    else
                    {
                        toolStripProgressBar1.Value = 0;
                        btn_start_client_1.Enabled = true;
                    }
                }));

                stream = client.GetStream();

                cam1_byte = new byte[8192 * 10];
                cam2_byte = new byte[8192 * 10];
                cam3_byte = new byte[8192 * 10];
                cam1_render = new byte[8192*10];
                

                BeginInvoke(new MethodInvoker(delegate
                {
                    logMessage += ("получили поток! " + Environment.NewLine);
                }));

                while (isWorkClient)
                {


                    request = stream.Read(cam1_byte, 0, client.ReceiveBufferSize);
                    //request = stream.Read(cam2_byte, 0, client.ReceiveBufferSize);
                    //request = stream.Read(cam3_byte, 0, client.ReceiveBufferSize);
                    cam1_render = cam1_byte;
                    //cam2_render = cam2_byte;
                    //cam3_render = cam3_byte;
                    //cam1_render = cam1_byte;

                    frameRecieved = true;
                    

                    //File.WriteAllBytes("cam/camera_image_" + i + ".jpg", cam1_byte);

                    frameCount++;
                    //Thread.Sleep(1000);
                }
                stream.Close();

                BeginInvoke(new MethodInvoker(delegate
                {
                    statusConnect.Text = "Не подключен ";
                    toolStripProgressBar1.Value = 0;
                    btn_start_client_1.Enabled = true;
                    btn_stop_client_1.Enabled = false;
                    isWorkClient = false;
                }));
            }
            catch (SocketException ex)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    statusConnect.Text = "Ошибка подключения!";
                    toolStripProgressBar1.Value = 0;
                    logMessage += ex.ToString();
                }));
            }
            finally
            {
                BeginInvoke(new MethodInvoker(delegate {
                    btn_start_client_1.Enabled = true;
                }));
                client.Close();
            }
        }

        private void recieve(int port, byte[] byte_image, int cam_number)
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
                client.Connect(textbox_host_1.Text, Convert.ToInt32(textBox_port_1.Text));

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
                        toolStripProgressBar1.Value = 0;
                        btn_start_client_1.Enabled = true;
                    }
                }));

                NetworkStream stream = client.GetStream();

                byte_image = new byte[8192 * 10];
                
                
                BeginInvoke(new MethodInvoker(delegate
                {
                    logMessage += ("Cam " + cam_number + " connected!" + Environment.NewLine);
                }));

                while (isWorkClient_1)
                {
                    request = stream.Read(byte_image, 0, client.ReceiveBufferSize);
                    MemoryStream ms = new MemoryStream(byte_image);
                    image1FromForm = Image.FromStream(ms);
                }
                stream.Close();

                BeginInvoke(new MethodInvoker(delegate
                {
                    statusConnect.Text = "Не подключен ";
                    toolStripProgressBar1.Value = 0;
                    btn_start_client_1.Enabled = true;
                    btn_stop_client_1.Enabled = false;
                    isWorkClient = false;
                }));
            }
            catch (SocketException ex)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    statusConnect.Text = "Ошибка подключения!";
                    toolStripProgressBar1.Value = 0;
                    logMessage += ex.ToString();
                }));
            }
            finally
            {
                BeginInvoke(new MethodInvoker(delegate {
                    btn_start_client_1.Enabled = true;
                }));
                client.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            logBox.Text = logMessage;
            label2.Text = frameCount.ToString();
            //renderPicture();
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
        private void btn_start_client_Click(object sender, EventArgs e)
        {
            Thread tcpClientThread = new Thread(tcpClient);
            tcpClientThread.Start();
        }
        private void btn_stop_client_Click(object sender, EventArgs e)
        {
            isWorkClient = false;
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
        //-----------------------End-of-buttons--------------------------//
    }
}
