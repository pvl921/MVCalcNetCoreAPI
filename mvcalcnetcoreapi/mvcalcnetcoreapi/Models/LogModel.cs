using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCalcNetCoreAPI.Models
{
    ///<summary>
    ///Содержит структуру хранения данных в базе данных.
    ///</summary> 
    public class LogModel
    {
        public int ID { get; set; }
        public string ResultLog { get; set; }
        public DateTimeOffset DateTimeLog { get; set; }

        public LogModel(int id, string resultLog, DateTimeOffset dateTimeLog)
        {
            ID = id;
            ResultLog = resultLog;
            DateTimeLog = dateTimeLog;
        }

        public override string ToString()
        {
            return $"\"ID\": {ID},\"ResultLog\": \"{ResultLog}\",\"DateTimeLog\": \"{DateTimeLog}\"";
        }
    }
}
