using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MegaTFLT.MegaEcm.Models;
using Oracle.ManagedDataAccess.Client;

namespace MegaTFLT.Models.MegaEcm.Repositorys
{
    public class ScreenSubConfigRepository : BaseRepository<TfScreenSubConfigModel>
    {
        private readonly string _sql = $@"
        SELECT 
             ID
            ,ScreenConfigId
            ,MESSAGESOURCECODE	
            ,TAGNAME
            ,EntityType
            ,NAMEADDRESS
            ,COUNTRYANDCITY
            ,GOODS
            ,NARRATIVE
            ,BICCODE
            ,PORT
            ,VALIDFLAG
            ,CREATEUSER
            ,CREATEDATETIME
            ,UPDATEUSER
            ,UPDATEDATETIME
        FROM SCREENSUBCONFIG
        Where 
            VALIDFLAG = 1
    ";

        public ScreenSubConfigRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public IEnumerable<TfScreenSubConfigModel> Query()
        {
            try
            {
                return Connection.Query<TfScreenSubConfigModel>(_sql, new { }, Transaction);
            }
            catch (OracleException)
            {
                throw;
            }
        }
    }
}