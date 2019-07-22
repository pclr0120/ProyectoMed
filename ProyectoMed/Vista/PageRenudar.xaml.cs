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
    /// Lógica de interacción para PageRenudar.xaml
    /// </summary>
    public partial class PageRenudar : Page
    {

        public bool Repuesta = false;
        public PageRenudar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Repuesta = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Repuesta = true;
          
        }
    }
}
