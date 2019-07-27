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
    /// Lógica de interacción para PageGanador.xaml
    /// </summary>
    public partial class PageGanador : Page
    {
    
        List<Equipo> Equipos;
        
        int ganador=-1;
        SoundPlayer sonido;
        public PageGanador(List<Equipo> Equipos,int ganador)
        {

         
            InitializeComponent();
            this.ganador = ganador;
            this.Equipos = Equipos;
            this.Loaded += Resultado_Loaded;
        }

        private void Resultado_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                sonido = new SoundPlayer(@"c:\winer.wav");
                sonido.Play();
                this.lblGanador.Content = this.Equipos[this.ganador].Nombre.ToUpper();
                this.lbl1.Content = this.Equipos[0].Nombre + " puntos: " + this.Equipos[0].Puntaje;
                this.lbl2.Content = this.Equipos[1].Nombre + " puntos: " + this.Equipos[1].Puntaje;
            }
            catch(Exception)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                                   "Error",
                                                                   MessageBoxButton.OK,
                                                                   MessageBoxImage.Question);
            }
          
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.sonido.Stop();
            this.sonido.Dispose();
            Grados t = new Grados();
            this.NavigationService.Navigate(t);
        }
    }
}
