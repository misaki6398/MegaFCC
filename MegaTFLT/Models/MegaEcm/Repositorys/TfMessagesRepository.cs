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
    public class TfMessagesRepository : BaseRepository
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
                :RawMessage,
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
        //public async Task<int> InsetAsync(TfMessageModel model) => await base.InsertAsync(model, _insertSql);
    }
}