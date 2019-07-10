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
    /// Lógica de interacción para ConfiguracionEquiposAdd.xaml
    /// </summary>
    public partial class ConfiguracionEquiposAdd : Page
    {
        public ConfiguracionEquiposAdd()
        {
            InitializeComponent();
        }

        private int _grado = 0;
        private int numEquipo = 0;
        public ConfiguracionEquiposAdd(int grado):this(){
            this._grado = grado;
            //this.numEquipo = numEquipo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(int.Parse(TxtNumero.Text) > 1)
                {
                    EquipoConfiguracion eq = new EquipoConfiguracion(this._grado, int.Parse(TxtNumero.Text));
                    this.NavigationService.Navigate(eq);
                }
                else
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("El numero debe ser mayor a 1 para poder continuar.",
                                                          "Configuracion",
                                                          MessageBoxButton.OK,
                                                          MessageBoxImage.Question);

                }
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }



        }
        public void SoloNumeros(TextCompositionEventArgs e)
        {
            //se convierte a Ascci del la tecla presionada 
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            //verificamos que se encuentre en ese rango que son entre el 0 y el 9 
            if(ascci >= 48 && ascci <= 57)
                e.Handled = false;
            else e.Handled = true;
        }
        private void TxtNumero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                PageGradosConfig p = new PageGradosConfig();
                this.NavigationService.Navigate(p);
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }

        }
    }
}
