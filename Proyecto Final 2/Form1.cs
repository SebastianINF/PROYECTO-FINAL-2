using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Proyecto_Final_2
{
    public partial class Form1 : Form
    {
        ArchivoAuxiliar a1, a2;
        int nr, llave;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            a1 = new ArchivoAuxiliar();
            a2 = new ArchivoAuxiliar();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            a1.Grabar(Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value),
                      Convert.ToString(dataGridView1.Rows[0].Cells[1].Value),
                      Convert.ToString(dataGridView1.Rows[0].Cells[2].Value),
                      Convert.ToString(dataGridView1.Rows[0].Cells[3].Value),
                      Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value),
                      Convert.ToInt32(dataGridView1.Rows[0].Cells[5].Value)
                      );
            dataGridView1.Rows.Clear();
            nr++;
            dataGridView1.Rows[0].Cells[0].Value = Convert.ToString(llave + nr);
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            if (nr > 1)
            {
                a1.Cerrar_Grabar();
                MessageBox.Show("Terminado con exito");
            }
            else
                MessageBox.Show("Grabar al menos un registro");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            textBox1.Clear();
        }

        private void seleccionarTipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string categoria = string.Concat(Interaction.InputBox("Costo Tipo/Categoria"));
            if (string.IsNullOrEmpty(categoria)) return; // validacion de cadena vacia o null
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return; // validacion
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return; // validacion
            a1.SeleccionarTipo(openFileDialog1.FileName, ref a2, saveFileDialog1.FileName, categoria);
        }

        private void seleccionarAñoIngresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int añoIngresoSeleccionar = int.Parse(Interaction.InputBox("Introducir año de Ingreso a seleccionar"));
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return; // validacion
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return; // validacion
            a1.SeleccionarAñoIngresoMayor(openFileDialog1.FileName, ref a2, saveFileDialog1.FileName, añoIngresoSeleccionar);
            MessageBox.Show($"TERMINADO \nAuxiliares Que Ingresaron Despues del Año {añoIngresoSeleccionar}");
        }

        private void salarioTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return; // validacion
            textBox1.Text = $"Salario Total: ${Math.Round(a1.SalarioTotal(openFileDialog1.FileName) / 6.96)} - {Math.Round(a1.SalarioTotal(openFileDialog1.FileName))}Bs";
        }

        private void leerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int llave = 0;
            string tipo = "";
            string nombre = "";
            string materia = "";
            int salario = 0;
            int añoIngreso = 0;
            bool est = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return; // validacion
            LimpiarDataGridView(); // borrar antes de escribir
            a1.Abrir_Leer(openFileDialog1.FileName);
            nr = -1;
            while (!a1.Verif_Fin())
            {
                a1.Leer(ref llave, ref tipo, ref nombre, ref materia, ref salario,ref añoIngreso, ref est);
                if (est)
                {
                    nr++;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[nr].Cells[0].Value = Convert.ToString(llave);
                    dataGridView1.Rows[nr].Cells[1].Value = Convert.ToString(tipo);
                    dataGridView1.Rows[nr].Cells[2].Value = Convert.ToString(nombre);
                    dataGridView1.Rows[nr].Cells[3].Value = Convert.ToString(materia);
                    dataGridView1.Rows[nr].Cells[4].Value = Convert.ToString(salario);
                    dataGridView1.Rows[nr].Cells[5].Value = Convert.ToString(añoIngreso);
                }
            }
            a1.Cerrar_Leer();
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() != DialogResult.OK) return; // validacion
            a1.Abrir_Grabar(saveFileDialog1.FileName);

            llave = Convert.ToInt32(Interaction.InputBox("Llavé: "));

            nr = 1;
            dataGridView1.Rows[0].Cells[0].Value = Convert.ToString(llave + nr);
        }

        public void LimpiarDataGridView()
        {
            dataGridView1.Rows.Clear();
        }
        //public void Altas()
        //{
        //    int llave = 0;
        //    string tipo = "";
        //    string nombre = "";
        //    string materia = "";
        //    int salario = 0;
        //    int añoIngreso = 0;
        //    bool est = true;

        //    // Leer
        //    openFileDialog1.ShowDialog();
        //    a1.Abrir_Leer(openFileDialog1.FileName);
        //    a1.Leer(ref llave, ref tipo, ref nombre, ref materia, ref salario, ref añoIngreso, ref est);
        //    llave = llave - 1;
        //    nr = a1.RetornarNumeroDeRegistros() + 1;
        //    a1.Cerrar_Leer();

        //    // Adicionar
        //    a1.Abrir_Adicionar(openFileDialog1.FileName);
        //    dataGridView1.Rows[0].Cells[0].Value = Convert.ToString(llave + nr);
        //}


    }
}
