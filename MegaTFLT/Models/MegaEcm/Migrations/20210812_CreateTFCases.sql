DROP TABLE MEGA_ECM.TF_CASES;

CREATE TABLE MEGA_ECM.TF_CASES (
	id raw(16) DEFAULT sys_guid() PRIMARY KEY,
	MessageId raw(16),
	MxType VARCHAR2(40 CHAR),
	SwallowId VARCHAR2(40 CHAR),
	MsgId VARCHAR2(40 CHAR),
	MsgCurrency varchar2(10),
	MsgAmount NUMBER(30, 10),
	MsgBranchNO VARCHAR2(10 CHAR),
	CaseStatusCode NUMBER(3, 0),
	Assignee varchar2(10),
	OWNER varchar2(10),
	MsgOriginalCreateDate TIMESTAMP,
	Locked  NUMBER(1) DEFAULT 0 NOT NULL,
	ValidFlag NUMBER(1) DEFAULT 0 NOT NULL,
	CreateUser varchar2(10) DEFAULT 'SYSTEM',
	CreateDatetime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	UpdateUser varchar2(10),
	UpdateDatetime TIMESTAMP
	);

CREATE INDEX MEGA_ECM.TF_CASES_MessageId_INDEX ON MEGA_ECM.TF_ALERTS (MessageId DESC);

CREATE INDEX MEGA_ECM.TF_CASES_CreateDatetime_INDEX ON MEGA_ECM.TF_CASES (CreateDatetime DESC);

CREATE INDEX MEGA_ECM.TF_CASES_VALIDFLAG_INDEX ON MEGA_ECM.TF_CASES (VALIDFLAG DESC);

COMMIT;
