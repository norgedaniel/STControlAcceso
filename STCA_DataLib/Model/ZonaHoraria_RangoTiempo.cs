using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_DataLib.Model
{
    public class ZonaHoraria_RangoTiempo
    {
        public int ZonaHorariaId { get; set; }
        public ZonaHoraria? ZonaHoraria { get; set; }

        public int RangoTiempoId { get; set; }

        public RangoTiempo? RangoTiempo { get; set; }

    }
}
