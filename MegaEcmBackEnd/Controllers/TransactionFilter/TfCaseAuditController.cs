using System.Threading.Tasks;
using AutoMapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Models.MegaEcm.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SasReportService.Controllers;

namespace MegaEcmBackEnd.Controllers.TransactionFilter
{
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

        // [HttpPost("")]
        // public async Task<IActionResult> Post([FromBody] TfCaseAuditsResource resource)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(this.GetModelAttributeErrorMessage());
        //     }
        //     return Ok();
           
        // }

    }
}