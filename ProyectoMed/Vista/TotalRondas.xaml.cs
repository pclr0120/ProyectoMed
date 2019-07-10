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
    /// Lógica de interacción para TotalRondas.xaml
    /// </summary>
    public partial class TotalRondas : Page
    {
        public TotalRondas()
        {
            InitializeComponent();
            this.Loaded += TotalRondas_Loaded;
        }
        private int Grado = 0;
        public TotalRondas(int grado) : this()
        {
            this.Grado = grado;
        }
        private string SeleccionE1 = "";
        private string SeleccionE2 = "";

        private void TotalRondas_Loaded(object sender, RoutedEventArgs e)
        {
            EquipoSeleccionar();
        }
        List<Equipo> eq;
        List<Equipo> eq2;
        private void EquipoSeleccionar()
        {
            try
            {
                LogicaExportarData l = new LogicaExportarData();
                eq = l.GetImportEequipos(this.Grado);

                this.cb2.IsEnabled = false;
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }




        }
        private void Cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }


        private  bool Continuar() {
            try
            {
                if(int.Parse(TxtNumero.Text.ToString()) > 2)
                {
                    if((cb1.SelectedValue.ToString() != "" && cb1.SelectedValue != null))
                        return true;
                    else
                        MessageBox.Show("Se deben de seleccionar los dos equipos para porder continuar..", "Rondas");
                }
                else
                {
                    MessageBox.Show("El numero de rondas minimo son 3", "Rondas");
                    return false;
                }

            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }
            return false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                List<Equipo> Equipos = new List<Equipo>();
                Equipo e1 = this.eq.Find(i => i.Nombre == cb1.Text);
                Equipos.Add(e1);
                Equipo e2 = this.eq.Find(i => i.Nombre == cb2.Text);
                Equipos.Add(e2);
                if(Continuar())
                {
                    LogicaHistorialRonda Lh = new LogicaHistorialRonda();

                    Ronda ronda = new Ronda();
                    ronda.Grado = this.Grado;
                    ronda.TotalRonda = int.Parse(this.TxtNumero.Text.ToString());
                    ronda.Equipo1 = this.cb1.Text;
                    ronda.Equipo2 = this.cb2.Text;
                    List<Ronda> lr = new List<Ronda>();
                    lr.Add(ronda);
                    if(Lh.GuardarTxtRonda(lr, Lh.GetRondas(this.Grado), this.Grado))
                    {

                        PageTablero Tb = new PageTablero(this.Grado);
                        this.NavigationService.Navigate(Tb);
                    }
                    else MessageBox.Show("Ups!. Ocurrio un error si persiste comuniquese con el desarrollador  al siguiente telefono 6681010012", "Tablero");
                }
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }

        }

        private void Cb1_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                EquipoSeleccionar();
                cb1.ItemsSource = this.eq;
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }


        }

        private void Cb1_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                this.cb2.IsEnabled = true;
                this.eq2 = this.eq;
                this.eq2.RemoveAll(i => i.Nombre == cb1.Text);
                cb2.ItemsSource = this.eq2;
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }


        }

        private void TxtNumero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Grados p = new Grados();
                this.NavigationService.Navigate(p);
            }
            catch(Exception err)
            {

                System.Windows.MessageBox.Show("Up!. Ocurrio un error si esto persiste reportelo al 6681010012", "Tablero");
            }

        }
    }
}
