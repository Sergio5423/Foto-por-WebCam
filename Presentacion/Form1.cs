using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private FilterInfoCollection Dispositivos;
        private VideoCaptureDevice FuenteDeVideo;
        private string path = @"C:\Users\starr\OneDrive\Documentos\Yo\Universidad\4to Semestre\Programación III\Camara\Presentacion\Imagenes\";

        private void Form1_Load(object sender, EventArgs e)
        {
            Dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Dispositivo in Dispositivos)
            {
                cbCamaras.Items.Add(Dispositivo.Name);
            }
            cbCamaras.SelectedIndex = 0;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            ApagarCamara();
            int i = cbCamaras.SelectedIndex;
            string NombreVideo = Dispositivos[i].MonikerString;
            FuenteDeVideo = new VideoCaptureDevice(NombreVideo);
            FuenteDeVideo.NewFrame += new NewFrameEventHandler(Capturando);
            FuenteDeVideo.Start();
        }

        private void ApagarCamara()
        {
            if(FuenteDeVideo != null && FuenteDeVideo.IsRunning)
            {
                FuenteDeVideo.SignalToStop();
                FuenteDeVideo = null;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ApagarCamara();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FuenteDeVideo != null && FuenteDeVideo.IsRunning)
            {
                pbImage2.Image = pbImage1.Image;
                pbImage2.Image.Save(path+"pueba.jpg", ImageFormat.Jpeg);
            }
        }

        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pbImage1.Image = Imagen;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApagarCamara();
        }
    }
}
