using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamingServer
{
    public partial class FormShowImage : Form
    {
        public PictureBox picturebox1;
        public PictureBox picturebox2;
        public PictureBox picturebox3;
        Form1 form;
        public FormShowImage()
        {
            InitializeComponent();
        }

        public FormShowImage(Form f)
        {
            InitializeComponent();
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = ((Form1)this.Tag).image1FromForm;
            pictureBox2.Image = ((Form1)this.Tag).image2FromForm;
            pictureBox3.Image = ((Form1)this.Tag).image3FromForm;
        }
    }
}
