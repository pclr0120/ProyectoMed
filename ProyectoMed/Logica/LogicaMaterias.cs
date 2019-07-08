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
   public class LogicaMaterias
    {




        public bool Eliminar(int grado)
        {




            try
            {

                MessageBoxResult result0 = System.Windows.MessageBox.Show("Desea Eliminar todas las preguntas del tablero grado " + grado.ToString() + "?",
                                              "Notificación",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                if(result0 == MessageBoxResult.Yes)
                {

                    string gradoRuta = "";
                    if(grado == 1)
                    {
                        gradoRuta = "m1.txt";
                    }
                    else if(grado == 2)
                        gradoRuta = "m2.txt";
                    else if(grado == 3)
                        gradoRuta = "m3.txt";
                    else if(grado == 4)
                        gradoRuta = "m4.txt";


                    string docPath = @"C:\Contenedor\recursos\";

                    using(System.IO.StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, gradoRuta)))
                    {



                    }
                    string ruta = @"C:\Contenedor\recursos\" + gradoRuta;
                    if(File.Exists(ruta) ? true : false)
                    {

                        MessageBoxResult result = System.Windows.MessageBox.Show("Configuracion salvada correctamente!",
                                                      "Notificación",
                                                      MessageBoxButton.OK,
                                                      MessageBoxImage.Question);
                        return true;
                    }
                    else
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                 "Error",
                                                 MessageBoxButton.OK,
                                                 MessageBoxImage.Question);

                    }
                }
                return false;

            }
            catch(Exception err)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                 "Error",
                                                 MessageBoxButton.OK,
                                                 MessageBoxImage.Question);
                return false;
            }


        }

        public List<Materias> GetImport(int grado)
        {
            try
            {

                string gradoRuta = "";
                if(grado == 1)
                {
                    gradoRuta = "m1.txt";
                }
                else if(grado == 2)
                    gradoRuta = "m2.txt";
                else if(grado == 3)
                    gradoRuta = "m3.txt";
                else if(grado == 4)
                    gradoRuta = "m4.txt";
                string[] _Preguntas;

                List<Materias> ListaPreguntasTxt = new List<Materias>();
                string ruta = @"C:\Contenedor\recursos\" + gradoRuta;
                if(File.Exists(ruta) ? true : false)
                {
                    _Preguntas = System.IO.File.ReadAllLines(ruta, System.Text.Encoding.UTF8);
                    for(int i = 0; i < _Preguntas.Length; i++)
                    {
                        char[] delimiters = new char[] { '\t' };
                        // string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        string[] _Pregunta;
                        _Pregunta = _Preguntas[i].ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        Materias Pre = new Materias();

                        // Pre.Id = _Pregunta[0];
                        Pre.Grado = int.Parse(_Pregunta[0]);
                        Pre.Nombre = _Pregunta[1];


                 
                        Pre.Fecha = _Pregunta[2];
                        Pre.Estatus = bool.Parse(_Pregunta[3]);

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

        private List<Materias> Guardar(List<Materias> ListaActulizar, List<Materias> ListaTotal)
        {

            try
            {
                List<Materias> Listtemp = ListaTotal;

                for(int i = 0; i < ListaActulizar.Count; i++)
                {
                    if(ListaTotal.Remove(ListaActulizar[i]))
                    {

                        ListaTotal.Add(ListaActulizar[i]);
                    }
                    else
                        ListaTotal.Add(ListaActulizar[i]);
                }

            }
            catch(Exception e)
            {
                MessageBox.Show("Error al Guardar:" + e.ToString(), "Notificacion");
                return null;
            }
            return ListaTotal;
        }
        public bool GuardarTxt(List<Materias> listaActulizar, List<Materias> ListaPreguntasTotal, int grado)
        {     /// guarda todo los cambios      
            string gradoRuta = "";
            if(grado == 1)
            {
                gradoRuta = "m1.txt";
            }
            else if(grado == 2)
                gradoRuta = "m2.txt";
            else if(grado == 3)
                gradoRuta = "m3.txt";
            else if(grado == 4)
                gradoRuta = "m4.txt";


            List<Materias> Lista = new List<Materias>();
            List<string> ListaTemp = new List<string>();
            Lista = this.Guardar(listaActulizar, ListaPreguntasTotal);
            string docPath = @"C:\Contenedor\recursos\";
            //Directory.CreateDirectory(docPath);
            //string[] DataWrite = null;
            for(int i = 0; i < Lista.Count; i++)
            {
                ListaTemp.Add(Lista[i].Grado.ToString() + '\t' + Lista[i].Nombre + '\t' + Lista[i].Fecha.ToString() + '\t' + Lista[i].Estatus.ToString());
            }
            using(System.IO.StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, gradoRuta)))
            {

                foreach(string i in ListaTemp)
                {
                    outputFile.WriteLine(i);
                }

            }
            string ruta = @"C:\Contenedor\recursos\" + gradoRuta;
            if(File.Exists(ruta) ? true : false)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Configuracion salvada correctamente!",
                                              "Notificación",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Question);
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
