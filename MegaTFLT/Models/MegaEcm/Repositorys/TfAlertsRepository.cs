using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.MegaEcm.Models;
using Oracle.ManagedDataAccess.Client;
using MegaTFLT.Utilitys;

namespace MegaTFLT.Models.MegaEcm.Repositorys
{
    public class TfAlertsRepository : BaseRepository<TfAlertsModel>
    {
        private readonly string _insertSql = $@"
          INSERT INTO tf_alerts (
                id,
                caseid,
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
                listrecord,
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
                (
                    SELECT
                        C_LIST_REC
                    FROM
                        { ConfigUtility.FccmAtomicSchema }.FSI_RT_LIST_DATA
                    WHERE
                        V_LIST_SUB_TYPE_ID = :ListSubTypeID 
                    FETCH FIRST ROW ONLY
                ),
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