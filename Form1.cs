using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavegadorD
{ 
    public partial class Form1 : Form
    {
        List<URL> listaHistorial = new List<URL>();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonIr_Click(object sender, EventArgs e)
        {
            //if(comboBox1.SelectedItem != null)
            //   webBrowser1.Navigate(new Uri(comboBox1.SelectedItem.ToString()));

           /* if (comboBox1.Text.ToString().Contains("http://www.") && comboBox1.Text.ToString().Contains(".")) 
                webBrowser1.Navigate(new Uri(comboBox1.SelectedItem.ToString()));

            else if (comboBox1.Text.ToString().Contains(".")==false)
                webBrowser1.Navigate(new Uri("https://www.google.com/search?q=" + comboBox1.Text));

            else if (comboBox1.Text.ToString().Contains("www")&& comboBox1.Text.ToString().Contains("."))
                webBrowser1.Navigate(new Uri("https://"+comboBox1.Text.ToString()));

            if (comboBox1.Items.Contains(comboBox1.Text) == false)
                guardarHistorial(comboBox1.Text.ToString());*/

            string uri = "";
            if (comboBox1.Text != null)
                uri = comboBox1.Text;
            else if (comboBox1.SelectedItem != null)
                uri = comboBox1.SelectedItem.ToString();
            if (!uri.Contains("."))
                uri = "https://www.bing.com/search?q=" + uri;
            if (!uri.Contains("https://"))
                uri = "https://" + uri;

            webBrowser1.Navigate(new Uri(uri));

            int yaEsta = 0;
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString() == uri)
                    yaEsta++;
            }

            if (yaEsta == 0)
            {
                comboBox1.Items.Add(uri);
                guardarHistorial();
            }

            DateTime fechaActual = DateTime.Now;

            if (comboBox1.Items.Contains(comboBox1.Text) == false)
            {
                guardarLista(comboBox1.Text, 1, fechaActual);
            }
            else
            {
                URL uri2 = listaHistorial.Find(x => x.Direcccion == comboBox1.Text);
                uri2.NoVisitadas = uri2.NoVisitadas + 1;
                uri2.UltimoAcceso = fechaActual;
                guardarHistorial();
            }

            comboBox1.Items.Clear();
            leerLista();

        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void haciaAtrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void haciaDelanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 0;
            webBrowser1.GoHome();
            //lectura del historial al iniciar el formulario
            leerHistorial("historial.txt");
            leerLista();
        }

        private void guardarHistorial()
        {
            //guardar o crear/leer historial
            string nombreArchivo = "historial.txt";
            FileStream stream = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write);
            StreamWriter escribir = new StreamWriter(stream);

            foreach (var uri in listaHistorial)
            {
                escribir.WriteLine(uri.Direcccion);
                escribir.WriteLine(uri.NoVisitadas);
                escribir.WriteLine(uri.UltimoAcceso);


            }
            escribir.Close();
            stream.Close();

        }

        private void leerHistorial(string nombreArchivo) 
        {
            //string nombreArchivo = "historial.txt";
            //lectura del historial
            FileStream stream = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() >-1)
            {
                //string texto = reader.ReadLine();
                //comboBox1.Items.Add(texto);
                URL datosUri = new URL();
                datosUri.Direcccion = reader.ReadLine();
                datosUri.NoVisitadas = Convert.ToInt16(reader.ReadLine());
                datosUri.UltimoAcceso = Convert.ToDateTime(reader.ReadLine());
                listaHistorial.Add(datosUri);
            }
            reader.Close();
            stream.Close();
        }

        private void leerLista()
        {
            foreach (var uri in listaHistorial)
            {
                comboBox1.Items.Add(uri.Direcccion);
            }
        }

        private void guardarLista(string direccion,int novisitadas, DateTime fecha)
        {
            URL url = new URL();
            url.Direcccion = direccion;
            url.NoVisitadas = novisitadas;
            url.UltimoAcceso = fecha;
            listaHistorial.Add(url);

            guardarHistorial();
        }

        private void masVisitadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listaHistorial = listaHistorial.OrderByDescending(x => x.NoVisitadas).ToList();
            comboBox1.Items.Clear();
            leerLista();
        }

        private void masRecientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listaHistorial = listaHistorial.OrderByDescending(x => x.UltimoAcceso).ToList();
            comboBox1.Items.Clear();
            leerLista();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Contains(comboBox1.Text))
            {
                comboBox1.Items.Contains(comboBox1.Text);
                listaHistorial.RemoveAll(x => x.Direcccion == comboBox1.Text);
                File.Delete("historial.txt");
                guardarHistorial();
                comboBox1.Items.Clear();
                leerLista();

            }

        }
    }
}
