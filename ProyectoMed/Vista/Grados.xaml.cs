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

        private void Grados_Loaded(object sender, RoutedEventArgs e)
        {
            TotalPreguntas();
            TotalEquipo();
        }

        private void TotalPreguntas() {
            try
            {
                LogicaPreguntasHistorial Lh = new LogicaPreguntasHistorial();
                LogicaExportarData l = new LogicaExportarData();
                Lh.GuardarTxt(l.GetImport1(1),new List<Modelo.Pregunta>(),1);
                Lh.GuardarTxt(l.GetImport1(2), new List<Modelo.Pregunta>(), 2);
                Lh.GuardarTxt(l.GetImport1(3), new List<Modelo.Pregunta>(), 3);
                Lh.GuardarTxt(l.GetImport1(4), new List<Modelo.Pregunta>(), 4);

                p1.Content = Lh.GetImport1(1).FindAll(i => i.Grado == 1 && i.Estatus == true).Count.ToString() +"/125";
                p2.Content = Lh.GetImport1(2).FindAll(i => i.Grado == 2 && i.Estatus == true).Count.ToString() + "/125";
                p3.Content = Lh.GetImport1(3).FindAll(i => i.Grado == 3 && i.Estatus == true).Count.ToString() + "/125";
                p4.Content = Lh.GetImport1(4).FindAll(i => i.Grado == 4 && i.Estatus==true).Count.ToString() + "/125";
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
                LogicaHistorialEquipos LE = new LogicaHistorialEquipos();
                LE.GuardarTxtEquipos(l.GetImportEequipos(1),new List<Equipo>(),1);
                LE.GuardarTxtEquipos(l.GetImportEequipos(2), new List<Equipo>(), 2);
                LE.GuardarTxtEquipos(l.GetImportEequipos(3), new List<Equipo>(), 3);
                LE.GuardarTxtEquipos(l.GetImportEequipos(4), new List<Equipo>(), 4);

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

            TotalRondas Tb = new TotalRondas(grado);
            this.NavigationService.Navigate(Tb);
        
            
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
    }
}
