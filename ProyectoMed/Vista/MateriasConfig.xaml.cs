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
    /// Lógica de interacción para MateriasConfig.xaml
    /// </summary>
    public partial class MateriasConfig : Page
    {
        

        private int grado = 0;

        private List<Materias> Lista;
        private int numeroE = 0;
        public MateriasConfig()
        {
            InitializeComponent();
            this.Loaded += MateriasConfig_Loaded;
        }

        private void MateriasConfig_Loaded(object sender, RoutedEventArgs e)
        {
            this.titulo.Content = "Materias del Grado " + this.grado;
            if(this.numeroE == 0)
            {
                LogicaMaterias l = new LogicaMaterias();
                this.Lista = new List<Materias>();
                this.Lista = l.GetImport(this.grado);
            }
            else
            {
                for(int i = 0; i < this.numeroE; i++)
                {
                    Materias eq = new Materias();
                    eq.Grado = this.grado;
                    this.Lista.Add(eq);
                }
            }


            this.Griddata.ItemsSource = this.Lista;
            this.Griddata.Items.Refresh();
        }

        public MateriasConfig(int grado, int NumeroDeEquipos) : this()
        {
            this.grado = grado;
            this.numeroE = NumeroDeEquipos;
            this.Lista = new List<Materias>();



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result0 = System.Windows.MessageBox.Show("Desea cancelar los cambios del grado" + grado.ToString() + "?",
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
                Materias eq = new Materias();
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
            Button b = sender as Button;
            this.Lista.RemoveAll(i => i.Id.Equals(b.CommandParameter.ToString()));
            this.Griddata.Items.Refresh();
            try
            {
                Materias eq = new Materias();
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
        private bool PuedeGuardar(List<Materias>lm) {

            if(lm.Count > 4)
            {
                if(CheckMateria(lm))
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
                    MessageBoxResult result = System.Windows.MessageBox.Show("Las  materias no deben de repetirse.",
                                    "Configuracion",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);
                    return false;
                }
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Para poder continuar debe de capturar las 5 materias.",
                                    "Configuracion",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);
                return false;

            }
        }

        private bool CheckMateria(List<Materias> lm)
        {

            if(lm.Count > 4)
            {

                
                foreach(var item in lm)
                {
                    if(lm.FindAll(i=>i.Nombre==item.Nombre).Count>1)
                        return false;
                }
                return true;
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Para poder continuar debe de capturar las 5 materias.",
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
                    LogicaMaterias l = new LogicaMaterias();
                    if(l.GuardarTxt(this.Lista, l.GetImport(this.grado), this.grado))
                    {
                        PageGradosConfig n = new PageGradosConfig();
                        this.NavigationService.Navigate(n);

                    }
                }
                else
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Para poder continuar debe de capturar la informacion de las 5 materias.",
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
