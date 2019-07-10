using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMed.Modelo
{
   public class Ronda
    {

        private string id = "";
        private int grado = 0;
        private int totalRonda = 0;
        private int rondaActual = 0;
        private int turno = 0;
        private bool estatus = true;
        private string fecha = "";
        private string ganador="";
        private int equipo1Puntaje = 0;
        private int equipo2Puntaje = 0;
        private string equipo1="";
        private string equipo2 = "";
        public Ronda() {
            this.Id = Guid.NewGuid().ToString();
            DateTime now = DateTime.Now;
            this.Fecha = now.ToString();
            this.grado = 0;
            this.RondaActual = 1;
            this.TotalRonda = 3;
            this.Turno = 0;
            this.Estatus = true;
            this.Equipo1 = "";
            this.Equipo2 = "";
            this.Ganador = "N";
        }

        public string Id { get => id; set => id = value; }
        public int Grado { get => grado; set => grado = value; }
        public int TotalRonda { get => totalRonda; set => totalRonda = value; }
        public int Turno { get => turno; set => turno = value; }
        public bool Estatus { get => estatus; set => estatus = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Ganador { get => ganador; set => ganador = value; }
        public string Equipo1 { get => equipo1; set => equipo1 = value; }
        public string Equipo2 { get => equipo2; set => equipo2 = value; }
        public int RondaActual { get => rondaActual; set => rondaActual = value; }
        public int Equipo1Puntaje { get => equipo1Puntaje; set => equipo1Puntaje = value; }
        public int Equipo2Puntaje { get => equipo2Puntaje; set => equipo2Puntaje = value; }
    }
}
