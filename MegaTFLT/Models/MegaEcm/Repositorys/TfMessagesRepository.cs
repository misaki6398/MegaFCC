using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Oracle;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.MegaEcm.Models;
using Oracle.ManagedDataAccess.Client;
using CommonMegaAp11.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Repositorys
{
    public class TfMessagesRepository : BaseRepository<TfMessageModel>
    {
        private readonly string _insertSql = @"
          INSERT INTO tf_messages (
                id,
                RawMessage,
                MessageDefinitionIdentifier,
                BusinessMessageIdentifier,
                BusinessService,
                OriginalCreateDate,
                FromId,
                ToId,
                Currency,
                Amount,
                SwallowId,
                BranchNO,
                ValidFlag,
                CreateUser,
                CreateDatetime,
                UpdateUser,
                UpdateDatetime
            ) VALUES (
                :id,
                :RawMessageClob,
                :MessageDefinitionIdentifier,
                :BusinessMessageIdentifier,
                :BusinessService,
                :OriginalCreateDate,
                :FromId,
                :ToId,
                :Currency,
                :Amount,
                :SwallowId,
                :BranchNO,
                :ValidFlag,
                :CreateUser,
                :CreateDatetime,
                :UpdateUser,
                :UpdateDatetime
            )
           ";
        public TfMessagesRepository(IDbTransaction transaction) : base(transaction)
        {
            this.InsertSql = _insertSql;
        }
        public override async Task<int> InsertAsync(TfMessageModel model)
        {
           model.RawMessageClob = OracleDBUtility.ConvertToClob(model.RawMessage, (Oracle.ManagedDataAccess.Client.OracleConnection)this.Connection);
           var parameter = new OracleDynamicParameters();    
           parameter.Add("RawMessageClob", model.RawMessageClob);


            return await base.InsertAsync(model, _insertSql);
        }
    }
}