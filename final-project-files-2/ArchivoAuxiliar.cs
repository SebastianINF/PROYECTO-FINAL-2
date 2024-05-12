using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace final_project_files_2
{
    class ArchivoAuxiliar
    {
        private string NArch;
        private FileStream stream;
        private BinaryWriter writer1;
        private BinaryReader reader1;

        public ArchivoAuxiliar()
        {
            NArch = "";
        }
        public void Abrir_Grabar(string Narch1)
        {
            NArch = Narch1;
            stream = new FileStream(NArch, FileMode.CreateNew, FileAccess.Write);
            writer1 = new BinaryWriter(stream);
        }
        // Grabar graba varias cosas a la ves
        public void Grabar(int llave, string tipo, string nombre, string materia, int salario, int añoIngreso) // <-- no va (bool est)
        {
            tipo = tipo.PadRight(28, ' ').Substring(0, 28);
            nombre = nombre.PadRight(28, ' ').Substring(0, 28);
            materia = materia.PadRight(28, ' ').Substring(0, 28);
            writer1.Write(llave);
            writer1.Write(tipo);
            writer1.Write(nombre);
            writer1.Write(materia);
            writer1.Write(salario);
            writer1.Write(añoIngreso);
            writer1.Write(true);
        }
        public void Leer(ref int llave, ref string tipo, ref string nombre, ref string materia, ref int salario, ref int añoIngreso, ref bool est)
        {
            llave = reader1.ReadInt32();
            tipo = reader1.ReadString();
            nombre = reader1.ReadString();
            materia = reader1.ReadString();
            salario = reader1.ReadInt32();
            añoIngreso = reader1.ReadInt32();
            est = reader1.ReadBoolean();
        }
        public void Cerrar_Grabar()
        {
            writer1.Close();
            stream.Close();
        }
        public void Abrir_Leer(string Narch1)
        {
            NArch = Narch1;
            stream = new FileStream(NArch, FileMode.Open, FileAccess.Read);
            reader1 = new BinaryReader(stream);
        }
        public void Cerrar_Leer()
        {
            reader1.Close();
            stream.Close();
        }
        public bool Verif_Fin()
        {
            return stream.Position == stream.Length;
        }

        public void SeleccionarTipo(string Narch1, ref ArchivoAuxiliar a2, string Narch2, string categoria)
        {
            int llave = 0;
            string tipo = "";
            string nombre = "";
            string materia = "";
            int salario = 0;
            int añoIngreso = 0;
            bool est = true;
            Abrir_Leer(Narch1);
            a2.Abrir_Grabar(Narch2);
            while (!Verif_Fin())
            {
                Leer(ref llave, ref tipo, ref nombre, ref materia, ref salario, ref añoIngreso, ref est);
                if (est && tipo.Trim() == categoria) 
                {
                    a2.Grabar(llave, tipo, nombre, materia, salario, añoIngreso);
                }
            }
            Cerrar_Leer();
            a2.Cerrar_Grabar();
        }

        public void SeleccionarAñoIngresoMayor(string Narch1, ref ArchivoAuxiliar a2, string Narch2, int añoIngresoMayor)
        {
            int llave = 0;
            string tipo = "";
            string nombre = "";
            string materia = "";
            int salario = 0;
            int añoIngreso = 0;
            bool est = true;
            Abrir_Leer(Narch1);
            a2.Abrir_Grabar(Narch2);
            while (!Verif_Fin())
            {
                Leer(ref llave, ref tipo, ref nombre, ref materia, ref salario, ref añoIngreso, ref est);
                if (est && añoIngreso > añoIngresoMayor)
                {
                    a2.Grabar(llave, tipo, nombre, materia, salario, añoIngreso);
                }
            }
            Cerrar_Leer();
            a2.Cerrar_Grabar();
        }

        public double SalarioTotal(string Narch1)
        {
            double salarioTotal = 0;
            int llave = 0;
            string tipo = "";
            string nombre = "";
            string materia = "";
            int salario = 0;
            int añoIngreso = 0;
            bool est = true;
            Abrir_Leer(Narch1);
            while (!Verif_Fin())
            {
                Leer(ref llave, ref tipo, ref nombre, ref materia, ref salario, ref añoIngreso, ref est);
                if (est)
                {
                    salarioTotal += salario;
                }
            }
            Cerrar_Leer();
            return salarioTotal;
        }

        // Mantenimiento
        public void Abrir_Adicionar(string Narch1)
        {
            NArch = Narch1;
            stream = new FileStream(NArch, FileMode.Append, FileAccess.Write);
            writer1 = new BinaryWriter(stream);
        }
        public void Cerrar_Adicionar()
        {
            writer1.Close();
            stream.Close();
        }
        public int RetornarNumeroDeRegistros()
        {
            return (int)stream.Length / 50;
        }
    }
}
