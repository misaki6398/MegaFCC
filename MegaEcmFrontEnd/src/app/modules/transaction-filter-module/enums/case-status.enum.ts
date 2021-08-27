export enum CaseStatus {
  // 不要亂動順序，會爆掉。 對應到 DB TF_Case_Stauts
  NewCase = 0,
  Assigned = 1,
  ReleaseRecommand = 2,
  BlockRecommand = 3,
  Release = 4,
  Block = 5,
  Reject = 6,
  QC = 7
}
