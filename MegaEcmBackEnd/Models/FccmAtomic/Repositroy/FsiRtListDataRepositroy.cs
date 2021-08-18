using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaEcmBackEnd.Controllers.TransactionFilter.Resources;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.Models.FccmAtomic.Repositorys
{
    public class FsiRtListDataRepositroy : BaseRepository
    {
        private readonly string _readListSql = @"
            select 
                C_LIST_REC
            from 
            fsi_rt_list_data 
            where v_list_sub_type_id = :ListSubTypeId
            fetch first row only
        ";

        public FsiRtListDataRepositroy(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<IEnumerable<string>> QueryListAsync(string listSubTypeId)
        {
            try
            {
                return await Connection.QueryAsync<string>(_readListSql, new { ListSubTypeId = listSubTypeId }, Transaction);
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