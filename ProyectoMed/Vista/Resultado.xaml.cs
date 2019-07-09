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
    /// Lógica de interacción para Resultado.xaml
    /// </summary>
    public partial class Resultado : Page
    {
        List<Pregunta> ListaPreguntas;
        Pregunta Pregunta;
        List<Equipo> Equipos;
        string Res;
        int puntaje;
        int grado;
        public Resultado(Pregunta Pregunta,string Respuesta,int grado,int puntaje,List<Equipo> Equipos)
        {
            InitializeComponent();
            this.grado = grado;
            this.puntaje = puntaje;
            this.Pregunta = Pregunta;
            this.Res = Respuesta;
            this.Equipos = Equipos;
            this.Loaded += Resultado_Loaded;
        }

        private void Resultado_Loaded(object sender, RoutedEventArgs e)
        {

            Pregunta p=this.ListaPreguntas.Find(item => item.Id == Pregunta.Id);
            if (p.Nivel == 1)
                this.puntaje = 200;
            if (p.Nivel == 2)
                this.puntaje = 400;
            if (p.Nivel == 3)
                this.puntaje = 600;
            if (p.Nivel == 4)
                this.puntaje = 800;
            if (p.Nivel == 5)
                this.puntaje = 1000;

            if (p.Rc.Equals(this.Res))
            {
                this.Respusta.Content = "Tu repuesta fue correcta!";
                this.Puntos.Content = " Puntos obtenidos: +" + this.puntaje;
            }
            else {
                this.Respusta.Content = "Tu repuesta fue Incorrecta.";
                this.Puntos.Content = "Puntos obtenidos: 0"  ;
            }

          var a=  this.ListaPreguntas.Find(item => item.Id == Pregunta.Id).Estatus = false;
            var bb = this.ListaPreguntas.FindAll(i=>i.Estatus==false);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  Grados G = new Grados();
            PageTablero t =  new PageTablero(this.grado,this.Equipos);
            this.NavigationService.Navigate(t);
        }
    }
}
