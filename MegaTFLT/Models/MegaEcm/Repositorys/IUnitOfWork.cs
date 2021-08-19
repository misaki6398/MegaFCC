using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  MegaTFLT.Models.MegaEcm.Repositorys
{
    public interface IUnitOfWork : IDisposable
    {
        ScreenConfigRepository ScreenConfigRepository { get; }
        TfMessagesRepository TfMessagesRepository { get; }
        TfCasesRepository TfCasesRepository { get; }
        TfAlertsRepository TfAlertsRepository { get; }
        TfAlertListDetailRepository TfAlertListDetailRepository { get; }
        void Commit();
    }
}