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
    /// Lógica de interacción para PagePregunta.xaml
    /// </summary>
    public partial class PagePregunta : Page
    {
        Pregunta Pregtunta;
        List<Pregunta> ListaPreguntas;
        int Puntaje = 0;
        string respuesta;
        string materia;
        int grado;
        int nivel;
        public PagePregunta()
        {
            InitializeComponent();


            this.Loaded += PagePregunta_Loaded;
        }
        public PagePregunta(string materia,int grado,int nivel) : this() {
            this.materia = materia;
            this.grado = grado;
            this.nivel = nivel;

        }

        private void PagePregunta_Loaded(object sender, RoutedEventArgs e)
        {

            LogicaExportarData l = new LogicaExportarData();
            LogicaMaterias lm = new LogicaMaterias();
            List<Pregunta> listaPreguntas = l.GetImport1(this.grado).FindAll(i => i.Materia == materia && i.Nivel == this.nivel);
            Random r = new Random();
            int random = r.Next(0,8);
          
           
            Modelo.Pregunta Pr= listaPreguntas[random];
            this.Pregtunta = Pr;
            
            string puntaje = "";
            if (nivel == 1)
                puntaje = "200";
                if (nivel == 2)
                puntaje = "400";
            if (nivel == 3)
                puntaje = "600";
            if (nivel == 4)
                puntaje = "800";
            if (nivel == 5)
                puntaje = "1000";
            this.Puntaje = int.Parse(puntaje);
            this.Titulo.Content = this.materia +" Por "+puntaje;

            this.Pregunta.Text = Pr.Descripcion;
            Random r2= new Random();
            int rm2 = r2.Next(0, 4);
            if(rm2==0) {
                this.R1.Content ="A:"+ Pr.R1;
                this.R2.Content = "B:" + Pr.R2;
                this.R3.Content = "C:"+Pr.R3;
                this.R4.Content ="D:"+Pr.Rc;
            }
            if(rm2 == 1)
            {
                this.R1.Content = "A:" + Pr.Rc;
                this.R2.Content = "B:"+Pr.R2;
                this.R3.Content = "C:" + Pr.R3;
                this.R4.Content = "D:"+Pr.R1;
            }
            if(rm2 == 2)
            {
                this.R1.Content ="A:"+ Pr.R2;
                this.R2.Content = "B:"+Pr.Rc;
                this.R3.Content = "C:"+Pr.R3;
                this.R4.Content = "D:"+Pr.R1;
            }
            if(rm2 == 3)
            {
                this.R1.Content ="A:"+ Pr.R2;
                this.R2.Content = "B:"+Pr.R3;
                this.R3.Content = "C:"+Pr.Rc;
                this.R4.Content = "D:"+Pr.R1;
            }
            if(rm2 == 4)
            {
                this.R1.Content = "A:"+Pr.R2;
                this.R2.Content = "B:"+Pr.R1;
                this.R3.Content ="C:"+ Pr.Rc;
                this.R4.Content = "D:"+Pr.R3;
            }

        }

        //public PagePregunta(PageTablero p) : this()

        //{
        //    var a = p;
        //    var c = p.Hola;


        //}



        private void R1_Checked(object sender, RoutedEventArgs e)
        {


            RadioButton r = sender as RadioButton;
            if (r.Name.Equals("R1"))
            {
                r.Foreground = Brushes.Yellow;
                this.R2.Foreground = Brushes.White;
                this.R3.Foreground = Brushes.White;
                this.R4.Foreground = Brushes.White;
                this.respuesta = r.Content.ToString();
            }
          

            if (r.Name.Equals("R2"))
            {
                r.Foreground = Brushes.Yellow;
                this.R1.Foreground = Brushes.White;
                this.R3.Foreground = Brushes.White;
                this.R4.Foreground = Brushes.White;
                this.respuesta = r.Content.ToString();
            }
           
            if (r.Name.Equals("R3"))
            {
                r.Foreground = Brushes.Yellow;
                this.R1.Foreground = Brushes.White;
                this.R2.Foreground = Brushes.White;
                this.R4.Foreground = Brushes.White;
                this.respuesta = r.Content.ToString();
            }
          
            if (r.Name.Equals("R4"))
            {
                r.Foreground = Brushes.Yellow;
                this.R1.Foreground = Brushes.White;
                this.R2.Foreground = Brushes.White;
                this.R3.Foreground = Brushes.White;
                this.respuesta = r.Content.ToString();
            }
          



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Resultado R = new Resultado(this.Pregtunta,this.respuesta,this.grado,this.Puntaje);
            this.NavigationService.Navigate(R);
        }
    }
}
