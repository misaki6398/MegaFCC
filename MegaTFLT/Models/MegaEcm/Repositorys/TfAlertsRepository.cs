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
    public class TfAlertsRepository : BaseRepository<TfAlertsModel>
    {
        private readonly string _insertSql = @"
          INSERT INTO tf_alerts (
                id,
                caseid,
                alertstatus,
                alertstatuscode,
                input,
                score,
                matchedlistrecordid,
                matchtype,
                matchedlistkey,
                matchedlistsubkey,
                matchedindividualname,
                matchedentityname,
                matchedkeyword,
                detailsofport,
                detailsofbic,
                detailsofgoods,
                countryname,
                datasource,
                rule,
                identifiercode,
                originalname,
                importcountry,
                exportcountry,
                importcountryfrom,
                exportcountryto,
                listsubtypeid,
                identifiertagname,
                identifiervalue,
                expressioncode,
                blockname,
                sequencename,
                tagname,
                webserviceid,
                jurisdiction,
                groupname,
                grouptagidentifiername,
                grouptagidentifiervalue,
                validflag,
                createuser,
                createdatetime,
                updateuser,
                updatedatetime
            ) VALUES (
                :id,
                :CaseId,
                :AlertStatus,
                :AlertStatusCode,
                :Input,
                :Score,
                :MatchedListRecordId,
                :MatchType,
                :MatchedListKey,
                :MatchedListSubKey,
                :MatchedIndividualName,
                :MatchedEntityName,
                :MatchedKeyword,
                :DetailsOfPort,
                :DetailsOfBIC,
                :DetailsOfGoods,
                :CountryName,
                :DataSource,
                :Rule,
                :IdentifierCode,
                :OriginalName,
                :ImportCountry,
                :ExportCountry,
                :ImportCountryFrom,
                :ExportCountryTo,
                :ListSubTypeID,
                :IdentifierTagName,
                :IdentifierValue,
                :ExpressionCode,
                :BlockName,
                :SequenceName,
                :TagName,
                :WebServiceID,
                :Jurisdiction,
                :GroupName,
                :GroupTagIdentifierName,
                :GroupTagIdentifierValue,
                :ValidFlag,
                :CreateUser,
                :CreateDatetime,
                :UpdateUser,
                :UpdateDatetime
            )
           ";
        public TfAlertsRepository(IDbTransaction transaction) : base(transaction)
        {
            this.InsertSql = _insertSql;
        }
        //public async Task<int> InsertAsync(List<TfAlertsModel> models) => await base.InsertAsync(models, _insertSql);

    }
}