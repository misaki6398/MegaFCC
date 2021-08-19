using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Utilitys;
using MegaTFLT.Models.MegaEcm.Models;
using Oracle.ManagedDataAccess.Client;

namespace MegaTFLT.Models.MegaEcm.Repositorys
{
    public class TfAlertListDetailRepository : BaseRepository
    {
        private readonly string _insertSql = $@"
            INSERT INTO
                tf_alert_list_detail (
                    alertid,
                    listsubtypeid,
                    listrecord
                )
            values(
                    :AlertId,
                    :ListSubTypeId,
                    (
                        SELECT
                            C_LIST_REC
                        FROM
                            { ConfigUtility.FccmAtomicSchema }.FSI_RT_LIST_DATA
                        WHERE
                            V_LIST_SUB_TYPE_ID = :ListSubTypeId 
                        FETCH FIRST ROW ONLY
                    )
                )
           ";


        public TfAlertListDetailRepository(IDbTransaction transaction) : base(transaction)
        {
            this.InsertSql = _insertSql;
        }
        //public async Task<int> InsertAsync(List<TfAlertListDetailModel> models) => await base.InsertAsync(models, _insertSql);
    }
}