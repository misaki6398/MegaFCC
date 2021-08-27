using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using MegaEcmBackEnd.Enums;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.Models.MegaEcm.Repositorys
{
    public class TfAlertsRepository : BaseRepository
    {
        private readonly string _readAlertSql = @"
            select 
                Id as alertIdRaw,
                MatchedListRecordId,
                MatchType,
                Case 
                when MATCHEDINDIVIDUALNAME is not null then MATCHEDINDIVIDUALNAME 
                when MATCHEDENTITYNAME is not null then MATCHEDENTITYNAME 
                when CountryName is not null then CountryName 
                when DETAILSOFPORT is not null then DETAILSOFPORT 
                when DETAILSOFBIC is not null then DETAILSOFBIC 
                when DETAILSOFGOODS is not null then DETAILSOFGOODS                
                end MatchedName,
                MatchedListSubKey,
                Rule,
                ListSubTypeId,
                TagName,
                AlertStatusCode
            from TF_ALERTS
            where 
                ValidFlag = 1
            and CaseId = :caseId
        ";


        private readonly string _readHitColumnSql = @"
            select 
                distinct
                ListSubTypeId,
                Input as MatchData,
                TagName
            from TF_ALERTS
            where 
                ValidFlag = 1
            and CaseId = :caseId
        ";

        private readonly string _updateAlertDecisionSql = @"          
            UPDATE
                 TF_ALERTS
            SET 
                AlertStatusCode = :AlertStatusCode,
                UpdateUser = '009647',
                UpdateDatetime = CURRENT_TIMESTAMP
            where 
                ValidFlag = 1
            and Id = :alertIdRaw
        ";

        private readonly string _readListDetailSql = @"          
            SELECT
                listrecord   
            FROM
                tf_alerts
            where Id = :AlertId
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
        
        public async Task<string> QueryListAsync(byte[] alertId)
        {
            try
            {
                var result = await Connection.QueryAsync<string>(_readListDetailSql, new { AlertId = alertId }, Transaction);
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

        public async Task<int> UpdateAlertDecisionAsync(List<AlertDecisionResource> resources)
        {
            try
            {
                return await Connection.ExecuteAsync(_updateAlertDecisionSql, resources, Transaction);
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