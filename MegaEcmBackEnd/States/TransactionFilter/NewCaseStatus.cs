using System;
using System.Threading.Tasks;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Enums;
using MegaEcmBackEnd.Models.MegaEcm.Models;
using MegaEcmBackEnd.Models.MegaEcm.Repositorys;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class NewCaseStatus : BaseCaseStatus
    {


        public NewCaseStatus(CaseContext context)
        {
            _context = context;

        }

        public override async Task Running(CaseStatus nextStatus)
        {

            switch (nextStatus)
            {
                case CaseStatus.Assigned:
                    bool isCaseLock = true;
                    var id = GuidUtility.ToRaw16(_context._resource.CaseId);
                    try
                    {
                        isCaseLock = await _context._megaEcmUnitOfWork.TfCasesRepository.QueryIsCaseLock(id);
                        if (!isCaseLock)
                        {
                            // lock case                            
                            await _context._megaEcmUnitOfWork.TfCasesRepository.UpdateLockFlag(id, true);
                            await _context._megaEcmUnitOfWork.TfCasesRepository.UpdateAssignCase(id);
                            var model = _context._mapper.Map<StateResource, TfCaseAuditsModel>(_context._resource);
                            model.CaseStatusCode = CaseStatus.Assigned;
                            await _context._megaEcmUnitOfWork.TfCasesAuditsRepository.InsertAsync(model);
                        }
                        else
                        {
                            _context._megaEcmUnitOfWork.Rollback();
                            throw new TaskCanceledException("Case Already Locked");
                        }
                        _context._megaEcmUnitOfWork.Commit();
                    }
                    catch (OracleException)
                    {
                        _context._megaEcmUnitOfWork.Rollback();
                        throw;
                    }
                    catch (Exception)
                    {
                        _context._megaEcmUnitOfWork.Rollback();
                        throw;
                    }
                    break;
                default:
                    throw new ArgumentException("Next status error");
            }
        }
    }
}