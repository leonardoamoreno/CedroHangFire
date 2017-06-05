using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CedroHangFire.Models
{
    public enum EnumTypeJob
    {
        [Display(Name = "Executar Imediatamente somente uma vez")]
        RunImmediately,
        [Display(Name = "Execução Agendada somente uma vez")]
        RunSchedule,
        [Display(Name = "Execução Recorrente programada")]
        RunScheduleRecurring
    }
}
