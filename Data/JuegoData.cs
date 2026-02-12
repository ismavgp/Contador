using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinContador.Entity;

namespace WinContador.Data
{
    public class JuegoData
    {

        public bool Guardar(string id, string fecha, string hora, decimal monto, string porcentajeUtilidad, string utilidad)
        {
            try
            {
                using (var context = new ContadorContext())
                {
                    var juego = new Entity.JuegoEntity
                    {
                        Id = id,
                        Fecha = fecha,
                        Hora = hora,
                        Monto = monto,
                        PorcentajeUtilidad = porcentajeUtilidad,
                        Utilidad = utilidad
                    };
                    context.Juegos.Add(juego);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario (por ejemplo, registrar el error)
                Console.WriteLine("Error al guardar el juego: " + ex.Message);
                return false;
            }
        }

        public List<JuegoEntity> ObtenerJuegos()
        {
            try
            {
                using (var context = new ContadorContext())
                {
                    return context.Juegos.ToList();
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario (por ejemplo, registrar el error)
                Console.WriteLine("Error al obtener los juegos: " + ex.Message);
                return new List<Entity.JuegoEntity>();
            }
        }
    }
}
