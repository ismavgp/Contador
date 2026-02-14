using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinContador.Utils
{
    public static class FormatoNumerico
    {

        public static string FormatDecimal(decimal value)
        {
            // Redondear siempre a 2 decimales
            decimal rounded = Math.Round(value, 2);

            // Si es entero después del redondeo, mostrar sin decimales
            if (rounded % 1 == 0)
            {
                return rounded.ToString("N0", CultureInfo.CurrentCulture);
            }

            // Si tiene decimales distintos de .00, mostrar exactamente 2 decimales
            return rounded.ToString("N2", CultureInfo.CurrentCulture);
        }
    }
}
