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
    public class ScreenConfigRepository : BaseRepository<TfScreenConfigModel>
    {
        private readonly string _sql = $@"
        SELECT 
             ID
            ,MESSAGESOURCECODE	
            ,TAGNAME
            ,ENTITYTYPE
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
        FROM SCREENCONFIG
        Where 
            VALIDFLAG = 1
    ";

        public ScreenConfigRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public IEnumerable<TfScreenConfigModel> Query()
        {
            try
            {
                return Connection.Query<TfScreenConfigModel>(_sql, new { }, Transaction);
            }
            catch (OracleException)
            {
                throw;
            }
        }
    }
}