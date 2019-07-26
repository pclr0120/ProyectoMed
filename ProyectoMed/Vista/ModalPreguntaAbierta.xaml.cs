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
using System.Windows.Shapes;

namespace ProyectoMed.Vista
{
    /// <summary>
    /// Lógica de interacción para ModalPreguntaAbierta.xaml
    /// </summary>
    public partial class ModalPreguntaAbierta : Window
    {

        public bool Repuesta = false;

        public ModalPreguntaAbierta(Pregunta pregunta, string respuesta)
        {
            InitializeComponent();
            this.lblRepuestaUser.Content = "Tu respusta fue: " + respuesta.ToUpper();
            this.lblRespustaCorrecta.Content = "Repuesta correcta es: " + pregunta.Rc.ToUpper() ;

            Validacion v = new Validacion();
            this.lblRepuestaUser.FontSize = v.sizeLetraRespuestas(respuesta);
            this.lblRespustaCorrecta.FontSize = v.sizeLetraRespuestas(pregunta.Rc);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(this.RbNo.IsChecked == true || this.RbSi.IsChecked == true)
                {
                    if(this.RbSi.IsChecked == true)
                        this.Repuesta = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Elija una opcion para continuar...");
                }

               

            }
            catch(Exception w)
            {

                MessageBoxResult result = System.Windows.MessageBox.Show("Ups!, Ocurrio un problema:Si el error persiste  reportelo al 6681010012.",
                                                                   "Error",
                                                                   MessageBoxButton.OK,
                                                                   MessageBoxImage.Question);
            }
        }

        
    }
}
