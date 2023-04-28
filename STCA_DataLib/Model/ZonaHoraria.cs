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
        [MaxLength(40)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<ZonaHoraria_RangoTiempo>? ZonaHoraria_RangoTiempo { get; set; }

    }
}
