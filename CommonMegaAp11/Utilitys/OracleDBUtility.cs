using System;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace CommonMegaAp11.Utilitys
{
    public static class OracleDBUtility
    {
        public static OracleClob ConvertToClob(string mystring, OracleConnection con)
        {
            byte[] newvalue = System.Text.Encoding.Unicode.GetBytes(mystring);
            var clob = new OracleClob(con);
            clob.Write(newvalue, 0, newvalue.Length);

            return clob;
        }       
    }
}