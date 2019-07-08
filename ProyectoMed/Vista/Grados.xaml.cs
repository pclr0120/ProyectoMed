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
    /// Lógica de interacción para Grados.xaml
    /// </summary>
    public partial class Grados : Page
    {
        public Grados()
        {
            InitializeComponent();
            this.Loaded += Grados_Loaded;
        }

        private void Grados_Loaded(object sender, RoutedEventArgs e)
        {
            TotalPreguntas();
            TotalEquipo();
        }

        private void TotalPreguntas() {
            try
            {
                LogicaExportarData l = new LogicaExportarData();
                p1.Content = l.GetImport1(1).FindAll(i => i.Grado == 1).Count.ToString() +"/125";
                p2.Content = l.GetImport1(2).FindAll(i => i.Grado == 2).Count.ToString() + "/125";
                p3.Content = l.GetImport1(3).FindAll(i => i.Grado == 3).Count.ToString() + "/125";
                p4.Content = l.GetImport1(4).FindAll(i => i.Grado == 4).Count.ToString() + "/125";
            }
            catch(Exception e)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);

            }
       

        }
        private void TotalEquipo()
        {
            try
            {
                LogicaExportarData l = new LogicaExportarData();
                e1.Content = l.GetImportEequipos(1).FindAll(i => i.Grado == 1).Count.ToString();
                e2.Content = l.GetImportEequipos(2).FindAll(i => i.Grado == 2).Count.ToString();
                e3.Content = l.GetImportEequipos(3).FindAll(i => i.Grado == 3).Count.ToString();
                e4.Content = l.GetImportEequipos(4).FindAll(i => i.Grado == 4).Count.ToString();
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

            TotalRondas Tb = new TotalRondas(grado);
            this.NavigationService.Navigate(Tb);
        
            
        }

        private void ClickSelect(object sender, RoutedEventArgs e)
        {
              Button b=sender as Button;

            GradoSelect(int.Parse(b.CommandParameter.ToString()));
        
        }

   

        private void Btback_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
          
        }
    }
}
