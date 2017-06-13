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
        private int port = 11100;
        private int width = 256;
        private int heigh = 256;
        private Stream imagestream;
        private Server server;
        private Size size; // тут храним разрешение 
        private bool isWorkClient = true;
        private bool frameRecieved = false;

        public int frameCount = 0; // счетчик принятых кадров
        public byte[] cam1_byte; // массивы для буфера с камер
        private byte[] cam1_render;
        public byte[] cam2_byte;
        public byte[] cam3_byte;

        public Form1()
        {
            InitializeComponent();
            server = new Server();
            size = new Size(width, heigh);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            textbox_host.Text = host;
            textBox_port.Text = port.ToString();
            btn_stop_client.Enabled = false;

            server.ImagesSource = ImageRecieve(size.Width,size.Height);
            server.Start();
        }

        private void tcpClient()
        {
            recieveImage(Convert.ToInt32(textBox_port.Text));
        }

        /// <summary>
        /// Функция передачи изображения для создания видеопотока
        /// </summary>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <returns></returns>
        private IEnumerable<Image> ImageRecieve(int width, int height) // 1280x720
        {
            while (true)
            {
                yield return encode(cam1_render);
            }
        }
        private Image encode(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void showPicture(byte[] byteImage)
        {
            if (frameRecieved == true)
            {
            }
        }

        private void recieveImage(int port)
        {
            BeginInvoke(new MethodInvoker(delegate {
                toolStripProgressBar1.Value = 50;
                btn_start_client.Enabled = false;
            }));

                MemoryStream ms = new MemoryStream(byteImage);
                var image = Image.FromStream(ms);
                pictureBox1.Image = image;
                frameRecieved = false;
            int i = 0;
            int request = 0;
            NetworkStream stream = null;
            TcpClient client = new TcpClient();
            try
            {
                // соединяемся с сервером
                client.Connect(textbox_host.Text, Convert.ToInt32(textBox_port.Text));

                BeginInvoke(new MethodInvoker(delegate
                {
                    if (client.Connected)
                    {
                        btn_start_client.Enabled = false;
                        btn_stop_client.Enabled = true;
                        statusConnect.Text = "Подключен к серверу";
                        logMessage += ("Connected! " + Environment.NewLine);
                        toolStripProgressBar1.Value = 100;
                    }
                    else
                    {
                        toolStripProgressBar1.Value = 0;
                        btn_start_client.Enabled = true;
                    }
                }));

                stream = client.GetStream();

                cam1_byte = new byte[8192*10];
                cam1_render = new byte[8192*10];
                byte[] size = new byte[1024*10];

                BeginInvoke(new MethodInvoker(delegate
                {
                    logMessage += ("получили поток! " + Environment.NewLine);
                }));

                while (isWorkClient)
                {

                    //cam1_render = new byte[client.ReceiveBufferSize];
                    //for (int j = 0; j < 10; j++)
                    //{


                    //}
                    //request = stream.Read(size, 0, client.ReceiveBufferSize);

                    //int sizeofbytes = BitConverter.ToInt32(size, 0);

                    request = stream.Read(cam1_byte, 0, client.ReceiveBufferSize);
                    cam1_render = cam1_byte;
                    //cam1_render = cam1_byte;
                    
                    frameRecieved = true;
                    //imagestream.WriteAsync(cam1_byte, 0, client.ReceiveBufferSize);
                    //string message = Encoding.UTF8.GetString(cam1_byte, 0, request);
                    //if (message == "stop") break;
                    //File.WriteAllBytes("cam/camera_image_" + i + ".jpg", cam1_byte);
                    //i++;
                    frameCount++;
                    //Thread.Sleep(1000);
                }
                stream.Close();

                BeginInvoke(new MethodInvoker(delegate
                {
                    statusConnect.Text = "Не подключен ";
                    toolStripProgressBar1.Value = 0;
                    btn_start_client.Enabled = true;
                    btn_stop_client.Enabled = false;
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
                    btn_start_client.Enabled = true;
                }));
                client.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            logBox.Text = logMessage;
            label2.Text = frameCount.ToString();
            showPicture(cam1_byte);
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
            FormShowImage newForm = new FormShowImage(this);
            newForm.Show();
        }

        private void btn_clear_log_Click(object sender, EventArgs e)
        {
            logMessage = "";
        }
        //-----------------------End-of-buttons--------------------------//
    }
}
