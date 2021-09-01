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
using MegaEcmBackEnd.Models.FccmAtomic.Repositorys;
using System.Text.Json;
using System.Net.Http;
using SasReportService.Controllers;

namespace MegaEcmBackEnd.Controllers.TransactionFilter
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class TfAlertsController : BaseController
    {
        private readonly ILogger<TfAlertsController> _logger;
        private readonly MegaEcmUnitOfWork _megaEcmUnitOfWork;
        private readonly FccmAtomicUnitOfWork _fccmAtomicUnitOfWork;
        public TfAlertsController(ILogger<TfAlertsController> logger, MegaEcmUnitOfWork megaEcmUnitOfWork, FccmAtomicUnitOfWork fccmAtomicUnitOfWork)
        {
            _logger = logger;
            _megaEcmUnitOfWork = megaEcmUnitOfWork;
            _fccmAtomicUnitOfWork = fccmAtomicUnitOfWork;
        }

        [HttpGet("{caseId}")]
        public async Task<IActionResult> Get(Guid caseId)
        {
            try
            {
                var result = await _megaEcmUnitOfWork.TfAlertsRepository.QueryAlertAsync(GuidUtility.ToRaw16(caseId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListDetail/{alertId}")]
        public async Task<IActionResult> GetListDetail(Guid alertId)
        {
            try
            {
                string result = await _megaEcmUnitOfWork.TfAlertsRepository.QueryListAsync(GuidUtility.ToRaw16(alertId));
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AlertDecision")]
        public async Task<IActionResult> PostAlertDecision([FromBody] List<AlertDecisionResource> resources)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.GetModelAttributeErrorMessage());
            }
            try
            {
                var result = await _megaEcmUnitOfWork.TfAlertsRepository.UpdateAlertDecisionAsync(resources);
                _megaEcmUnitOfWork.Commit();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _megaEcmUnitOfWork.Rollback();
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}