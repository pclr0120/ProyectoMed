using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMed.Modelo
{
    public class Materias
    {
        private string id = "";
        private int grado = 0;
        private string nombre = "";
        private bool estatus = true;
        private string fecha = "";
        public Materias() {
            this.Id = Guid.NewGuid().ToString();
            DateTime now = DateTime.Now;
            this.Fecha = now.ToString();
            this.Estatus = true;
        }
        public string Id { get => id; set => id = value; }
        public int Grado { get => grado; set => grado = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Estatus { get => estatus; set => estatus = value; }
        public string Fecha { get => fecha; set => fecha = value; }
    }
}
