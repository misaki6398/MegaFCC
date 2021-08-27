using System;
using System.ComponentModel.DataAnnotations;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Controllers.TransactionFilter.Resources
{
    public class AlertDecisionResource
    {

        private Guid alertId;
        public Guid AlertId
        {
            get { return alertId; }
            set
            {
                alertId = value;
                alertIdRaw = GuidUtility.ToRaw16(value);
            }
        }

        public byte[] alertIdRaw { get; set; }
        // [Required]
        // public Guid AlertId { get; set; }
        // public byte[] alertId { get; set; } = { GuidUtility.ToRaw16(AlertId) };

        [Required]
        public AlertStatus AlertStatusCode { get; set; }

    }
}