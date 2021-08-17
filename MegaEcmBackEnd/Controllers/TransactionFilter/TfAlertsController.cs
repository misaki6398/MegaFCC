using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Models.MegaEcm.Repositorys;
using CommonMegaAp11.Utilitys;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MegaEcmBackEnd.Controllers.TransactionFilter
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class TfAlertsController : ControllerBase
    {
        private readonly ILogger<TfAlertsController> _logger;
        private readonly MegaEcmUnitOfWork _megaEcmUnitOfWork;
        public TfAlertsController(ILogger<TfAlertsController> logger, MegaEcmUnitOfWork megaEcmUnitOfWork)
        {
            _logger = logger;
            _megaEcmUnitOfWork = megaEcmUnitOfWork;
        }

        [HttpGet("{caseId}")]
        public async Task<IActionResult> Get(Guid caseId)
        {
            try
            {
                var result = await _megaEcmUnitOfWork.TfAlertsRepository.QueryAlertAsync(GuidUtility.ToRaw16(caseId));
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("HitColumn/{caseId}")]
        public async Task<IActionResult> GetHitColumn(Guid caseId)
        {
            try
            {
                var result = await _megaEcmUnitOfWork.TfAlertsRepository.QueryHitColumnAsync(GuidUtility.ToRaw16(caseId));
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}