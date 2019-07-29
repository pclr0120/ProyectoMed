using ProyectoMed.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProyectoMed.Logica
{
   public class LogicaHistorialEquipos
    {

        private string rutaEquipos = @"C:\Program Files (x86)\PCLR\Instalador_Tablero_Preguntas_V_1_0\TableroConfiguracion\Enjuego\E\";

        public List<Equipo> GetImportEequipos(int grado)
        {
            try
            {

                string gradoRuta = "";
                if(grado == 1)
                {
                    gradoRuta = "e1.txt";
                }
                else if(grado == 2)
                    gradoRuta = "e2.txt";
                else if(grado == 3)
                    gradoRuta = "e3.txt";
                else if(grado == 4)
                    gradoRuta = "e4.txt";
                string[] _Preguntas;

                List<Equipo> ListaPreguntasTxt = new List<Equipo>();
                string ruta = this.rutaEquipos + gradoRuta;
                if(File.Exists(ruta) ? true : false)
                {
                    _Preguntas = System.IO.File.ReadAllLines(ruta, System.Text.Encoding.UTF8);
                    for(int i = 0; i < _Preguntas.Length; i++)
                    {
                        char[] delimiters = new char[] { '\t' };
                        // string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        string[] _Pregunta;
                        _Pregunta = _Preguntas[i].ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        Equipo Pre = new Equipo();

                        // Pre.Id = _Pregunta[0];
                        Pre.Grado = int.Parse(_Pregunta[0]);
                        Pre.Nombre = _Pregunta[1];


                        Pre.Puntaje = int.Parse(_Pregunta[2]);
                        Pre.Fecha = _Pregunta[3];
                        Pre.Estatus = bool.Parse(_Pregunta[4]);

                        //if (_Pregunta[8].Equals("true"))

                        //else
                        //    Pre.Estatus = false;
                        ListaPreguntasTxt.Add(Pre);
                    }
                }

                return ListaPreguntasTxt;
            }
            catch(Exception e)
            {
                MessageBox.Show("Formato del archivo no valido. Error:" + e.ToString(), "Notificacion");
                return null;
            }

        }

        private List<Equipo> Guardar(List<Equipo> ListaPreguntasActualizar, List<Equipo> ListaPreguntasTotal)
        {

            try
            {
                List<Equipo> Listtemp = ListaPreguntasTotal;

                for(int i = 0; i < ListaPreguntasActualizar.Count; i++)
                {
                    if(ListaPreguntasTotal.Remove(ListaPreguntasActualizar[i]))
                    {

                        ListaPreguntasTotal.Add(ListaPreguntasActualizar[i]);
                    }
                    else
                        ListaPreguntasTotal.Add(ListaPreguntasActualizar[i]);
                }

            }
            catch(Exception e)
            {
                MessageBox.Show("Error al Guardar:" + e.ToString(), "Notificacion");
                return null;
            }
            return ListaPreguntasTotal;
        }
        public bool GuardarTxtEquipos(List<Equipo> listaActulizar, List<Equipo> ListaPreguntasTotal, int grado)
        {     /// guarda todo los cambios      
            string gradoRuta = "";
            if(grado == 1)
            {
                gradoRuta = "e1.txt";
            }
            else if(grado == 2)
                gradoRuta = "e2.txt";
            else if(grado == 3)
                gradoRuta = "e3.txt";
            else if(grado == 4)
                gradoRuta = "e4.txt";


            List<Equipo> Lista = new List<Equipo>();
            List<string> ListaTemp = new List<string>();
            Lista = this.Guardar(listaActulizar, ListaPreguntasTotal);
            string docPath = this.rutaEquipos;
            //Directory.CreateDirectory(docPath);
            //string[] DataWrite = null;
            for(int i = 0; i < Lista.Count; i++)
            {
                ListaTemp.Add(Lista[i].Grado.ToString() + '\t' + Lista[i].Nombre + '\t' + Lista[i].Puntaje.ToString() + '\t' + Lista[i].Fecha.ToString() + '\t' + Lista[i].Estatus.ToString());
            }
            File.Delete(docPath + gradoRuta);
            using(System.IO.StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, gradoRuta)))
            {
             
                foreach(string i in ListaTemp)
                {
                    outputFile.WriteLine(i);
                }

            }
            string ruta = this.rutaEquipos + gradoRuta;
            if(File.Exists(ruta) ? true : false)
            {

                //MessageBoxResult result = System.Windows.MessageBox.Show("Configuracion salvada correctamente!",
                //                              "Notificación",
                //                              MessageBoxButton.OK,
                //                              MessageBoxImage.Question);
                return true;
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                         "Error",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Question);

            }
            return false;
        }
    }
}
