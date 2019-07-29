using ProyectoMed.Logica;
using ProyectoMed.Modal;
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

            this.Navigated += MainWindow_Navigated;
        }

        private void MainWindow_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }

        private void NavigationWindow_KeyUp(object sender, KeyEventArgs e)
        {

            //Grados g = new Grados();
         
            HistorialResultados h = new HistorialResultados();
            if(e.Key.Equals(Key.Escape))
                h.ShowDialog();

            if(h.cerrar)
            {
                Environment.Exit(0);
              
            }
            //    NavigationService.GoBack();
            // NavigationService.Navigate(g);
        }
    }
}
