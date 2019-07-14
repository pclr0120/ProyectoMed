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
   public  class LogicaHistorialRonda
    {
        private string rutaEquipos = @"C:\TableroConfiguracion\Enjuego\R\";
        public List<Ronda> GetRondas(int grado)
        {
            try
            {

                string gradoRuta = "";
                if(grado == 1)
                {
                    gradoRuta = "r1.txt";
                }
                else if(grado == 2)
                    gradoRuta = "r2.txt";
                else if(grado == 3)
                    gradoRuta = "r3.txt";
                else if(grado == 4)
                    gradoRuta = "r4.txt";
                string[] _Preguntas;

                List<Ronda> ListaPreguntasTxt = new List<Ronda>();
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
                        Ronda Pre = new Ronda();

                      
                        Pre.Grado = int.Parse(_Pregunta[0]);
                        Pre.TotalRonda = int.Parse(_Pregunta[1]);


                        Pre.RondaActual = int.Parse(_Pregunta[2]);
                        Pre.Turno = int.Parse(_Pregunta[3]);

                     

                        Pre.Ganador = _Pregunta[4];
                        Pre.Equipo1 = _Pregunta[5];
                        Pre.Equipo1Puntaje = int.Parse(_Pregunta[6]);
                        Pre.Equipo2 = _Pregunta[7];
                        Pre.Equipo2Puntaje = int.Parse(_Pregunta[8]);
                        Pre.Fecha = _Pregunta[9];
                        Pre.Estatus = bool.Parse(_Pregunta[10]);
                        Pre.Id = _Pregunta[11];


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

        private List<Ronda> Guardar(List<Ronda> ListaPreguntasActualizar, List<Ronda> ListaPreguntasTotal)
        {

            try
            {
                List<Ronda> Listtemp = ListaPreguntasTotal;

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
        public bool GuardarTxtRonda(List<Ronda> listaActulizar, List<Ronda> ListaOriginal, int grado)
        {     /// guarda todo los cambios      
            string gradoRuta = "";
            if(grado == 1)
            {
                gradoRuta = "r1.txt";
            }
            else if(grado == 2)
                gradoRuta = "r2.txt";
            else if(grado == 3)
                gradoRuta = "r3.txt";
            else if(grado == 4)
                gradoRuta = "r4.txt";


            List<Ronda> Lista = new List<Ronda>();
            List<string> ListaTemp = new List<string>();
            Lista = this.Guardar(listaActulizar, ListaOriginal);
            string docPath = this.rutaEquipos;
            //Directory.CreateDirectory(docPath);
            //string[] DataWrite = null;
            for(int i = 0; i < Lista.Count; i++)
            {
                ListaTemp.Add(Lista[i].Grado.ToString() + '\t' + Lista[i].TotalRonda + '\t' + Lista[i].RondaActual.ToString() + '\t' + Lista[i].Turno.ToString() + '\t' + Lista[i].Ganador.ToString() + '\t' + Lista[i].Equipo1.ToString() + '\t' + Lista[i].Equipo1Puntaje.ToString() + '\t' + Lista[i].Equipo2.ToString() + '\t' + Lista[i].Equipo2Puntaje.ToString() + '\t' + Lista[i].Fecha.ToString() + '\t' + Lista[i].Estatus.ToString() + '\t' + Lista[i].Id.ToString());
            }
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
                LogicaExportarData le = new LogicaExportarData();
                List<Equipo> tem = new List<Equipo>();
                List<Equipo> LEquipo = new List<Equipo>();
                Equipo equipo = new Equipo();
                LogicaMarcador lm = new LogicaMarcador();
                tem =  le.GetImportEequipos(Lista[0].Grado);
                equipo = tem.Find(item=>item.Nombre== Lista[0].Equipo1);
                equipo.Id = Lista[0].Id;
                LEquipo.Add(equipo);
               
                equipo = tem.Find(item => item.Nombre == Lista[0].Equipo2);
                equipo.Id = Lista[0].Id;
                LEquipo.Add(equipo);

               lm.GuardarTxtEquipos(LEquipo,lm.GetImportEequipos(Lista[0].Grado), Lista[0].Grado);
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
