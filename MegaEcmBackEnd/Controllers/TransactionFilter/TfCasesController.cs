using System;
using System.Threading.Tasks;
using MegaEcmBackEnd.Models.MegaEcm.Repositorys;
using MegaEcmBackEnd.Utilitys;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MegaEcmBackEnd.Controllers.TransactionFilter
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class TfCasesController : ControllerBase
    {
        private readonly ILogger<TfCasesController> _logger;
        private readonly MegaEcmUnitOfWork _megaEcmUnitOfWork;
        public TfCasesController(ILogger<TfCasesController> logger, MegaEcmUnitOfWork megaEcmUnitOfWork)
        {
            _logger = logger;
            _megaEcmUnitOfWork = megaEcmUnitOfWork;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(string caseId)
        {
            var result = await _megaEcmUnitOfWork.TfCasesRepository.QueryCaseAsync();
            return Ok(result);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var result = await _megaEcmUnitOfWork.TfCasesRepository.QueryCaseAsync(GuidUtility.ToRaw16(guid));
            return Ok(result);
        }
    }
}