using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.Models.MegaEcm.Repositorys
{
    public class TfAlertsRepository : BaseRepository
    {
        private readonly string _readAlertSql = @"
            select 
                MatchedListRecordId,
                MatchType,
                Case 
                when MatchType = 'Individual' then MATCHEDINDIVIDUALNAME 
                when MatchType = 'Entity' then MATCHEDENTITYNAME 
                when MatchType = 'Country' then CountryName
                end MatchedName,
                MatchedListSubKey,
                Rule
            from TF_ALERTS
            where 
                ValidFlag = 1
            and CaseId = :caseId
        ";


        private readonly string _readHitColumnSql = @"
            select 
                distinct
                MatchedListRecordId,
                Input as MatchData,
                TagName
            from TF_ALERTS
            where 
                ValidFlag = 1
            and CaseId = :caseId
        ";
        public TfAlertsRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<IEnumerable<HitColumnResource>> QueryHitColumnAsync(byte[] caseId)
        {
            try
            {
                return await Connection.QueryAsync<HitColumnResource>(_readHitColumnSql, new { caseId = caseId }, Transaction);
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

        public async Task<IEnumerable<TfAlertsResource>> QueryAlertAsync(byte[] caseId)
        {
            try
            {
                return await Connection.QueryAsync<TfAlertsResource>(_readAlertSql, new { caseId = caseId }, Transaction);
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