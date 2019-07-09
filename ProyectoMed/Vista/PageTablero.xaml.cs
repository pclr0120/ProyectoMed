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
    /// Lógica de interacción para PageTablero.xaml
    /// </summary>
    public partial class PageTablero : Page
    {
        private int _Grado = 0;

        private List<Pregunta> ListaPreguntas;
        private List<Materias> ListaMaterias;
        private List<Equipo> Equipos;
        private int grado=0;
        private int ronda = 0;
        private int turno = 0;
        private int Totalronda = 0;


        public PageTablero()
        {
            InitializeComponent();

        }
        public PageTablero(int grado,List<Equipo> Equipos) : this()
        {
            this._Grado = grado;
            this.Equipos = Equipos;

            LogicaExportarData l = new LogicaExportarData();
            LogicaMaterias m = new LogicaMaterias();
            LogicaHistorialRonda lh = new LogicaHistorialRonda();
            List<Ronda> lronda = lh.GetRondas(this._Grado);

            //simpre habra uno por que se tiene que registrar la rondas antes de empezar
            if(lronda.Count > 1 && lronda[lronda.Count - 1].Estatus==true)
            {
             
               
                this.ronda = lronda[lronda.Count - 1].RondaActual;
                this.turno = lronda[lronda.Count - 1].Turno;
                this.Totalronda = lronda[lronda.Count - 1].TotalRonda;
                this.InputTeam1.Content= lronda[lronda.Count - 1].Equipo1;
                this.InputTeam2.Content = lronda[lronda.Count - 1].Equipo2;
                this.InputTeam1Puntaje.Content = lronda[lronda.Count - 1].Equipo1Puntaje;
                this.InputTeam2Puntaje.Content = lronda[lronda.Count - 1].Equipo2Puntaje;
                this.lblRondas.Content = lronda[lronda.Count - 1].TotalRonda.ToString();
            }
           
            this.grado = grado;

            try
            {

                this.ListaPreguntas = l.GetImport1(this.grado);
                this.ListaMaterias = m.GetImport(this._Grado);
                // if() {
                this.M1.Content = this.ListaMaterias[0].Nombre.ToString();
                this.M2.Content = this.ListaMaterias[1].Nombre.ToString();
                this.M3.Content = this.ListaMaterias[2].Nombre.ToString();
                this.M4.Content = this.ListaMaterias[3].Nombre.ToString();
                this.M5.Content = this.ListaMaterias[4].Nombre.ToString();
                CargarTablero();

            }
            catch(Exception)
            {

                MessageBox.Show("Error. Reportelo al 6681010012","Error");
            }
        }


        void CargarTablero()
        {

            this.P11.Content = "200 " + this.ListaPreguntas.FindAll(item => item.Nivel == 1 && item.Estatus == true && item.Materia == this.ListaMaterias[0].Nombre).Count + "/5"; ;
            this.P12.Content = "400 " + this.ListaPreguntas.FindAll(item => item.Nivel == 2 && item.Estatus == true && item.Materia == this.ListaMaterias[0].Nombre).Count + "/5";
            this.P13.Content = "600 " + this.ListaPreguntas.FindAll(item => item.Nivel == 3 && item.Estatus == true && item.Materia == this.ListaMaterias[0].Nombre).Count + "/5";
            this.P14.Content = "800 " + this.ListaPreguntas.FindAll(item => item.Nivel == 4 && item.Estatus == true && item.Materia == this.ListaMaterias[0].Nombre).Count + "/5";
            this.P15.Content = "1000 " + this.ListaPreguntas.FindAll(item => item.Nivel == 5 && item.Estatus == true && item.Materia == this.ListaMaterias[0].Nombre).Count + "/5";

            this.P21.Content = "200 " + this.ListaPreguntas.FindAll(item => item.Nivel == 1 && item.Estatus == true && item.Materia == this.ListaMaterias[1].Nombre).Count + "/5";
            this.P22.Content = "400 " + this.ListaPreguntas.FindAll(item => item.Nivel == 2 && item.Estatus == true && item.Materia == this.ListaMaterias[1].Nombre).Count + "/5";
            this.P23.Content = "600 " + this.ListaPreguntas.FindAll(item => item.Nivel == 3 && item.Estatus == true && item.Materia == this.ListaMaterias[1].Nombre).Count + "/5";
            this.P24.Content = "800 " + this.ListaPreguntas.FindAll(item => item.Nivel == 4 && item.Estatus == true && item.Materia == this.ListaMaterias[1].Nombre).Count + "/5";
            this.P25.Content = "1000 " + this.ListaPreguntas.FindAll(item => item.Nivel == 5 && item.Estatus == true && item.Materia == this.ListaMaterias[1].Nombre).Count + "/5";

            this.P31.Content = "200 " + this.ListaPreguntas.FindAll(item => item.Nivel == 1 && item.Estatus == true && item.Materia == this.ListaMaterias[2].Nombre).Count + "/5";
            this.P32.Content = "400 " + this.ListaPreguntas.FindAll(item => item.Nivel == 2 && item.Estatus == true && item.Materia == this.ListaMaterias[2].Nombre).Count + "/5";
            this.P33.Content = "600 " + this.ListaPreguntas.FindAll(item => item.Nivel == 3 && item.Estatus == true && item.Materia == this.ListaMaterias[2].Nombre).Count + "/5";
            this.P34.Content = "800 " + this.ListaPreguntas.FindAll(item => item.Nivel == 4 && item.Estatus == true && item.Materia == this.ListaMaterias[2].Nombre).Count + "/5";
            this.P35.Content = "1000 " + this.ListaPreguntas.FindAll(item => item.Nivel == 5 && item.Estatus == true && item.Materia == this.ListaMaterias[2].Nombre).Count + "/5";

            this.P41.Content = "200 " + this.ListaPreguntas.FindAll(item => item.Nivel == 1 && item.Estatus == true && item.Materia == this.ListaMaterias[3].Nombre).Count + "/5";
            this.P42.Content = "400 " + this.ListaPreguntas.FindAll(item => item.Nivel == 2 && item.Estatus == true && item.Materia == this.ListaMaterias[3].Nombre).Count + "/5";
            this.P43.Content = "600 " + this.ListaPreguntas.FindAll(item => item.Nivel == 3 && item.Estatus == true && item.Materia == this.ListaMaterias[3].Nombre).Count + "/5";
            this.P44.Content = "800 " + this.ListaPreguntas.FindAll(item => item.Nivel == 4 && item.Estatus == true && item.Materia == this.ListaMaterias[3].Nombre).Count + "/5";
            this.P45.Content = "1000 " + this.ListaPreguntas.FindAll(item => item.Nivel == 5 && item.Estatus == true && item.Materia == this.ListaMaterias[3].Nombre).Count + "/5";

            this.P51.Content = "200 " + this.ListaPreguntas.FindAll(item => item.Nivel == 1 && item.Estatus == true && item.Materia == this.ListaMaterias[4].Nombre).Count + "/5";
            this.P52.Content = "400 " + this.ListaPreguntas.FindAll(item => item.Nivel == 2 && item.Estatus == true && item.Materia == this.ListaMaterias[4].Nombre).Count + "/5";
            this.P53.Content = "600 " + this.ListaPreguntas.FindAll(item => item.Nivel == 3 && item.Estatus == true && item.Materia == this.ListaMaterias[4].Nombre).Count + "/5";
            this.P54.Content = "800 " + this.ListaPreguntas.FindAll(item => item.Nivel == 4 && item.Estatus == true && item.Materia == this.ListaMaterias[4].Nombre).Count + "/5";
            this.P55.Content = "1000 " + this.ListaPreguntas.FindAll(item => item.Nivel == 5 && item.Estatus == true && item.Materia == this.ListaMaterias[4].Nombre).Count + "/5";

        }
        public void GetPregunta(object sender)
        {

            Button b = sender as Button;
            string materia = "";
            if(b.Name.Equals("P11") || b.Name.Equals("P12") || b.Name.Equals("P13") || b.Name.Equals("P14") || b.Name.Equals("P15"))
                materia = this.M1.Content.ToString();
            if(b.Name.Equals("P21") || b.Name.Equals("P22") || b.Name.Equals("P23") || b.Name.Equals("P24") || b.Name.Equals("P25"))
                materia = this.M2.Content.ToString();
            if(b.Name.Equals("P31") || b.Name.Equals("P32") || b.Name.Equals("P33") || b.Name.Equals("P34") || b.Name.Equals("P35"))
                materia = this.M3.Content.ToString();
            if(b.Name.Equals("P41") || b.Name.Equals("P42") || b.Name.Equals("P43") || b.Name.Equals("P44") || b.Name.Equals("P45"))
                materia = this.M4.Content.ToString();
            if(b.Name.Equals("P51") || b.Name.Equals("P52") || b.Name.Equals("P53") || b.Name.Equals("P54") || b.Name.Equals("P55"))
                materia = this.M5.Content.ToString();
            int nivel = 0;
            //Random r = new Random();
            //int preguntaRamdom = r.Next(1, 5);
            List<Pregunta> pr = new List<Pregunta>();

            if(b.Content.ToString().Split(' ')[0].Equals("200"))
                nivel = 1;
            if(b.Content.ToString().Split(' ')[0].Equals("400"))
                nivel = 2;
            if(b.Content.ToString().Split(' ')[0].Equals("600"))
                nivel = 3;
            if(b.Content.ToString().Split(' ')[0].Equals("800"))
                nivel = 4;
            if(b.Content.ToString().Split(' ')[0].Equals("1000"))
                nivel = 5;

            
            PagePregunta p = new PagePregunta(materia, this.grado, nivel,this.Equipos);
            this.NavigationService.Navigate(p);


        }

        private void P11_Click(object sender, RoutedEventArgs e)
        {
            GetPregunta(sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageGradosConfig p = new PageGradosConfig();
            this.NavigationService.Navigate(p);
        }




        private void B1M_Click(object sender, RoutedEventArgs e)
        {
            LogicaMarcador l = new LogicaMarcador();

            l.Sumar(Equipos, 0,100,this.ronda);
        }

        private void B1R_Click(object sender, RoutedEventArgs e)
        {

            LogicaMarcador l = new LogicaMarcador();

            l.Restar(Equipos, 0, 100, this.ronda);
        }

        private void B2M_Click(object sender, RoutedEventArgs e)
        {
            LogicaMarcador l = new LogicaMarcador();

            l.Sumar(Equipos, 0, 100, this.ronda);
        }

        private void B2R_Click(object sender, RoutedEventArgs e)
        {
            LogicaMarcador l = new LogicaMarcador();

            l.Restar(Equipos, 0, 100, this.ronda);
        }
    }
}

