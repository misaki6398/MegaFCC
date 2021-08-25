using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Models.MegaEcm.Models;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.Models.MegaEcm.Repositorys
{
    public class TfCasesAuditsRepository : BaseRepository
    {
        private readonly string _insertAuditSql = @"
           INSERT INTO tf_case_audits (
                Id,
                CaseId,
                UserComment,
                CaseStatusCode,
                ValidFlag,
                CreateUser,
                CreateDatetime,
                UpdateUser,
                UpdateDatetime
            ) VALUES (
                :Id,
                :CaseId,
                :UserComment,
                :CaseStatusCode,
                :ValidFlag,
                '009647',
                CURRENT_TIMESTAMP,
                :UpdateUser,
                :UpdateDatetime
            )
        ";

        public TfCasesAuditsRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<int> InsertAsync(TfCaseAuditsModel model)
        {
            try
            {
                return await Connection.ExecuteAsync(_insertAuditSql, model, Transaction);
            }
            catch (OracleException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}