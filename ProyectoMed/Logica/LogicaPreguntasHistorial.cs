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
   public  class LogicaPreguntasHistorial
    {
        private string rutaPreguntas = @"C:\TableroConfiguracion\Enjuego\P\";
        public List<Pregunta> GetImport1(int grado)
        {
            try
            {

                string gradoRuta = "";
                if(grado == 1)
                {
                    gradoRuta = "p1.txt";
                }
                else if(grado == 2)
                    gradoRuta = "p2.txt";
                else if(grado == 3)
                    gradoRuta = "p3.txt";
                else if(grado == 4)
                    gradoRuta = "p4.txt";
                string[] _Preguntas;

                List<Pregunta> ListaPreguntasTxt = new List<Pregunta>();
                string ruta = this.rutaPreguntas + gradoRuta;
                if(File.Exists(ruta) ? true : false)
                {
                    _Preguntas = System.IO.File.ReadAllLines(ruta, System.Text.Encoding.UTF8);
                    for(int i = 0; i < _Preguntas.Length; i++)
                    {
                        char[] delimiters = new char[] { '\t' };
                        // string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        string[] _Pregunta;
                        _Pregunta = _Preguntas[i].ToString().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        Pregunta Pre = new Pregunta();

                        // Pre.Id = _Pregunta[0];
                        Pre.Grado = int.Parse(_Pregunta[0]);
                        Pre.Materia = _Pregunta[1];


                        Pre.Nivel = int.Parse(_Pregunta[2]);
                        Pre.Descripcion = _Pregunta[3];
                        Pre.R1 = _Pregunta[4];
                        Pre.R2 = _Pregunta[5];
                        Pre.R3 = _Pregunta[6];
                        Pre.Rc = _Pregunta[7];
                        //if (_Pregunta[8].Equals("true"))
                        Pre.Estatus = bool.Parse(_Pregunta[8]);
                        Pre.Id = _Pregunta[9];
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

        private List<Pregunta> Guardar(List<Pregunta> ListaPreguntasActualizar, List<Pregunta> ListaPreguntasTotal)
        {

            try
            {
                List<Pregunta> Listtemp = ListaPreguntasTotal;

                for(int i = 0; i < ListaPreguntasActualizar.Count; i++)
                {
                    if(ListaPreguntasTotal.RemoveAll(item=>item.Id==ListaPreguntasActualizar[i].Id)>0)
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



        public bool GuardarTxt(List<Pregunta> listaActulizar, List<Pregunta> ListaPreguntasTotal, int grado)
        {     /// guarda todo los cambios      
            string gradoRuta = "";
            if(grado == 1)
            {
                gradoRuta = "p1.txt";
            }
            else if(grado == 2)
                gradoRuta = "p2.txt";
            else if(grado == 3)
                gradoRuta = "p3.txt";
            else if(grado == 4)
                gradoRuta = "p4.txt";


            List<Pregunta> Lista = new List<Pregunta>();
            List<string> ListaTemp = new List<string>();
            if(GetImport1(grado).Count > 0)
                Lista = this.Guardar(listaActulizar, GetImport1(grado));
            else
                Lista = listaActulizar;
            string docPath = this.rutaPreguntas;
            //Directory.CreateDirectory(docPath);
            //string[] DataWrite = null;
            for(int i = 0; i < Lista.Count; i++)
            {
                ListaTemp.Add(Lista[i].Grado.ToString() + '\t' + Lista[i].Materia + '\t' + Lista[i].Nivel.ToString() + '\t' + Lista[i].Descripcion.ToString() + '\t' + Lista[i].R1.ToString() + '\t' + Lista[i].R2.ToString() + '\t' + Lista[i].R3.ToString() + '\t' + Lista[i].Rc.ToString() + '\t' + Lista[i].Estatus.ToString() + '\t' + Lista[i].Id.ToString());
            }
            using(System.IO.StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, gradoRuta)))
            {

                foreach(string i in ListaTemp)
                {
                    outputFile.WriteLine(i);
                }

            }
            string ruta = this.rutaPreguntas + gradoRuta;
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
