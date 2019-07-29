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
    /// Lógica de interacción para Grados.xaml
    /// </summary>
    public partial class Grados : Page
    {
        public Grados()
        {
            InitializeComponent();
            this.Loaded += Grados_Loaded;
        }

        private int RondaActual = 0;
        private void Grados_Loaded(object sender, RoutedEventArgs e)
        {

            if(this.ExisteHistorial())
                if(this.ExisteRonda() )
                {

                    Confirmacion cp = new Confirmacion();
                    cp.ShowDialog();
                   
                //    MessageBoxResult result = System.Windows.MessageBox.Show("Existe un juego ya iniciado. Desea continuarlo o cargar un nuevo juego?. Si para continuar/ NO para Iniciar uno nuevo.",
                // "TABLERO: JUEGO SIN FINALIZAR",
                //MessageBoxButton.YesNo,
                // MessageBoxImage.Question);
                    if(cp.Repuesta)
                    {

                        PageTablero Tb = new PageTablero(this.RondaActual);
                        this.NavigationService.Navigate(Tb);
                    }
                    else {
                        this.CargarDatosNuevos();
                        Grados Tb = new Grados();
                        this.NavigationService.Navigate(Tb);
                    }
                }

                else
                {
                    //Grados Tb = new Grados();
                    //this.NavigationService.Navigate(Tb);
                    TotalPreguntas();
                    TotalEquipo();
                }
            else {
                if(this.CargarDatosNuevos()) {
                    TotalPreguntas();
                    TotalEquipo();
                }
               
            }
            
        }

        private void TotalPreguntas() {
            try
            {
                LogicaPreguntasHistorial Lh = new LogicaPreguntasHistorial();
                LogicaExportarData l = new LogicaExportarData();
              

                p1.Content = Lh.GetImport1(1).FindAll(i => i.Grado == 1 && i.Estatus == true).Count.ToString() +"/125";
                p2.Content = Lh.GetImport1(2).FindAll(i => i.Grado == 2 && i.Estatus == true).Count.ToString() + "/125";
                p3.Content = Lh.GetImport1(3).FindAll(i => i.Grado == 3 && i.Estatus == true).Count.ToString() + "/125";
                p4.Content = Lh.GetImport1(4).FindAll(i => i.Grado == 4 && i.Estatus==true).Count.ToString() + "/125";
                var hi = Lh.GetImport1(2).FindAll(i => i.Grado == 2 && i.Estatus == true).Count;
            }
            catch(Exception e)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);
                
            }
       

        }

        private bool CargarDatosNuevos() {
            try
            {

                if(this.BorrarDatos())
                {
                    LogicaPreguntasHistorial Lh = new LogicaPreguntasHistorial();
                    LogicaExportarData l = new LogicaExportarData();
                    Lh.GuardarTxt(l.GetImport1(1), new List<Modelo.Pregunta>(), 1);
                    Lh.GuardarTxt(l.GetImport1(2), new List<Modelo.Pregunta>(), 2);
                    Lh.GuardarTxt(l.GetImport1(3), new List<Modelo.Pregunta>(), 3);
                    Lh.GuardarTxt(l.GetImport1(4), new List<Modelo.Pregunta>(), 4);

                    LogicaExportarData lm = new LogicaExportarData();
                    LogicaHistorialEquipos LE = new LogicaHistorialEquipos();
                 
                    LE.GuardarTxtEquipos(lm.GetImportEequipos(1), new List<Equipo>(), 1);
                    LE.GuardarTxtEquipos(lm.GetImportEequipos(2), new List<Equipo>(), 2);
                    LE.GuardarTxtEquipos(lm.GetImportEequipos(3), new List<Equipo>(), 3);
                    LE.GuardarTxtEquipos(lm.GetImportEequipos(4), new List<Equipo>(), 4);

             
                    return true;
                }
                return false;
            }
            catch(Exception error)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                      "Error",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Question);
                return false;
            }
          
        }
        private bool BorrarDatos()
        {
            try
            {
                LogicaPreguntasHistorial Lh = new LogicaPreguntasHistorial();
               
                Lh.GuardarTxt(new List<Modelo.Pregunta>(), new List<Modelo.Pregunta>(), 1);
                Lh.GuardarTxt(new List<Modelo.Pregunta>(), new List<Modelo.Pregunta>(), 2);
                Lh.GuardarTxt(new List<Modelo.Pregunta>(), new List<Modelo.Pregunta>(), 3);
                Lh.GuardarTxt(new List<Modelo.Pregunta>(), new List<Modelo.Pregunta>(), 4);

               
                LogicaHistorialEquipos LE = new LogicaHistorialEquipos();
                LE.GuardarTxtEquipos(new List<Equipo>(), new List<Equipo>(), 1);
                LE.GuardarTxtEquipos(new List<Equipo>(), new List<Equipo>(), 2);
                LE.GuardarTxtEquipos(new List<Equipo>(), new List<Equipo>(), 3);
                LE.GuardarTxtEquipos(new List<Equipo>(), new List<Equipo>(), 4);
                LogicaHistorialRonda LR = new LogicaHistorialRonda();
                LR.GuardarTxtRonda(new List<Ronda>(), new List<Ronda>(), 1);
                LR.GuardarTxtRonda(new List<Ronda>(), new List<Ronda>(), 2);
                LR.GuardarTxtRonda(new List<Ronda>(), new List<Ronda>(), 3);
                LR.GuardarTxtRonda(new List<Ronda>(), new List<Ronda>(), 4);
                return true;
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

        private bool ExisteHistorial() {
            LogicaHistorialRonda Lh = new LogicaHistorialRonda();
            LogicaPreguntasHistorial lp = new LogicaPreguntasHistorial();

            try
            {
                if(Lh.GetRondas(1).FindAll(i => i.Estatus == true).Count > 0)
                {
                    this.RondaActual = 1;
                    return true;
                }
                else if(Lh.GetRondas(2).FindAll(i => i.Estatus == true).Count > 0)
                {
                    this.RondaActual = 2;
                    return true;
                }
                else if(Lh.GetRondas(3).FindAll(i => i.Estatus == true).Count > 0)
                {
                    this.RondaActual = 3;
                    return true;
                }
                else if(Lh.GetRondas(4).FindAll(i => i.Estatus == true).Count > 0)
                {
                    this.RondaActual = 4;
                    return true;
                }

                else if(lp.GetImport1(1).FindAll(i => i.Estatus == true).Count > 4)
                {
                    this.RondaActual = 0;

                    return true;
                }
                else if(lp.GetImport1(2).FindAll(i => i.Estatus == true).Count > 4)
                {
                    this.RondaActual = 0;
                    return true;
                }
                else if(lp.GetImport1(3).FindAll(i => i.Estatus == true).Count > 4)
                {
                    this.RondaActual = 0;
                    return true;
                }
                else if(lp.GetImport1(4).FindAll(i => i.Estatus == true).Count > 4)
                {
                    this.RondaActual = 0;
                    return true;
                }
                return false;


            }
            catch(Exception)
            {

                return false;
            }
        }


        private bool ExisteRonda()
        {
            LogicaHistorialRonda Lh = new LogicaHistorialRonda();
            try
            {

                if(Lh.GetRondas(1).FindAll(i=>i.Estatus==true).Count > 0)
                    return true;
                else if(Lh.GetRondas(2).FindAll(i => i.Estatus == true).Count > 0)
                    return true;
                else if(Lh.GetRondas(3).FindAll(i => i.Estatus == true).Count > 0)
                    return true;
                else if(Lh.GetRondas(4).FindAll(i => i.Estatus == true).Count > 0)
                    return true;
                return false;
            }
            catch(Exception)
            {

                return false;
            }
        }
        private void TotalEquipo()
        {
            try
            {
                LogicaExportarData l = new LogicaExportarData();
                LogicaHistorialEquipos LE = new LogicaHistorialEquipos();
        

                e1.Content = LE.GetImportEequipos(1).FindAll(i => i.Grado == 1).Count.ToString();
                e2.Content = LE.GetImportEequipos(2).FindAll(i => i.Grado == 2).Count.ToString();
                e3.Content = LE.GetImportEequipos(3).FindAll(i => i.Grado == 3).Count.ToString();
                e4.Content = LE.GetImportEequipos(4).FindAll(i => i.Grado == 4).Count.ToString();
            }
            catch(Exception e)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);

            }


        }

        void GradoSelect(int grado) {
            Validacion v = new Validacion();
            if(v.IsValido(grado))
            {
                TotalRondas Tb = new TotalRondas(grado);
                this.NavigationService.Navigate(Tb);
            }
            else {
                Notificacion c = new Notificacion("El tablero del grado "+grado.ToString()+" no tiene suficientes preguntas.","","",false);
                c.ShowDialog();
            }
            
        }

        private void ClickSelect(object sender, RoutedEventArgs e)
        {
            try
            {
                Button b = sender as Button;

                GradoSelect(int.Parse(b.CommandParameter.ToString()));
            }
            catch(Exception err)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);

            }


        }

   

        private void Btback_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
          
        }

        private void BtNetx_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Desea generar un tablero nuevo?",
                                  "TABLERO",
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                this.CargarDatosNuevos();
                Grados Tb = new Grados();
                this.NavigationService.Navigate(Tb);
            }
        }
    }
}
