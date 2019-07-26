using ProyectoMed.Logica;
using ProyectoMed.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Lógica de interacción para Incorrecto.xaml
    /// </summary>
    public partial class Incorrecto : Page
    {
        List<Pregunta> ListaPreguntas;
        Pregunta Pregunta;
        List<Equipo> Equipos;
        string Res;
        int puntaje;
        int grado;
        public Incorrecto(Pregunta Pregunta, string Respuesta, int grado, int puntaje, List<Equipo> Equipos)
        {
            InitializeComponent();
            this.grado = grado;
            this.puntaje = puntaje;
            this.Pregunta = Pregunta;
            this.Res = Respuesta;
            this.Equipos = Equipos;
            this.Loaded += Resultado_Loaded;
        }
        SoundPlayer sonido;
        private void Resultado_Loaded(object sender, RoutedEventArgs e)
        {



            sonido = new SoundPlayer(@"c:\incorrecto.wav");
            sonido.Play();
            LogicaHistorialRonda LR = new LogicaHistorialRonda();
            Ronda RondaActual = LR.GetRondas(this.grado).Find(i => i.Id == this.Equipos[1].Id);
            if(this.Equipos[0].Turno == this.Equipos[1].Turno && RondaActual.Turno==0 )
            {
                this.InputTeam1.Content = this.Equipos[0].Nombre;
                this.InputTeam1Puntaje.Content = "+" + this.Equipos[0].Puntaje.ToString();
            }
            else
            {
                this.InputTeam1.Content = this.Equipos[1].Nombre;
                this.InputTeam1Puntaje.Content = "+" + this.Equipos[1].Puntaje.ToString();
            }


            LogicaPreguntasHistorial lh = new LogicaPreguntasHistorial();
            this.ListaPreguntas = lh.GetImport1(this.grado);
            Pregunta p = this.ListaPreguntas.Find(item => item.Id == Pregunta.Id);
            if(p.Nivel == 1)
                this.puntaje = 200;
            if(p.Nivel == 2)
                this.puntaje = 400;
            if(p.Nivel == 3)
                this.puntaje = 600;
            if(p.Nivel == 4)
                this.puntaje = 800;
            if(p.Nivel == 5)
                this.puntaje = 1000;
            this.lblCorrecta.Content = "R:" + this.Pregunta.Rc;
            Validacion V = new Validacion();
            this.lblCorrecta.FontSize = V.sizeLetraRespuestas(this.Res);
            this.Estado.Content = "TU RESPUESTA FUE: " + this.Res;
     
            this.Estado.FontSize = V.sizeLetraRespuestas(this.Res);

            this.Respusta.Content = "Tu respuesta fue Incorrecta.";
            this.Puntos.Content = "Puntos obtenidos: +0";


            var a = this.ListaPreguntas.Find(item => item.Id == Pregunta.Id).Estatus = false;
            var bb = this.ListaPreguntas.FindAll(i => i.Estatus == false);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  Grados G = new Grados();
            this.rondaFinish();
        }


        public void rondaFinish()
        {
            try
            {
                LogicaHistorialRonda lr = new LogicaHistorialRonda();
                LogicaMarcador lh = new LogicaMarcador();
                List<Ronda> r = lr.GetRondas(this.grado);
                Ronda RondaActual = r.Find(i => i.Id == this.Equipos[0].Id);
                if(RondaActual.RondaActual == RondaActual.TotalRonda && RondaActual.Turno == 1 && RondaActual.Estatus == false)
                {
                    RondaActual.Estatus = false; //desctivar
                   

                    if(this.Equipos[0].Puntaje != this.Equipos[1].Puntaje)
                    {
                        //List<Ronda> ListaRondaActual = new List<Ronda>();
                        //RondaActual.Estatus = false;
                        int ganador = -1;
                        if(this.Equipos[0].Puntaje > this.Equipos[1].Puntaje)
                        {
                            //RondaActual.Ganador = 0;
                            //this.Equipos[0].Ganador = true;
                            //this.Equipos[0].Estatus = false;
                            ganador = 0;
                        }

                        else
                        {
                            //this.Equipos[1].Estatus = false;
                            //this.Equipos[1].Ganador = true;
                            //RondaActual.Ganador = 1;
                            ganador = 1;
                        }
                        //ListaRondaActual.Add(RondaActual);

                        //lr.GuardarTxtRonda(ListaRondaActual, r, this.grado);
                        //lh.GuardarTxtEquipos(this.Equipos, lh.GetImportEequipos(this.grado), this.grado);
                        this.sonido.Stop();
                        this.sonido.Dispose();

                        PageGanador R = new PageGanador(this.Equipos, ganador);
                        this.NavigationService.Navigate(R);

                    }
                    else
                    if(this.Equipos[0].Puntaje == this.Equipos[1].Puntaje)
                    {
                        //List<Ronda> ListaRondaActual = new List<Ronda>();

                        //ListaRondaActual.Add(RondaActual);

                        //lr.GuardarTxtRonda(ListaRondaActual, r, this.grado);
                        //lh.GuardarTxtEquipos(this.Equipos, lh.GetImportEequipos(this.grado), this.grado);
                        this.sonido.Stop();
                        this.sonido.Dispose();

                        RondaEmpate R = new RondaEmpate(this.Pregunta, this.Res, this.grado, this.puntaje, this.Equipos);
                        this.NavigationService.Navigate(R);

                    }


                }
                else{
                    this.sonido.Stop();
                    this.sonido.Dispose();
                    PageTablero t = new PageTablero(this.grado);
                    this.NavigationService.Navigate(t);
                }
                //else if((this.Equipos[0].Turno && this.Equipos[1].Turno && RondaActual.Turno == 1)) ///los turnos se gurdan en la pagepregunta 
                //{
                //    //this.Equipos[1].Turno = true;
                //    //RondaActual.Turno = 1;
                //    //List<Ronda> ListaRondaActual = new List<Ronda>();

                //    //ListaRondaActual.Add(RondaActual);

                //    //lr.GuardarTxtRonda(ListaRondaActual, r, this.grado);
                //    //lh.GuardarTxtEquipos(this.Equipos, lh.GetImportEequipos(this.grado), this.grado);
                //    this.sonido.Stop();
                //    this.sonido.Dispose();
                //PageTablero t = new PageTablero(this.grado);
               // this.NavigationService.Navigate(t);

                //}
                //else if(this.Equipos[0].Turno && this.Equipos[1].Turno == false && RondaActual.Turno == 0)
                //{
                //    //this.Equipos[0].Turno = true;
                //    //this.Equipos[1].Turno = false;
                //    //RondaActual.Turno = 0;
                //    //RondaActual.RondaActual += 1;

                //    //List<Ronda> ListaRondaActual = new List<Ronda>();

                //    //ListaRondaActual.Add(RondaActual);

                //    //lr.GuardarTxtRonda(ListaRondaActual, r, this.grado);
                //    //lh.GuardarTxtEquipos(this.Equipos, lh.GetImportEequipos(this.grado), this.grado);
                //    this.sonido.Stop();
                //    this.sonido.Dispose();
                //    PageTablero t = new PageTablero(this.grado);
                //    this.NavigationService.Navigate(t);
                //}


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
