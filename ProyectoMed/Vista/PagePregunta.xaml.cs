﻿using ProyectoMed.Logica;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProyectoMed.Vista
{
    /// <summary>
    /// Lógica de interacción para PagePregunta.xaml
    /// </summary>
    public partial class PagePregunta : Page
    {
        private Pregunta pregunta;
        private List<Pregunta> ListaPreguntas;
        private List<Equipo> Equipos;
        private int Puntaje = 0;
        private string respuesta = "Se termino el tiempo";
        private string materia;
        private int grado;
        private int nivel;
        private string RondaID;
        private bool Correcta = false;
        private int TipoPregunta = 0;
        private SoundPlayer sonido;
        public PagePregunta()
        {
            InitializeComponent();


            this.Loaded += PagePregunta_Loaded;
            this.RespuestaUser.Visibility = Visibility.Hidden;
        }
        public PagePregunta(string materia, int grado, int nivel, List<Equipo> Equipos, string IDRonda) : this() {
            this.RondaID = IDRonda;
            this.materia = materia;
            this.grado = grado;
            this.nivel = nivel;
            this.Equipos = Equipos;
        }

        private int tipoPregunta(Pregunta pregunta) {


            int V1 = pregunta.Rc.ToUpper().IndexOf("VERDADERO");
            int V2 = pregunta.Rc.ToUpper().IndexOf("VERDAD");
            int V3 = pregunta.Rc.ToUpper().IndexOf("FALSO");
            int V4 = pregunta.Rc.ToUpper().IndexOf("CIERTO");

            if(V1 > -1 || V2 != -1 || V3 != -1 || V4 != -1)
            {
                this.R1.Visibility = Visibility.Visible;
                this.R2.Visibility = Visibility.Visible;
                this.R3.Visibility = Visibility.Hidden;
                this.R4.Visibility = Visibility.Hidden;
                if(pregunta.Rc.ToUpper() == "VERDADERO" || pregunta.Rc.ToUpper() == "VERDAD")
                {
                    this.R1.Content = "A:" + pregunta.Rc.ToUpper();
                    this.R2.Content = "B:Falso".ToUpper();
                }
                else {
                    this.R1.Content = "A:" + pregunta.Rc;
                    this.R2.Content = "B:Verdadero";
                }

                if(pregunta.Rc.ToUpper() == "CIERTO") {
                    this.R1.Content = "A:" + pregunta.Rc.ToUpper();
                    this.R2.Content = "B:CIERTO";
                }
                    return 1;
            }


            else if(pregunta.R1 == "" || pregunta.R2 == "" || pregunta.R3 == "" || pregunta.R1.ToUpper() == "N" || pregunta.R2.ToUpper() == "N" || pregunta.R3.ToUpper() == "N") {
                this.R1.Visibility = Visibility.Hidden;
                this.R2.Visibility = Visibility.Hidden;
                this.R3.Visibility = Visibility.Hidden;
                this.R4.Visibility = Visibility.Hidden;
                this.RespuestaUser.Visibility = Visibility.Visible;
                return 2;

            }
                
            return 0;
        }

        private void PagePregunta_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Correcta = false;
                this.TiempoIniciar();

                sonido = new SoundPlayer(@"C:\Program Files (x86)\PCLR\Instalador_Tablero_Preguntas_V_1_0\TableroConfiguracion\Sonido\Titac.wav");
                sonido.Play();
                LogicaPreguntasHistorial l = new LogicaPreguntasHistorial();
                LogicaHistorialRonda Lm = new LogicaHistorialRonda();
                Ronda ronda = Lm.GetRondas(this.grado).Find(item=>item.Id==this.Equipos[0].Id);
                List<Pregunta> listaPreguntas = l.GetImport1(this.grado).FindAll(i => i.Materia == materia && i.Nivel == this.nivel && i.Estatus == true);
                Random r = new Random();
                Validacion v = new Validacion();
                int random = r.Next(0, listaPreguntas.Count-1);
                this.respuesta = "null";
                if(ronda.Turno == 0)
                {
                    this.InputTeam1.Content = this.Equipos[0].Nombre;
                    this.InputTeam1Puntaje.Content = "+" + this.Equipos[0].Puntaje.ToString();
                    this.InputTeam1.FontSize = v.sizeLetraNameTeam(this.Equipos[0].Nombre);
                }
                else {
                    this.InputTeam1.Content = this.Equipos[1].Nombre;
                    this.InputTeam1.FontSize = v.sizeLetraNameTeam(this.Equipos[1].Nombre);
                    this.InputTeam1Puntaje.Content = "+" + this.Equipos[1].Puntaje.ToString();

                }
               
                Modelo.Pregunta Pr = listaPreguntas[random];
             

                this.pregunta = Pr;
                Pr.Estatus = false;
                string puntaje = "";
                if(nivel == 1)
                    puntaje = "200";
                if(nivel == 2)
                    puntaje = "400";
                if(nivel == 3)
                    puntaje = "600";
                if(nivel == 4)
                    puntaje = "800";
                if(nivel == 5)
                    puntaje = "1000";
                this.Puntaje = int.Parse(puntaje);
                this.Titulo.Content = (this.materia + " POR " + puntaje).ToUpper();

                this.Pregunta.Text = Pr.Descripcion;
                this.TipoPregunta = this.tipoPregunta(Pr);
                if( this.TipoPregunta== 0)
                {
                    Random r2 = new Random();
                    int rm2 = r2.Next(0, 4);
                    if(rm2 == 0)
                    {
                        this.R1.Content = "A:" + Pr.R1;
                        this.R2.Content = "B:" + Pr.R2;
                        this.R3.Content = "C:" + Pr.R3;
                        this.R4.Content = "D:" + Pr.Rc;
                    }
                    if(rm2 == 1)
                    {
                        this.R1.Content = "A:" + Pr.Rc;
                        this.R2.Content = "B:" + Pr.R2;
                        this.R3.Content = "C:" + Pr.R3;
                        this.R4.Content = "D:" + Pr.R1;
                    }
                    if(rm2 == 2)
                    {
                        this.R1.Content = "A:" + Pr.R2;
                        this.R2.Content = "B:" + Pr.Rc;
                        this.R3.Content = "C:" + Pr.R3;
                        this.R4.Content = "D:" + Pr.R1;
                    }
                    if(rm2 == 3)
                    {
                        this.R1.Content = "A:" + Pr.R2;
                        this.R2.Content = "B:" + Pr.R3;
                        this.R3.Content = "C:" + Pr.Rc;
                        this.R4.Content = "D:" + Pr.R1;
                    }
                    if(rm2 == 4)
                    {
                        this.R1.Content = "A:" + Pr.R2;
                        this.R2.Content = "B:" + Pr.R1;
                        this.R3.Content = "C:" + Pr.Rc;
                        this.R4.Content = "D:" + Pr.R3;
                    }
                }
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }


        }

        //public PagePregunta(PageTablero p) : this()

        //{
        //    var a = p;
        //    var c = p.Hola;


        //}



        private void R1_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                System.Windows.Controls.RadioButton r = sender as System.Windows.Controls.RadioButton;
                if(r.Name.Equals("R1"))
                {
                    r.Foreground = Brushes.Yellow;
                    this.R2.Foreground = Brushes.White;
                    this.R3.Foreground = Brushes.White;
                    this.R4.Foreground = Brushes.White;
                    this.respuesta = r.Content.ToString();
                }


                if(r.Name.Equals("R2"))
                {
                    r.Foreground = Brushes.Yellow;
                    this.R1.Foreground = Brushes.White;
                    this.R3.Foreground = Brushes.White;
                    this.R4.Foreground = Brushes.White;
                    this.respuesta = r.Content.ToString();
                }

                if(r.Name.Equals("R3"))
                {
                    r.Foreground = Brushes.Yellow;
                    this.R1.Foreground = Brushes.White;
                    this.R2.Foreground = Brushes.White;
                    this.R4.Foreground = Brushes.White;
                    this.respuesta = r.Content.ToString();
                }

                if(r.Name.Equals("R4"))
                {
                    r.Foreground = Brushes.Yellow;
                    this.R1.Foreground = Brushes.White;
                    this.R2.Foreground = Brushes.White;
                    this.R3.Foreground = Brushes.White;
                    this.respuesta = r.Content.ToString();
                }

            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }





        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if(this.TipoPregunta == 2)
                    this.respuesta = this.RespuestaUser.Text.ToUpper();
                if(this.respuesta!= "null" && this.respuesta != "Capture la respuesta".ToUpper())
                this.siguiente();
                else System.Windows.MessageBox.Show("No has seleccionado ninguna respuesta y aun queda tiempo", "Tablero");

            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }

        }

        void GuardarHisrotial (){
            try
            {

                
            LogicaExportarData LE = new LogicaExportarData();
            LogicaHistorialRonda LR = new LogicaHistorialRonda();
            LogicaMarcador Lm = new LogicaMarcador();
            Ronda RondaActual = LR.GetRondas(this.grado).Find(i=>i.Id==this.RondaID);
            List<Ronda> ListR  = new List<Ronda>();
                LogicaPreguntasHistorial lpp = new LogicaPreguntasHistorial();
                List<Pregunta> listActualizar = new List<Pregunta>();
                listActualizar.Add(this.pregunta);
                lpp.GuardarTxt(listActualizar, lpp.GetImport1(this.grado), this.grado);
                if(this.TipoPregunta < 2)
                {
                    string[] res = this.respuesta.Split(':');

                    this.respuesta = res[1];
                }
                if(RondaActual.RondaActual == RondaActual.TotalRonda && RondaActual.Turno == 1)
                {
                    RondaActual.Estatus = false;
                  
                }
                else
                if(this.respuesta.ToUpper() == this.pregunta.Rc.ToUpper() && RondaActual.Estatus)
                {
                    this.Correcta = true;
                    if(RondaActual.Turno == 0) 
                    {
                        RondaActual.Turno = 1;
                        this.Equipos[0].Turno = true;
                        this.Equipos[1].Turno = true;
                        RondaActual.Equipo1Puntaje += this.Puntaje;
                        this.Equipos[0].Puntaje += this.Puntaje;
                    }
                    else if(RondaActual.Turno == 1 )
                    {
                        RondaActual.Turno = 0;
                        this.Equipos[0].Turno = true;
                        this.Equipos[1].Turno = false;
                        RondaActual.Equipo2Puntaje += this.Puntaje;
                        this.Equipos[1].Puntaje += this.Puntaje;
                        RondaActual.RondaActual += 1;

                    }
                    //if(RondaActual.RondaActual == RondaActual.TotalRonda)
                    //    RondaActual.Estatus = false;

                }
                else
                {
                    this.Correcta = false;

                    if(RondaActual.Turno == 0 && RondaActual.Estatus)
                    {
                        RondaActual.Turno = 1;
                        this.Equipos[0].Turno = true;
                        this.Equipos[1].Turno = true;
                        // RondaActual.Equipo1Puntaje += this.Puntaje;
                        //this.Equipos[0].Puntaje += this.Puntaje;
                    }
                    else if(RondaActual.Turno == 1 && RondaActual.Estatus)
                    {
                        RondaActual.Turno = 0;
                        this.Equipos[0].Turno = true;
                        this.Equipos[1].Turno = false;
                        RondaActual.RondaActual += 1;
                        // RondaActual.Equipo2Puntaje += this.Puntaje;
                        //   this.Equipos[1].Puntaje += this.Puntaje;
                        //  RondaActual.RondaActual += 1;

                    }


                }

                if(RondaActual.Estatus == false)//guarda al ganador
                    if(RondaActual.Equipo1Puntaje > RondaActual.Equipo2Puntaje)
                    {
                        RondaActual.EquipoGanador = RondaActual.Equipo1;
                        RondaActual.Ganador = 0;
                    }
                    else {
                        RondaActual.EquipoGanador = RondaActual.Equipo2;
                        RondaActual.Ganador = 1;
                    }
                            


      
            ListR.Add(RondaActual);

            if(LR.GuardarTxtRonda(ListR, LR.GetRondas(this.grado), this.grado)) {
               List<Equipo> listEquipos= Lm.GetImportEequipos(this.grado);
               
                Lm.GuardarTxtEquipos(this.Equipos,listEquipos,this.grado);
            }
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }
        }
        void siguiente() {
            myTimer.Stop();
            myTimer.Dispose();
            if(this.TipoPregunta == 2)
            {
                this.sonido.Stop();
                this.sonido.Dispose();
                ModalPreguntaAbierta Modal = new ModalPreguntaAbierta(this.pregunta,this.RespuestaUser.Text);
                Modal.ShowDialog();
                if(Modal.Repuesta)
                {
                    this.respuesta = this.pregunta.Rc;
                }
                else this.respuesta = this.RespuestaUser.Text;
            }
            this.GuardarHisrotial();
            if(this.Correcta)
            {
                Resultado R = new Resultado(this.pregunta, this.respuesta, this.grado, this.Puntaje, this.Equipos);
                this.NavigationService.Navigate(R);
            }
            else {
                Incorrecto R = new Incorrecto(this.pregunta, this.respuesta, this.grado, this.Puntaje, this.Equipos);
                this.NavigationService.Navigate(R);
            }
        }
        Timer myTimer = new Timer();
        int timeLeft = 30;

        private void TiempoIniciar()
        {
            //set properties for the Timer
            myTimer.Interval = 1000;
            myTimer.Enabled = true;

            //Set the event handler for the timer, named "myTimer_Tick"
            myTimer.Tick += myTimer_Tick;

            //Start the timer as soon as the form is loaded
            myTimer.Start();

            //Show the time set in the "timeLeft" variable
           
            Tiempo.Content = timeLeft;
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //perform these actions at the interval set in the properties.
                
                timeLeft -= 1;
                Tiempo.Content = timeLeft.ToString();
                if(timeLeft == 1) {
                    this.sonido.Stop();
                    this.sonido.Dispose();
                    sonido = new SoundPlayer(@"C:\Program Files (x86)\PCLR\Instalador_Tablero_Preguntas_V_1_0\TableroConfiguracion\Sonido\RelojFin.wav");
                    sonido.Play();
                  
                }
                    if(timeLeft < 0)
                {
                   
                    myTimer.Stop();
                    if(this.TipoPregunta == 2)
                        this.respuesta = this.RespuestaUser.Text.ToUpper();
                    if(this.respuesta == "null" ||this.respuesta== "Capture la respuesta".ToUpper())
                        this.respuesta = ":Se termino el tiempo";
                    this.siguiente();
                }
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012","Tablero");
            }

        }
    }
}
