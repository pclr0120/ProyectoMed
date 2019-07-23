using ProyectoMed.Logica;
using ProyectoMed.Vista;
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

namespace ProyectoMed
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
              
         
        }

        private void NavigationWindow_KeyUp(object sender, KeyEventArgs e)
        {

            //Grados g = new Grados();
            NavigationService.RemoveBackEntry();
           
            //if (e.Key.Equals(Key.Escape) && NavigationService.CanGoBack)
            //    NavigationService.GoBack();
            // NavigationService.Navigate(g);
        }
    }
}
