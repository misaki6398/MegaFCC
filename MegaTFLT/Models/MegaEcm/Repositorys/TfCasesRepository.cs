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
                mxtype,
                swallowid,
                msgid,
                msgcurrency,
                msgamount,
                msgbranchno,
                casestatus,
                casestatuscode,
                assignee,
                owner,
                msgoriginalcreatedate,
                validflag,
                createuser,
                createdatetime,
                updateuser,
                updatedatetime
            ) VALUES (
                :id,
                :messageid,
                :mxtype,
                :swallowid,
                :msgid,
                :msgcurrency,
                :msgamount,
                :msgbranchno,
                :casestatus,
                :casestatuscode,
                :assignee,
                :owner,
                :msgoriginalcreatedate,
                :validflag,
                :createuser,
                :createdatetime,
                :updateuser,
                :updatedatetime
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