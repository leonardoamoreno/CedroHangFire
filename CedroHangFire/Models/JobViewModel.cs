using Hangfire;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CedroHangFire.Models
{
    public class JobViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Tipo de Job")]
        public EnumTypeJob TypeJob { get; set; }

        [Display(Name = "Expressão Cron")]
        public string CronExpression { get; set; }

        [Display(Name = "Data e hora para execução")]
        public DateTime? DateTime { get; set;}

        [Required(ErrorMessage = "Por favor envie uma dll")]
        [DataType(DataType.Upload)]
        [Display(Name = "DLL do programa")]        
        public IFormFile DllFile { get; set; }
                
        [DataType(DataType.Upload)]
        [Display(Name = "DLL(s) das dependências")]
        public ICollection<IFormFile> DependenciesFile { get; set; }
        
    }
}
