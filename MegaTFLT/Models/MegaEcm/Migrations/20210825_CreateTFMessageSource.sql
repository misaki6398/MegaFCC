DROP TABLE MEGA_ECM.TF_MESSAGE_SOURCE;

CREATE TABLE MEGA_ECM.TF_MESSAGE_SOURCE (
    Id raw(16) DEFAULT sys_guid() PRIMARY KEY,
    MessageSourceCode NUMBER(3, 0),
    MessageSource VARCHAR2(100 CHAR),
    ValidFlag NUMBER(1) DEFAULT 0 NOT NULL,
    CreateUser varchar2(10) NOT NULL,
    CreateDatetime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateUser varchar2(10),
    UpdateDatetime TIMESTAMP
);
CREATE INDEX MEGA_ECM.TF_TF_MESSAGE_SOURCE_MessageSourceCode_ValidFlag_INDEX ON MEGA_ECM.TF_MESSAGE_SOURCE (MessageSourceCode ASC, ValidFlag DESC);
commit;

Insert into MEGA_ECM.TF_MESSAGE_SOURCE (MESSAGESOURCECODE,MESSAGESOURCE,VALIDFLAG,CREATEUSER) values (0,'Mx',1,'008958');
Insert into MEGA_ECM.TF_MESSAGE_SOURCE (MESSAGESOURCECODE,MESSAGESOURCE,VALIDFLAG,CREATEUSER) values (1,'TxnObs',1,'008958');
Insert into MEGA_ECM.TF_MESSAGE_SOURCE (MESSAGESOURCECODE,MESSAGESOURCE,VALIDFLAG,CREATEUSER) values (2,'TxnTw',1,'008958');
Insert into MEGA_ECM.TF_MESSAGE_SOURCE (MESSAGESOURCECODE,MESSAGESOURCE,VALIDFLAG,CREATEUSER) values (3,'Mt',1,'008958');
commit;
