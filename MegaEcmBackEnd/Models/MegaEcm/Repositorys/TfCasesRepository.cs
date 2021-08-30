using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using MegaEcmBackEnd.Enums;

namespace MegaEcmBackEnd.Models.MegaEcm.Repositorys
{
    public class TfCasesRepository : BaseRepository
    {
        private readonly string _readCaseSql = @"
            SELECT
                MessageType,
                tf_cases.SwallowId,
                BusinessMessageIdentifier,
                Currency,
                Amount,
                BranchNo,
                CaseStatus,
                tf_cases.CaseStatusCode,
                Assignee,
                tf_cases.CreateDateTime,
                tf_cases.Id as caseIdRaw
            FROM tf_cases
            JOIN tf_case_status ON tf_cases.CaseStatusCode = tf_case_status.CaseStatusCode and tf_case_status.validflag = 1
            WHERE 
                tf_cases.validflag = 1
            -- AND tf_cases.casestatuscode IN (0,1,2,3)
        ";
        private readonly string _readRawDataSql = @"
            select 
                RAWMESSAGE
            from tf_cases 
            join tf_messages on tf_cases.messageid = tf_messages.Id
            where 
                tf_cases.Id = :CaseId
            and tf_messages.validflag = 1 
            and tf_cases.validflag = 1
        ";

        private readonly string _isCaseLockedSql = @"
            select
                LockFlag
            from mega_ecm.tf_cases
            where id = :caseId
            and ValidFlag = 1
            FOR UPDATE NOWAIT
        ";

        private readonly string _updateCaseLockedSql = @"
            update
              mega_ecm.tf_cases 
            set LockFlag = :lockFlag
            where id = :caseId
            and ValidFlag = 1
        ";

        private readonly string _updateCaseStatusCodeSql = @"
            update
              mega_ecm.tf_cases 
            set 
            Assignee = '009647',
            CaseStatusCode = :CaseStatus
            where id = :caseId
            and ValidFlag = 1
        ";


        public TfCasesRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<IEnumerable<TfCasesResource>> QueryCaseAsync()
        {
            try
            {
                return await Connection.QueryAsync<TfCasesResource>(_readCaseSql, new { }, Transaction);
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
        public async Task<IEnumerable<TfCasesResource>> QueryCaseAsync(byte[] caseId)
        {
            string sql = $"{_readCaseSql} and tf_cases.Id = :caseId";
            try
            {
                return await Connection.QueryAsync<TfCasesResource>(sql, new { CaseId = caseId }, Transaction);
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

        public async Task<IEnumerable<string>> QueryRawdataAsync(byte[] caseId)
        {
            string sql = $"{_readCaseSql} and tf_cases.Id = :caseId";
            try
            {
                return await Connection.QueryAsync<string>(_readRawDataSql, new { CaseId = caseId }, Transaction);
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

        public async Task<bool> QueryIsCaseLock(byte[] caseId)
        {
            try
            {
                var result = await Connection.QueryAsync<bool>(_isCaseLockedSql, new { CaseId = caseId }, Transaction);

                return result.FirstOrDefault();
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

        public async Task<int> UpdateLockFlag(byte[] caseId, bool lockFlag)
        {
            try
            {
                return await Connection.ExecuteAsync(_updateCaseLockedSql, new { CaseId = caseId, LockFlag = (lockFlag) ? 1 : 0 }, Transaction);
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


        public async Task<int> UpdateCaseStatus(byte[] caseId, CaseStatus caseStatus)
        {
            try
            {
                return await Connection.ExecuteAsync(_updateCaseStatusCodeSql, new { CaseId = caseId, CaseStatus = caseStatus }, Transaction);
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