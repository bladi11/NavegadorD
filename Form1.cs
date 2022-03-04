﻿using System;
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
    List<URL> listaHistorial = new List<URL>(); 
    public partial class Form1 : Form
    {
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
                uri = "https://www.google.com/search?q=" + uri;
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
                guardarHistorial("historial.txt", uri);
            }

            DateTime fechaActual = DateTime.Now;

            //comboBox1.Items.Clear();
            //leerHistorial();

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
        }

        private void guardarHistorial(string nombreArchivo, string texto)
        {
            //guardar o crear/leer historial
            FileStream stream = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write);
            StreamWriter escribir = new StreamWriter(stream);

            escribir.WriteLine(texto);
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
                string texto = reader.ReadLine();
                comboBox1.Items.Add(texto);
            }
            reader.Close();
        }
    }
}
