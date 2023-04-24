using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_DataLib.Model
{
    public enum DiasSemana
    {
        LUN, MAR, MIE, JUE, VIE, SAB, DOM
    }

    public class RangoTiempo
    {
        public int Id { get; set; }

        [Required]
        public string? DiaSemana { get; set; }

        [Required]
        public TimeSpan HoraInicial { get; set; }

        [Required]
        public TimeSpan HoraFinal { get; set; }

        public ICollection<ZonaHoraria_RangoTiempo>? ZonaHoraria_RangoTiempo { get; set; }

    }
}
