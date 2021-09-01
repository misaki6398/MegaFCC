using System;
using System.Threading.Tasks;
using MegaEcmBackEnd.Models.MegaEcm.Repositorys;
using CommonMegaAp11.Utilitys;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using SasReportService.Controllers;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.States.TransactionFilter;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Controllers.TransactionFilter
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class TfCasesController : BaseController
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
            try
            {
                var result = await _megaEcmUnitOfWork.TfCasesRepository.QueryCaseAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetById(Guid guid)
        {
            try
            {
                var result = await _megaEcmUnitOfWork.TfCasesRepository.QueryCaseAsync(GuidUtility.ToRaw16(guid));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("RawData/{guid}")]
        public async Task<IActionResult> GetRawdataById(Guid guid)
        {
            try
            {
                var result = await _megaEcmUnitOfWork.TfCasesRepository.QueryRawdataAsync(GuidUtility.ToRaw16(guid));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);                
            }
            
        }

        [HttpPost("Workflow")]
        public async Task<IActionResult> PostWorkflow([FromBody] StateResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.GetModelAttributeErrorMessage());
            }
            using (_megaEcmUnitOfWork)
            {
                try
                {
                    var caseContext = new CaseContext(resource.NowWorkflow, resource, _megaEcmUnitOfWork);
                    await caseContext.RunProcess(resource.NextWorkflow);
                    return Ok();
                }
                catch (TaskCanceledException ex)
                {
                    _logger.LogError(ex.ToString());
                    return BadRequest(ex.Message);
                }
                catch (OracleException ex)
                {
                    _logger.LogError(ex.ToString());
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    return BadRequest(ex.Message);
                }
                finally
                {
                    _megaEcmUnitOfWork.Rollback();
                }
            }
        }


        
    }
}