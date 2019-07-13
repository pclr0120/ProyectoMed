using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMed.Modelo
{
   public  class Equipo
    {
        private string id = "";
        private int grado = 0;
        private string nombre = "";
        private int puntaje = 0;
        private bool estatus = true;
        private string fecha = "";
        private bool turno = false;
        public Equipo() {
            this.Id = Guid.NewGuid().ToString();
            DateTime now = DateTime.Now;
            this.Fecha = now.ToString();
            this.grado = 0;
            this.nombre = "";
            this.puntaje = 0;
            this.Estatus = true;
            this.Turno = false;
            

        }

      
        public int Grado { get => grado; set => grado = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Puntaje { get => puntaje; set => puntaje = value; }

        public string Fecha { get => fecha; set => fecha = value; }
        public string Id { get => id; set => id = value; }
        public bool Estatus { get => estatus; set => estatus = value; }
        public bool Turno { get => turno; set => turno = value; }
    }
}
