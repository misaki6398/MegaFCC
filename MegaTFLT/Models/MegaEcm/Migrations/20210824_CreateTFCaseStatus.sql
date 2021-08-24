DROP TABLE MEGA_ECM.TF_CASE_STATUS;

CREATE TABLE MEGA_ECM.TF_CASE_STATUS (
    Id raw(16) DEFAULT sys_guid() PRIMARY KEY,
    CaseStatusCode NUMBER(3, 0),
    CaseStatus VARCHAR2(100 CHAR),
    ValidFlag NUMBER(1) DEFAULT 0 NOT NULL,
    CreateUser varchar2(10) NOT NULL,
    CreateDatetime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdateUser varchar2(10),
    UpdateDatetime TIMESTAMP
);
CREATE INDEX MEGA_ECM.TF_CASES_COMMENTS_CaseStatusCode_ValidFlag_INDEX ON MEGA_ECM.TF_CASE_STATUS (CaseStatusCode ASC, ValidFlag DESC);
commit;