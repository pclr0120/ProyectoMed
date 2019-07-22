using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMed.Modelo
{
    public class Pregunta
    {
        private string id;
        private int grado;
        private string materia;
        private int nivel;
        private string descripcion;
        private string r1;//Repuestas
        private string r2;
        private string r3;
        private string rc;//repuesta correcta
        private bool estatus;
        private int turno = 0;


        public Pregunta() {
            this.Id = Guid.NewGuid().ToString();
            this.Grado = 0;
            this.Materia = "";
            this.Nivel = 0;
            this.Descripcion = "";
            this.R1 = "";
            this.R2 = "";
            this.R3 = "";
            this.Rc = "";
            this.Estatus = true;

        }
        public string Id { get => id; set => id = value; }
        public int Grado { get => grado; set => grado = value; }
        public string Materia { get => materia; set => materia = value.Trim(); }
        public int Nivel { get => nivel; set => nivel = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string R1 { get => r1; set => r1 = value; }
        public string R2 { get => r2; set => r2 = value; }
        public string R3 { get => r3; set => r3 = value; }
        public string Rc { get => rc; set => rc = value; }
        public bool Estatus { get => estatus; set => estatus = value; }
    }
}
