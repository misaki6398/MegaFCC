using System;
using System.Data;
using System.Data.SqlClient;
using MegaTFLT.Utilitys;
using Oracle.ManagedDataAccess.Client;

namespace MegaTFLT.Models.MegaEcm.Repositorys
{
    public class MegaEcmUnitOfWork : IUnitOfWork
    {

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private ScreenConfigRepository _screenConfigRepository;
        private TfMessagesRepository _tfMessagesRepository;
        private TfCasesRepository _tfCasesRepository;
        private TfAlertsRepository _tfAlertsRepository;
        private TfAlertListDetailRepository _tfAlertListDetailRepository;
        private bool _disposed = false;

        public MegaEcmUnitOfWork()
        {
            _connection = new OracleConnection(ConfigUtility.MegaEcmConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ScreenConfigRepository ScreenConfigRepository
        {
            get { return _screenConfigRepository ?? (_screenConfigRepository = new ScreenConfigRepository(_transaction)); }
        }
        public TfMessagesRepository TfMessagesRepository
        {
            get { return _tfMessagesRepository ?? (_tfMessagesRepository = new TfMessagesRepository(_transaction)); }
        }
        public TfCasesRepository TfCasesRepository
        {
            get { return _tfCasesRepository ?? (_tfCasesRepository = new TfCasesRepository(_transaction)); }
        }
        public TfAlertsRepository TfAlertsRepository
        {
            get { return _tfAlertsRepository ?? (_tfAlertsRepository = new TfAlertsRepository(_transaction)); }
        }
        public TfAlertListDetailRepository TfAlertListDetailRepository
        {
            get { return _tfAlertListDetailRepository ?? (_tfAlertListDetailRepository = new TfAlertListDetailRepository(_transaction)); }
        }
        
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {                
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _screenConfigRepository = null;
            _tfMessagesRepository = null;
            _tfAlertsRepository = null;
            _tfAlertListDetailRepository = null;
            _tfCasesRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~MegaEcmUnitOfWork()
        {
            dispose(false);
        }
    }
}