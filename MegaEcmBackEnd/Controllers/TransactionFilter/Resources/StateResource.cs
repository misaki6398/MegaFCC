using System;
using System.ComponentModel.DataAnnotations;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Controllers.TransactionFilter.Resources
{
    public class StateResource
    {
        [Required]
        public Guid CaseId { get; set; }
        public string UserComment { get; set; }  

    }
}