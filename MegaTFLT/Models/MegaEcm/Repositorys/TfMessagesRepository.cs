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

            //byte[] newvalue = System.Text.Encoding.Unicode.GetBytes(model.RawMessage);
            //var clob = new OracleClob((Oracle.ManagedDataAccess.Client.OracleConnection)this.Connection);
            //clob.Write(newvalue, 0, newvalue.Length);

            parameter.Add("id", model.id);
            parameter.Add("RawMessageClob", model.RawMessageClob);
            parameter.Add("MessageDefinitionIdentifier", model.MessageDefinitionIdentifier);
            parameter.Add("BusinessMessageIdentifier", model.BusinessMessageIdentifier);
            parameter.Add("BusinessService", model.BusinessService);
            parameter.Add("OriginalCreateDate", model.OriginalCreateDate);
            parameter.Add("FromId", model.FromId);
            parameter.Add("ToId", model.ToId);
            parameter.Add("Currency", model.Currency);
            parameter.Add("Amount", model.Amount);
            parameter.Add("SwallowId", model.SwallowId);
            parameter.Add("BranchNO", model.BranchNO);
            parameter.Add("ValidFlag", model.ValidFlag);
            parameter.Add("CreateUser", model.CreateUser);
            parameter.Add("CreateDatetime", model.CreateDatetime);
            parameter.Add("UpdateUser", model.UpdateUser);
            parameter.Add("UpdateDatetime", model.UpdateDatetime);

            try
            {
                return await Connection.ExecuteAsync(this.InsertSql, parameter, Transaction);
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