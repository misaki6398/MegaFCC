DROP TABLE MEGA_ECM.SCREENCONFIG;
create table SCREENCONFIG (
    id raw(16) DEFAULT sys_guid() PRIMARY KEY,
    MessageSourceCode NUMBER(3, 0),
    TagName varchar2(100),
    HasSubFlag NUMBER(1) DEFAULT 0 NOT NULL,
    NameAddress NUMBER(1) DEFAULT 0 NOT NULL,
    CountryAndCity NUMBER(1) DEFAULT 0 NOT NULL,
    Goods NUMBER(1) DEFAULT 0 NOT NULL,
    Narrative NUMBER(1) DEFAULT 0 NOT NULL,
    BicCode NUMBER(1) DEFAULT 0 NOT NULL,
    Port NUMBER(1) DEFAULT 0 NOT NULL,
    ValidFlag NUMBER(1) DEFAULT 0 NOT NULL,
    CreateUser varchar2(10),
    CreateDatetime Timestamp DEFAULT CURRENT_TIMESTAMP,
    UpdateUser varchar2(10),
    UpdateDatetime Timestamp
);

CREATE INDEX MEGA_ECM.SCREENCONFIG_TAGNAME_INDEX ON MEGA_ECM.SCREENCONFIG (TAGNAME ASC, VALIDFLAG DESC);
CREATE INDEX MEGA_ECM.SCREENCONFIG_MESSAGESOURCECODE_TAGNAME_INDEX ON MEGA_ECM.SCREENCONFIG (
    MESSAGESOURCECODE ASC,
    TAGNAME ASC,
    VALIDFLAG DESC
);
CREATE INDEX MEGA_ECM.SCREENCONFIG_VALIDFLAG_INDEX ON MEGA_ECM.SCREENCONFIG (VALIDFLAG DESC);

Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('C92C642CD5B878DDE0531336A8C05837',0,'Nm',0,1,0,0,0,0,0,1,'008958',to_timestamp('10-08-2021 10.38.04.989837000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('C92C642CD5B978DDE0531336A8C05837',0,'Ctry',0,0,1,0,0,0,0,1,'008958',to_timestamp('10-08-2021 10.38.43.797252000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('C92C642CD5BA78DDE0531336A8C05837',0,'TwnNm',0,0,1,0,0,0,0,1,'008958',to_timestamp('10-08-2021 10.39.37.916445000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('C92C642CD5BB78DDE0531336A8C05837',0,'TwnLctnNm',0,0,1,0,0,0,0,1,'008958',to_timestamp('10-08-2021 10.40.29.985393000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('C91D9C4907136868E0531336A8C059AC',0,'BICFI',0,0,1,0,0,1,0,1,'009647',to_timestamp('09-08-2021 17.00.01.852149000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('CA3482441B5A19B2E0531336A8C0FE2A',1,'english_name',1,1,0,0,0,0,0,1,'008958',to_timestamp('23-08-2021 13.44.21.197633000','DD-MM-RR HH24.MI.SSXFF'),'008958',to_timestamp('27-08-2021 11.55.13.163639000','DD-MM-RR HH24.MI.SSXFF'));
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('CA3482441B5B19B2E0531336A8C0FE2A',1,'ccc_code',0,1,0,0,0,0,0,1,'008958',to_timestamp('23-08-2021 14.40.44.270074000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('CA3482441B5C19B2E0531336A8C0FE2A',1,'non_english_name',0,1,0,0,0,0,0,1,'008958',to_timestamp('23-08-2021 14.40.44.280548000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('CA3482441B5D19B2E0531336A8C0FE2A',1,'bic_swift_code',0,0,1,0,0,1,0,1,'008958',to_timestamp('23-08-2021 14.41.46.006557000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('CA3482441B5E19B2E0531336A8C0FE2A',1,'free_format_text',0,0,0,0,1,0,0,1,'008958',to_timestamp('23-08-2021 14.42.25.301845000','DD-MM-RR HH24.MI.SSXFF'),null,null);
Insert into MEGA_ECM.SCREENCONFIG (ID,MESSAGESOURCECODE,TAGNAME,HASSUBFLAG,NAMEADDRESS,COUNTRYANDCITY,GOODS,NARRATIVE,BICCODE,PORT,VALIDFLAG,CREATEUSER,CREATEDATETIME,UPDATEUSER,UPDATEDATETIME) values ('CA3482441B5F19B2E0531336A8C0FE2A',1,'country',0,0,1,0,0,0,0,1,'008958',to_timestamp('23-08-2021 14.42.48.426724000','DD-MM-RR HH24.MI.SSXFF'),null,null);

commit;