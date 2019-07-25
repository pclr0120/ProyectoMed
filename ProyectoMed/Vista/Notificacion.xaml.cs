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
using System.Windows.Shapes;

namespace ProyectoMed.Vista
{
    /// <summary>
    /// Lógica de interacción para Notificacion.xaml
    /// </summary>
    public partial class Notificacion : Window
    {

        private string Mensaje="";
        public bool Respuesta = false;
        private string btn1="";
        private string btn2="";
        private bool allBtn = false;

        public Notificacion(string mensaje, string btnTrue, string btnFalse,bool allBtn)
        {
            InitializeComponent();
            this.allBtn = allBtn;
            this.Mensaje = mensaje;
            this.btn1 = btnTrue;
            this.btn2 = btnFalse;
            this.Loaded += Notificacion_Loaded;

            
        }

        private void Notificacion_Loaded(object sender, RoutedEventArgs e)
        {


            if(this.allBtn)
            {
                this.cmd0.Visibility = Visibility.Hidden;

            }
            else {
                this.cmd1.Visibility = Visibility.Hidden;
                this.cmd2.Visibility = Visibility.Hidden;
                this.cmd0.Visibility = Visibility.Visible;
            }
            this.lblMensaje.Content = this.Mensaje;
            this.cmd1.Content = this.btn1;
            this.cmd2.Content = this.btn2;
        }

        private void btnContinuar(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if(bt.CommandParameter.Equals("1"))
                this.Respuesta = true;
            else
                this.Respuesta = false;

            this.Close();

        }
    }
}
