using System;
using System.Data;
using System.Data.SqlClient;
using MegaTFLT.Utilitys;
using Oracle.ManagedDataAccess.Client;

namespace MegaEcmBackEnd.Models.FccmAtomic.Repositorys
{
    public class FccmAtomicUnitOfWork : IUnitOfWork
    {

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private FsiRtListDataRepositroy _fsiRtListDataRepositroy;
        
        private bool _disposed;

        public FccmAtomicUnitOfWork()
        {
            _connection = new OracleConnection(ConfigUtility.FccmAtomicConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        
        public FsiRtListDataRepositroy FsiRtListDataRepositroy
        {
            get { return _fsiRtListDataRepositroy ?? (_fsiRtListDataRepositroy = new FsiRtListDataRepositroy(_transaction)); }
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
            _fsiRtListDataRepositroy = null;
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

        ~FccmAtomicUnitOfWork()
        {
            dispose(false);
        }
    }
}