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
        private void EquipoSeleccionar() {
            LogicaExportarData l = new LogicaExportarData();
            eq = l.GetImportEequipos(this.Grado);
            this.cb2.IsEnabled = false;


        }
        private void Cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }


        private  bool Continuar() {
            if(int.Parse(TxtNumero.Text.ToString()) > 2)
            {
                if((cb1.SelectedValue.ToString()!="" && cb1.SelectedValue!=null))
                 return true;
                else
                    MessageBox.Show("Se deben de seleccionar los dos equipos para porder continuar..", "Rondas");
            }
            else
            {
                MessageBox.Show("El numero de rondas minimo son 3", "Rondas");
                return false;
            }
            return false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Equipo> Equipos = new List<Equipo>();
            Equipo e1 = this.eq.Find(i => i.Nombre == cb1.Text);
            Equipos.Add(e1);
            Equipo e2 = this.eq.Find(i => i.Nombre == cb2.Text);
            Equipos.Add(e2);
            if(Continuar())
            {
                PageTablero Tb = new PageTablero(this.Grado,Equipos);
                this.NavigationService.Navigate(Tb);
            }
            
        }

        private void Cb1_DropDownOpened(object sender, EventArgs e)
        {       
            cb1.ItemsSource = this.eq;
        }

        private void Cb1_DropDownClosed(object sender, EventArgs e)
        {
            this.cb2.IsEnabled = true;
            List<Equipo> letemp = this.eq;
            letemp.RemoveAll(i=>i.Nombre==cb1.Text);
            cb2.ItemsSource = letemp;
           
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
    }
}
