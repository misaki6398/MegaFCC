namespace MegaEcmBackEnd.Enums
{
    public enum CaseStatus
    {
        // 不要亂動順序，會爆掉
        NewCase,
        ReleaseRecommand,
        BlockRecommand,
        Release,
        Block,
        Reject,
        QC
    }
}