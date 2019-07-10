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
    /// Lógica de interacción para PagePreguntasConfig.xaml
    /// </summary>
    public partial class PagePreguntasConfig : Page
    {

        private List<Pregunta> ListaOriginal;
        private List<Pregunta> ListaConCambios;
     
        int grado = 0;
       // private List<Pregunta> ListaPreguntasNivel2;//Sin cambio
        private LogicaExportarData LData;
    
        public PagePreguntasConfig()
        {
            InitializeComponent();
            this.LData = new LogicaExportarData();
            this.ListaOriginal = new List<Pregunta>();
            this.Loaded += AdministrarPreguntas_Loaded;
        }
        public PagePreguntasConfig(string materia,int grado,int nivel) : this(){
            try
            {
                this.grado = grado;

                LogicaExportarData l = new LogicaExportarData();
                if(l.GetImport1(grado).FindAll(i=>i.Nivel==nivel && i.Materia==materia).Count == 0)
                {
                    LogicaMaterias m = new LogicaMaterias();

                    this.titulo.Content = materia + "/" + " Nivel " + nivel.ToString();
                    for(int i = 0; i < 5; i++)
                    {
                        Pregunta p = new Pregunta();
                        p.Grado = grado;
                        p.Nivel = nivel;
                        p.Materia = materia;
                        this.ListaOriginal.Add(p);
                       

                    }
                    this.ListaConCambios = this.ListaOriginal.FindAll(i => i.Nivel == nivel && i.Materia == materia);
                }
                else {

                    this.ListaOriginal = l.GetImport1(grado);
                    this.ListaConCambios = this.ListaOriginal.FindAll(i => i.Nivel == nivel && i.Materia == materia);
                }

                
         

            }
            catch(Exception)
            {

                throw;
            }
          


        }
      
     

        private void AdministrarPreguntas_Loaded(object sender, RoutedEventArgs e)
        {
          //  this.ListaPreguntas = this.LData.GetImport1();
            this.Griddata.ItemsSource = this.ListaConCambios;
            this.Griddata.Items.Refresh();
        }

        public void SoloNumeros(TextCompositionEventArgs e)
        {
            //se convierte a Ascci del la tecla presionada 
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            //verificamos que se encuentre en ese rango que son entre el 0 y el 9 
            if (ascci >= 48 && ascci <= 57)
                e.Handled = false;
            else e.Handled = true;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);

        }

        private void Griddata_SelectedCellsChanged_1(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {

                IList<DataGridCellInfo> celdas = e.AddedCells;
                //Video valor = celdas[0].Item as Video;



            }
            catch (Exception err)
            {
                MessageBox.Show("Error al cargar el video: " + e.ToString(), "Video");
            }

        }

        private void DataGridTemplateColumn_Error(object sender, ValidationErrorEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result0 = System.Windows.MessageBox.Show("Desea cancelar los cambios " + grado.ToString() + "?",
                                             "Notificación",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);
            if(result0 == MessageBoxResult.Yes)
            {
                PageTableroCofig p = new PageTableroCofig(this.grado);
                NavigationService.Navigate(p);
            }
        }

        private void Griddata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private bool PuedeGuardar(List<Pregunta> lm)
        {



                foreach(var item in lm)
                {
                    if(item.Descripcion == "" || item.R1=="" || item.R2 == "" || item.R3 == "" || item.Rc == "")
                        return false;
                }
                return true;

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if(PuedeGuardar(this.ListaConCambios))
                {
                    if(this.LData.GuardarTxt(this.ListaConCambios, this.ListaOriginal, this.grado))
                    {

                        PageTableroCofig p = new PageTableroCofig(this.grado);
                        NavigationService.Navigate(p);
                    }
                }
                else
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Para poder continuar debe de capturar toda la informacion de las 5 preguntas.",
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
    }
}
