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
    /// Lógica de interacción para PageGradosConfig.xaml
    /// </summary>
    public partial class PageGradosConfig : Page
    {
        public PageGradosConfig()
        {
            InitializeComponent();
            this.Loaded += Grados_Loaded;
        }

        private void Grados_Loaded(object sender, RoutedEventArgs e)
        {
            TotalPreguntas();
            TotalEquipo();
        }

        private int TotalPreguntas(List<Pregunta> lm)
        {
            try
            {
                int count = 0;
                if(lm.Count > 0)
                {
                    foreach(var item in lm)
                    {
                        if(item.Materia != "")
                        {
                            count++;
                        }

                    }
                    return count;

                }
                return 0;
            }
            catch(Exception)
            {
                
                MessageBox.Show("Ups!,Ocurrio un error reportelo al 6681010012","Error");
                return 0;
            }


        }
        private void TotalPreguntas()
        {
            try
            {
                LogicaExportarData l = new LogicaExportarData();
                p1.Content = TotalPreguntas(l.GetImport1(1).FindAll(i => i.Grado == 1)).ToString() + "/125";
                p2.Content = TotalPreguntas(l.GetImport1(2).FindAll(i => i.Grado == 2)).ToString() + "/125";
                p3.Content = TotalPreguntas(l.GetImport1(3).FindAll(i => i.Grado == 3)).ToString() + "/125";
                p4.Content = TotalPreguntas(l.GetImport1(4).FindAll(i => i.Grado == 4)).ToString() + "/125";
                TotalEquipo();
            }
            catch(Exception e)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);

            }


        }

        private void Configuracion(int grado) {
            LogicaMaterias l = new LogicaMaterias();
            LogicaExportarData lm = new LogicaExportarData();
            if(l.GetImport(grado).Count == 5 && lm.GetImportEequipos(grado).Count >1)
            {

                PageTableroCofig Tb = new PageTableroCofig(grado);
                this.NavigationService.Navigate(Tb);
            }
            else if(l.GetImport(grado).Count < 5)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Primero comenzaremos a configurar 5 materias del tablero.",
                "TABLERO",
                MessageBoxButton.OK,
                MessageBoxImage.Question);
                MateriasConfig Tb = new MateriasConfig(grado, 5);
                this.NavigationService.Navigate(Tb);
            }
            else
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Ahora comenzaremos a configurar los equipos de este grado.",
                 "TABLERO",
                 MessageBoxButton.OK,
                 MessageBoxImage.Question);
                ConfiguracionEquiposAdd Tb = new ConfiguracionEquiposAdd(grado);
                this.NavigationService.Navigate(Tb);
            }
        }
        private void G1_Click(object sender, RoutedEventArgs e)
        {

            this.Configuracion(1);
        }

        private void G2_Click(object sender, RoutedEventArgs e)
        {

            this.Configuracion(2);

        }

        private void G3_Click(object sender, RoutedEventArgs e)
        {
            this.Configuracion(3);
        }

        private void G4_Click(object sender, RoutedEventArgs e)
        {
            this.Configuracion(4);
        }
        private bool  EliminarTablero(int grado) {
            LogicaExportarData L = new LogicaExportarData();
         
            
           return L.EliminarPreguntasGrado(grado);
        }
        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if(EliminarTablero(1))
                TotalPreguntas();

        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {
            if(EliminarTablero(2))
                TotalPreguntas();
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            if(EliminarTablero(3))
                TotalPreguntas();
        }

        private void B4_Click(object sender, RoutedEventArgs e)
        {
            if(EliminarTablero(4))
                TotalPreguntas();
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
        private void E1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button b = sender as Button;
                //if(int.Parse(this.e1.Content.ToString()) < 2)
                //{

                //}
                LogicaExportarData l = new LogicaExportarData();
                if(l.GetImportEequipos(int.Parse(b.CommandParameter.ToString())).Count > 1)
                {
                    EquipoConfiguracion Tb = new EquipoConfiguracion(int.Parse(b.CommandParameter.ToString()), 0);
                    this.NavigationService.Navigate(Tb);
                }
                else {
                    ConfiguracionEquiposAdd p = new ConfiguracionEquiposAdd(int.Parse(b.CommandParameter.ToString()));
                    this.NavigationService.Navigate(p);
                }

            }
            catch(Exception err)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Question);
            }

    
        }

        private void E2_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home p = new Home();
            this.NavigationService.Navigate(p);
        }
    }
}
