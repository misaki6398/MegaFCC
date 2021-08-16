using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.MegaEcm.Models;
using Oracle.ManagedDataAccess.Client;

namespace MegaTFLT.Models.MegaEcm.Repositorys
{
    public class TfCasesRepository : BaseRepository
    {
        private readonly string _insertSql = @"
            INSERT INTO tf_cases (
                id,
                messageid,
                casestatus,
                casestatuscode,
                validflag,
                createuser,
                createdatetime,
                updateuser,
                updatedatetime
            ) VALUES (
                :Id,
                :MessageId,
                :CaseStatus,
                :CaseStatusCode,
                :ValidFlag,
                :CreateUser,
                :CreateDatetime,
                :UpdateUser,
                :UpdateDatetime
            )
           ";
        public TfCasesRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<int> InsertAsync(TfCasesModel models)
        {
            try
            {
                return await Connection.ExecuteAsync(_insertSql, models, Transaction);
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