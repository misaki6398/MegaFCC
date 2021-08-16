drop table MEGA_ECM.TF_ALERTS;

create table MEGA_ECM.TF_ALERTS
( 
id raw(16) DEFAULT sys_guid() PRIMARY KEY,
CaseID raw(16),
AlertStatus VARCHAR2(100 CHAR),
AlertStatusCode NUMBER(3,0),
Input VARCHAR2(4000 CHAR) NOT NULL,
Score NUMBER(3,0),
MatchedListRecordId VARCHAR2(40 CHAR),
MatchType VARCHAR2(100 CHAR),
MatchedListKey VARCHAR2(4000 CHAR),
MatchedListSubKey VARCHAR2(4000 CHAR),
MatchedIndividualName VARCHAR2(4000 CHAR),
MatchedEntityName VARCHAR2(4000 CHAR),
MatchedKeyword VARCHAR2(4000 CHAR),
DetailsOfPort VARCHAR2(4000 CHAR),
DetailsOfBIC VARCHAR2(4000 CHAR),
DetailsOfGoods VARCHAR2(4000 CHAR),
CountryName VARCHAR2(4000 CHAR),
DataSource VARCHAR2(4000 CHAR),
Rule VARCHAR2(4000 CHAR),
IdentifierCode VARCHAR2(40 CHAR),
OriginalName VARCHAR2(4000 CHAR),
ImportCountry VARCHAR2(50 CHAR),
ExportCountry VARCHAR2(50 CHAR),
ImportCountryFrom VARCHAR2(50 CHAR),
ExportCountryTo VARCHAR2(50 CHAR),
ListSubTypeID  VARCHAR2(40 CHAR),
IdentifierTagName VARCHAR2(40 CHAR),
IdentifierValue VARCHAR2(250 CHAR),
ExpressionCode VARCHAR2(40 CHAR),
BlockName VARCHAR2(40 CHAR),
SequenceName VARCHAR2(40 CHAR),
TagName VARCHAR2(40 CHAR),
WebServiceID NUMBER(10,0),
Jurisdiction VARCHAR2(40 CHAR),
GroupName VARCHAR2(40 CHAR),
GroupTagIdentifierName VARCHAR2(255 CHAR),
GroupTagIdentifierValue VARCHAR2(255 CHAR),
ValidFlag NUMBER(1) DEFAULT 0 NOT NULL,
CreateUser varchar2(10) DEFAULT 'SYSTEM',
CreateDatetime Timestamp DEFAULT CURRENT_TIMESTAMP,
UpdateUser varchar2(10),
UpdateDatetime Timestamp
);

CREATE INDEX MEGA_ECM.TF_ALERTS_CaeID_INDEX ON MEGA_ECM.TF_ALERTS (CaseID DESC);
CREATE INDEX MEGA_ECM.TF_ALERTS_CreateDatetime_INDEX ON MEGA_ECM.TF_ALERTS (CreateDatetime DESC);
CREATE INDEX MEGA_ECM.TF_ALERTS_VALIDFLAG_INDEX ON MEGA_ECM.TF_ALERTS (VALIDFLAG DESC);
commit;