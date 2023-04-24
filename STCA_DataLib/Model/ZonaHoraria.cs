using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_DataLib.Model
{
    public class ZonaHoraria
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public ICollection<ZonaHoraria_RangoTiempo>? ZonaHoraria_RangoTiempo { get; set; }

    }
}
