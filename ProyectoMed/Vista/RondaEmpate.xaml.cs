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
    /// Lógica de interacción para RondaEmpate.xaml
    /// </summary>
    public partial class RondaEmpate : Page
    {
        List<Pregunta> ListaPreguntas;
        Pregunta Pregunta;
        List<Equipo> Equipos;
        string Res;
        int puntaje;
        int grado;
        int ganador=-1;
        public RondaEmpate(Pregunta Pregunta, string Respuesta, int grado, int puntaje, List<Equipo> Equipos)
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
            this.lbl1.Content = this.Equipos[0].Nombre + " puntos: "+ this.Equipos[0].Puntaje;
            this.lbl2.Content = this.Equipos[1].Nombre + " puntos: " + this.Equipos[1].Puntaje;
            this.tem1.Content = this.Equipos[0].Nombre;
            this.tem2.Content = this.Equipos[1].Nombre;



        }

        //public RondaEmpate(Pregunta Pregunta, string Respuesta, int grado, int puntaje, List<Equipo> Equipos) : this() {
        //    this.grado = grado;
        //    this.puntaje = puntaje;
        //    this.Pregunta = Pregunta;
        //    this.Res = Respuesta;
        //    this.Equipos = Equipos;
        //}

        bool Continuar() {
            LogicaHistorialRonda lr = new LogicaHistorialRonda();
            LogicaMarcador lh = new LogicaMarcador();

            List<Ronda> r = lr.GetRondas(this.grado);

            Ronda RondaActual = r.Find(i => i.Id == this.Equipos[0].Id);

            try
            {
                if(this.tem1.IsChecked == true || this.tem2.IsChecked == true)
                    if(this.tem1.IsChecked == true)
                    {
                        RondaActual.GaneManual = true;
                        RondaActual.Ganador = 0;
                        this.Equipos[0].Ganador = true;
                        this.ganador = 0;
                    }
                    else
                    {
                        this.ganador = 1;
                        RondaActual.GaneManual = true;
                        RondaActual.Ganador = 1;
                        this.Equipos[1].Ganador = true;
                    }
                else
                    MessageBox.Show("Debe de elegir un ganador para contunuar..");
                RondaActual.Estatus = false;
                List<Ronda> ListaRondaActual = new List<Ronda>();

                ListaRondaActual.Add(RondaActual);

                if(lr.GuardarTxtRonda(ListaRondaActual, r, this.grado))
                    return lh.GuardarTxtEquipos(this.Equipos, lh.GetImportEequipos(this.grado), this.grado);
                else
                    return false;
                
            }
            catch(Exception)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                       "Error",
                                                       MessageBoxButton.OK,
                                                       MessageBoxImage.Question);
                return false;
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(this.tem1.IsChecked == true || this.tem2.IsChecked == true)
            {
                if(this.Continuar())
                {
                    PageGanador t = new PageGanador(this.Equipos, this.ganador);
                    this.NavigationService.Navigate(t);
                }
                else
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                           "Error",
                                                           MessageBoxButton.OK,
                                                           MessageBoxImage.Question);
                }

            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Elija a un ganador para poder continuar...",
                                                          "Tablero",
                                                          MessageBoxButton.OK,
                                                          MessageBoxImage.Question);
            }

        }
    }
}
