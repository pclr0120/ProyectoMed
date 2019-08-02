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
using System.Windows.Shapes;

namespace ProyectoMed.Modal
{
    /// <summary>
    /// Lógica de interacción para HistorialResultados.xaml
    /// </summary>
    public partial class HistorialResultados : Window
    {

        private List<Ronda> ListaRondas;
        public HistorialResultados()
        {
            InitializeComponent();
            this.Loaded += HistorialResultados_Loaded;
        }

        private void HistorialResultados_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetGanadores(1);
            this.G1.Foreground = Brushes.Blue;

        }

        private void ClickSelect(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            this.GradoSeleccionado(b);
            this.GetGanadores(int.Parse(b.CommandParameter.ToString()));

        }



        private void GetGanadores(int Grado) {
            try
            {
                LogicaHistorialRonda Lh = new LogicaHistorialRonda();

                this.ListaRondas = new List<Ronda>();

                this.ListaRondas = Lh.GetRondas(Grado).FindAll(item => item.Estatus == false);

                this.Griddata.ItemsSource = this.ListaRondas;
                this.Griddata.Items.Refresh();
            }
            catch(Exception err)
            {

                MessageBox.Show("Error. Reportelo al 6681010012","TABLERO");
            }
          

        }

        void GradoSeleccionado(Button b) {

            this.G1.Foreground = Brushes.Yellow;
            this.G2.Foreground = Brushes.Yellow;
            this.G3.Foreground = Brushes.Yellow;
            this.G4.Foreground = Brushes.Yellow;

            b.Foreground = Brushes.Blue;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public bool cerrar = false;
        private void Button_ClickSalir(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Desea cerrar el juego?",
                                               "Notificación",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes) {
                this.cerrar = true;
            }
                this.Close();
        }
    }
}
