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
        LUNES, MARTES, MIERCOLES, JUEVES, VIERNES, SABADO, DOMINGO
    }

    public class RangoTiempo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Día Semana")]
        public string DiaSemana { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Hora Inicial")]
        public TimeSpan HoraInicial { get; set; }

        [Required]
        [Display(Name = "Hora Final")]
        public TimeSpan HoraFinal { get; set; }

        public ICollection<ZonaHoraria_RangoTiempo>? ZonaHoraria_RangoTiempo { get; set; }

    }
}
