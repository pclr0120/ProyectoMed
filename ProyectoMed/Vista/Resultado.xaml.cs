using ProyectoMed.Logica;
using ProyectoMed.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoMed.Vista
{
    /// <summary>
    /// Lógica de interacción para Resultado.xaml
    /// </summary>
    public partial class Resultado : Page
    {
        List<Pregunta> ListaPreguntas;
        Pregunta Pregunta;
        List<Equipo> Equipos;
        string Res;
        int puntaje;
        int grado;
        public Resultado(Pregunta Pregunta,string Respuesta,int grado,int puntaje,List<Equipo> Equipos)
        {
            InitializeComponent();
            this.grado = grado;
            this.puntaje = puntaje;
            this.Pregunta = Pregunta;
            this.Res = Respuesta;
            this.Equipos = Equipos;
            this.Loaded += Resultado_Loaded;
        }

        private void Resultado_Loaded(object sender, RoutedEventArgs e)
        {
            LogicaPreguntasHistorial lh = new LogicaPreguntasHistorial();
            this.ListaPreguntas = lh.GetImport1(this.grado);
            Pregunta p=this.ListaPreguntas.Find(item => item.Id == Pregunta.Id);
            if (p.Nivel == 1)
                this.puntaje = 200;
            if (p.Nivel == 2)
                this.puntaje = 400;
            if (p.Nivel == 3)
                this.puntaje = 600;
            if (p.Nivel == 4)
                this.puntaje = 800;
            if (p.Nivel == 5)
                this.puntaje = 1000;

            if (p.Rc.Equals(this.Res))
            {
                this.Respusta.Content = "Tu repuesta fue correcta!";
                this.Puntos.Content = " Puntos obtenidos: +" + this.puntaje;
            }
            else {
                this.Respusta.Content = "Tu repuesta fue Incorrecta.";
                this.Puntos.Content = "Puntos obtenidos: 0"  ;
            }

          var a=  this.ListaPreguntas.Find(item => item.Id == Pregunta.Id).Estatus = false;
            var bb = this.ListaPreguntas.FindAll(i=>i.Estatus==false);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  Grados G = new Grados();
            this.rondaFinish();
        }

        public void rondaFinish() {
            try
            {
                LogicaHistorialRonda lr = new LogicaHistorialRonda();
                LogicaHistorialEquipos lh = new LogicaHistorialEquipos();
                List<Ronda> r = lr.GetRondas(this.grado);
                Ronda RondaActual = r.Find(i => i.Id == this.Equipos[0].Id);
                if(RondaActual.RondaActual == RondaActual.TotalRonda)
                {
                    RondaActual.Estatus = false; //desctivar
                    MessageBoxResult result = System.Windows.MessageBox.Show("Fin de las rondas",
                                                                           "Fin ronda",
                                                                          MessageBoxButton.OK,
                                                                           MessageBoxImage.Question);
              
                }
                else if((this.Equipos[0].Turno && this.Equipos[1].Turno == false && RondaActual.Turno == 0))
                {
                    this.Equipos[1].Turno = true;
                    RondaActual.Turno = 1;
               
                }
                else if(this.Equipos[0].Turno && this.Equipos[1].Turno == true && RondaActual.Turno == 1)
                {
                    this.Equipos[0].Turno = true;
                    this.Equipos[1].Turno = false;
                    RondaActual.Turno = 0;
                    RondaActual.RondaActual += 1;


                }
                List<Ronda> ListaRondaActual = new List<Ronda>();
             
                ListaRondaActual.Add(RondaActual);

                lr.GuardarTxtRonda(ListaRondaActual, r, this.grado);
                lh.GuardarTxtEquipos(this.Equipos, lh.GetImportEequipos(this.grado), this.grado);
                PageTablero t = new PageTablero(this.grado);
                this.NavigationService.Navigate(t);
            }
            catch(Exception)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                       "Error",
                                                       MessageBoxButton.OK,
                                                       MessageBoxImage.Question);
            }
          
                



        }
    }
}
