using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoMed.Logica
{
    public class Validacion
    {

        private bool PregutasCompletadas()
        {

            LogicaExportarData LogDataConfig = new LogicaExportarData();
            try
            {

                int P1= LogDataConfig.GetImport1(1).Count;
                int P2 = LogDataConfig.GetImport1(2).Count;
                int P3 = LogDataConfig.GetImport1(3).Count;
                int P4 = LogDataConfig.GetImport1(4).Count;
                int total = P1 + P2 + P3 + P4;
                if(total == 500)
                    return true;
                else {
                    return false;
                }
                   
            }
            catch(Exception e)
            {


                return false;
            }

        }
        private bool MateriasCompletadas()
        {

            LogicaMaterias LogDataConfig = new LogicaMaterias();
            try
            {

                int P1 = LogDataConfig.GetImport(1).Count;
                int P2 = LogDataConfig.GetImport(2).Count;
                int P3 = LogDataConfig.GetImport(3).Count;
                int P4 = LogDataConfig.GetImport(4).Count;
                int total = P1 + P2 + P3 + P4;
                if(total == 20)
                    return true;
                else
                {
                    return false;
                }

            }
            catch(Exception e)
            {


                return false;
            }

        }

        private bool EquiposCompletos()
        {

            LogicaExportarData LogDataConfig = new LogicaExportarData();
            try
            {

                int P1 = LogDataConfig.GetImportEequipos(1).Count;
                int P2 = LogDataConfig.GetImportEequipos(2).Count;
                int P3 = LogDataConfig.GetImportEequipos(3).Count;
                int P4 = LogDataConfig.GetImportEequipos(4).Count;
                int total = P1 + P2 + P3 + P4;
                if(total >= 8)
                    return true;
                else
                {
                    return false;
                }

            }
            catch(Exception e)
            {


                return false;
            }

        }
        public bool ContinuarAGrado() {

            try
            {
                if(MateriasCompletadas())
                    if(EquiposCompletos())
                        if(PregutasCompletadas())
                            return true;
                   
                            return false;
            }
            catch(Exception err)
            {

                return false;
            }
        }
        public bool  IsValido(int grado)
        {
            LogicaPreguntasHistorial LP = new LogicaPreguntasHistorial();

 
            try
            {

                int p1 = LP.GetImport1(grado).Count;

                if(p1 >= 5)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {


                return false;
            }

        }


    }


}
