using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMed.Modelo
{
    public class Tablero
    {
        public int Id = 0;
        public int Grado = 0;
        public string Nombre = "";
        public string Descripcion = "";
        public List<Pregunta> preguntas = new List<Pregunta>();
        public List<Equipo> equipos = new List<Equipo>();
        public bool Estatus = true;



        public Tablero()
        {

        }

        private List<Pregunta> Get(int grado, string materia, int nivel)
        {
            List<Pregunta> p = new List<Pregunta>();

       //     p = this.preguntas.FindAll(Item => Item.Grado == grado && Item.Materia == materia && Item.Nivel == nivel).Remove() ;
            return p;
        }


        public Pregunta GetRandom(int grado, string materia, int nivel)
        {
            Pregunta p = new Pregunta();
            Random random = new Random();
            int num = random.Next(1, 5);

            return p;
        }

    }
}
