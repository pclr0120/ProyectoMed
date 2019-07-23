using ProyectoMed.Logica;
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
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

          
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Notificacion n = new Notificacion("No se puede iniciar el juego por que faltan informacion en configuracion.", "Ir configuracion", "OK", true);
            Validacion v = new Validacion();
            if(v.ContinuarAGrado())
            {
                Grados G = new Grados();
                this.NavigationService.Navigate(G);
            }
            else {
                n.ShowDialog();
                if(n.Respuesta)
                {
                    PageGradosConfig G = new PageGradosConfig();
                    this.NavigationService.Navigate(G);
                }
                else {
                    Home G = new Home();
                    this.NavigationService.Navigate(G);
                }
            }
        
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageGradosConfig G = new PageGradosConfig();
            this.NavigationService.Navigate(G);
            //AdministrarPreguntas administrarPreguntas = new AdministrarPreguntas();
            //this.NavigationService.Navigate(administrarPreguntas);
        }
    }
}
