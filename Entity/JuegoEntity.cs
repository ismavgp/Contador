using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;

namespace WinContador.Entity
{
    public class JuegoEntity
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public decimal Monto { get; set; }
        public string PorcentajeUtilidad { get; set; }
        public decimal Utilidad { get; set; }

    }


    public class JuegoResultEntity
    {
        public string Id { get; set; }

        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Monto { get; set; }
        public string PorcentajeUtilidad { get; set; }
        public string Utilidad { get; set; }

    }
}
