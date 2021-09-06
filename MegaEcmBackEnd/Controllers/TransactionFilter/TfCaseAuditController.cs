using System;
using System.Threading.Tasks;
using AutoMapper;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Models.MegaEcm.Repositorys;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SasReportService.Controllers;

namespace MegaEcmBackEnd.Controllers.TransactionFilter
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class TfCaseAuditController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TfCaseAuditController> _logger;
        private readonly MegaEcmUnitOfWork _megaEcmUnitOfWork;

        public TfCaseAuditController(ILogger<TfCaseAuditController> logger, MegaEcmUnitOfWork megaEcmUnitOfWork, IMapper mapper)
        {
            _logger = logger;
            _megaEcmUnitOfWork = megaEcmUnitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{caseId}")]
        public async Task<IActionResult> GetById(Guid caseId)
        {
            try
            {
                var result = await _megaEcmUnitOfWork.TfCasesAuditsRepository.QueryAsync(GuidUtility.ToRaw16(caseId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}