using System;
using System.Threading.Tasks;
using CommonMegaAp11.Utilitys;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Enums;
using MegaEcmBackEnd.Models.MegaEcm.Models;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.States.TransactionFilter
{
    public class AssignedStatus : BaseCaseStatus
    {
        public AssignedStatus(CaseContext context)
        {
            _context = context;
        }

        public override async Task Running(CaseStatus nextStatus)
        {
            var id = GuidUtility.ToRaw16(_context._resource.CaseId);
            bool isAssigneeEqualtoRequestId = true;
            // TODO: Is case assignee equal to request user

            switch (nextStatus)
            {
                case CaseStatus.ReleaseRecommand:
                case CaseStatus.BlockRecommand:
                    try
                    {
                        if (isAssigneeEqualtoRequestId)
                        {
                            // lock case
                            await _context._megaEcmUnitOfWork.TfCasesRepository.UpdateLockFlag(id, false);
                            await _context._megaEcmUnitOfWork.TfCasesRepository.UpdateCaseStatus(id, nextStatus);
                            var model = _context._mapper.Map<StateResource, TfCaseAuditsModel>(_context._resource);
                            model.CaseStatusCode = nextStatus;
                            await _context._megaEcmUnitOfWork.TfCasesAuditsRepository.InsertAsync(model);
                        }
                        else
                        {
                            _context._megaEcmUnitOfWork.Rollback();
                            throw new TaskCanceledException("Case assignee not equal to request user");
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