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
    /// Lógica de interacción para EquipoConfiguracion.xaml
    /// </summary>
    public partial class EquipoConfiguracion : Page
    {

        private int grado = 0;
       
        private List<Equipo> Lista;
        private int numeroE = 0;
        public EquipoConfiguracion()
        {
            InitializeComponent();
            this.Loaded += EquipoConfiguracion_Loaded;
        }

        private void EquipoConfiguracion_Loaded(object sender, RoutedEventArgs e)
        {
            this.titulo.Content = "Equipos del Grado " + this.grado;
            if(this.numeroE == 0)
            {
                LogicaExportarData l = new LogicaExportarData();
                this.Lista = new List<Equipo>();
                this.Lista = l.GetImportEequipos(this.grado);
            }
            else
            {
                for(int i = 0; i < this.numeroE; i++)
                {
                    Equipo eq = new Equipo();
                    eq.Grado = this.grado;
                    this.Lista.Add(eq);
                }
            }


            this.Griddata.ItemsSource = this.Lista;
            this.Griddata.Items.Refresh();
        }

        public EquipoConfiguracion( int grado,int NumeroDeEquipos) : this()
        {
            this.grado = grado;
            this.numeroE =NumeroDeEquipos;
            this.Lista=new List<Equipo>();

        

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result0 = System.Windows.MessageBox.Show("Desea cancelar los cambios" + grado.ToString() + "?",
                                             "Notificación",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);
            if(result0 == MessageBoxResult.Yes)
            {
                PageGradosConfig p = new PageGradosConfig();
                NavigationService.Navigate(p);
            }
        }

        private void Button_Click_nuevo(object sender, RoutedEventArgs e)
        {
           
            try
            {
                Equipo eq = new Equipo();
                eq.Grado = this.grado;
                this.Lista.Add(eq);
                this.Griddata.ItemsSource = this.Lista;
                this.Griddata.Items.Refresh();
            }
            catch(Exception)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                        "Error",
                                                        MessageBoxButton.OK,
                                                        MessageBoxImage.Question);

            }


        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            
        }

        private void Button_Click_Eliminar(object sender, RoutedEventArgs e)
        {
          
            try
            {
                Button b = sender as Button;
                this.Lista.RemoveAll(i => i.Id.Equals(b.CommandParameter.ToString()));
                this.Griddata.Items.Refresh();
                //Equipo eq = new Equipo();
                //eq.Grado = this.grado;
                //this.Lista.Add(eq);
                //this.Griddata.ItemsSource = this.Lista;
                //this.Griddata.Items.Refresh();
            }
            catch(Exception)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                        "Error",
                                                        MessageBoxButton.OK,
                                                        MessageBoxImage.Question);

            }

        }
        private bool Checkequipo(List<Equipo> lm)
        {



                foreach(var item in lm)
                {
                    if(lm.FindAll(i => i.Nombre == item.Nombre).Count > 1)
                        return false;
                }
                return true;
            
           
        }
        private bool PuedeGuardar(List<Equipo> lm)
        {

            if(lm.Count > 1)
            {
                if(Checkequipo(lm))
                {
                    foreach(var item in lm)
                    {
                        if(item.Nombre == "")
                            return false;
                    }
                    return true;
                }
                else
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Los equipos  no deben de repetirse.",
                                    "Configuracion",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);
                    return false;
                }
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Para poder continuar debe de capturar por lo menos 2 equipos.",
                                    "Configuracion",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);
                return false;

            }
        }
        private void Button_Click_GuardarCambios(object sender, RoutedEventArgs e)
        {
            try
            {
                if(PuedeGuardar(this.Lista))
                {
                    LogicaExportarData l = new LogicaExportarData();
                    if(l.GuardarTxtEquipos(this.Lista, l.GetImportEequipos(this.grado), this.grado)) {
                        PageGradosConfig p = new PageGradosConfig();
                        this.NavigationService.Navigate(p);
                    }

                }
                else {

                    MessageBoxResult result = System.Windows.MessageBox.Show("Para poder continuar debe de capturar la informacion de todos los equipos en la lista.",
                                      "Configuracion",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Question);
                }
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
