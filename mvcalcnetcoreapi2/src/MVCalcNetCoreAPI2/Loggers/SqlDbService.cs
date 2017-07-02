using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCalcNetCoreAPI2.Interfaces;
using MVCalcNetCoreAPI2.Models;
using System.Data.SqlClient;
using System.Data;

namespace MVCalcNetCoreAPI2.Loggers
{
    public class SqlDbService : ILogDbAccess
    {
        ///<summary>
        ///Записывает операнды, результат вычислений и время операции в журнал событий. 
        ///Возвращает номер (ID) записи или 0 в случае неудачной попытки записи.
        ///</summary>
        public int Add(DataModel model, string op1, string op, string op2)
        {
            var resultString = $"{op1}\t{op}\t{op2}\t{model.Result}";
            try
            {
                using (var sp = new SqlDbConnector.GetDBStoredProcedure("[Add]"))
                {
                    sp.connection.Open();
                    sp.command.Parameters.AddWithValue("@pResult", resultString);
                    sp.command.Parameters.AddWithValue("@pDate", DateTimeOffset.Now);
                    var p = new SqlParameter();
                    p.ParameterName = "@pID";
                    p.SqlDbType = SqlDbType.Int;
                    p.Direction = ParameterDirection.Output;
                    sp.command.Parameters.Add(p);
                    sp.command.ExecuteNonQuery();
                    return (int)((sp.command.Parameters["@pID"].Value) ?? 0);
                }
            }
            catch { return 0; }
        }

        ///<summary>
        ///Метод записи в БД через логгер. 
        ///Возвращает номер (ID) записи или 0 в случае неудачной попытки записи.
        ///</summary>
        public int Add(string mesage)
        {
            try
            {
                using (var sp = new SqlDbConnector.GetDBStoredProcedure("[Add]"))
                {
                    sp.connection.Open();
                    sp.command.Parameters.AddWithValue("@pResult", mesage);
                    sp.command.Parameters.AddWithValue("@pDate", DateTimeOffset.Now);
                    var p = new SqlParameter();
                    p.ParameterName = "@pID";
                    p.SqlDbType = SqlDbType.Int;
                    p.Direction = ParameterDirection.Output;
                    sp.command.Parameters.Add(p);
                    sp.command.ExecuteNonQuery();
                    return (int)((sp.command.Parameters["@pID"].Value) ?? 0);
                }
            }
            catch { return 0; }
        }

        ///<summary>
        ///Удаляет запись из журнала событий по номеру записи. Возвращает количество успешно удаленных записей.
        ///</summary>
        public int Delete(int id)
        {
            try
            {
                using (var sp = new SqlDbConnector.GetDBStoredProcedure("[Delete]"))
                {
                    sp.connection.Open();
                    sp.command.Parameters.AddWithValue("@pID", id);
                    return sp.command.ExecuteNonQuery();
                }
            }
            catch
            {
                return 0;
            }
        }

        ///<summary>
        ///Считывает весь лог из базы данных. 
        ///</summary>
        public List<LogModel> List()
        {
            List<LogModel> rowsFromDb = new List<LogModel>();
            using (var sp = new SqlDbConnector.GetDBStoredProcedure("[List]"))
            {
                sp.connection.Open();
                SqlDataReader dr = sp.command.ExecuteReader();
                while (dr.Read())
                {
                    rowsFromDb.Add(new LogModel((int)dr[0], dr[1].ToString(), (DateTimeOffset)dr[2]));
                }
            }
            return rowsFromDb;
        }

        ///<summary>
        ///Считывает одну запись из лога по ID. 
        ///</summary>
        public LogModel Get(int id)
        {
            using (var sp = new SqlDbConnector.GetDBStoredProcedure("Get"))
            {
                sp.connection.Open();
                sp.command.Parameters.AddWithValue("@pID", id);
                LogModel row = new LogModel(0, null, DateTimeOffset.Now);
                SqlDataReader dr = sp.command.ExecuteReader();
                while (dr.Read())
                {
                    row.ID = (int)dr[0];
                    row.ResultLog = dr[1].ToString();
                    row.DateTimeLog = (DateTimeOffset)dr[2];
                }
                return row;
            }
        }

    }
}
