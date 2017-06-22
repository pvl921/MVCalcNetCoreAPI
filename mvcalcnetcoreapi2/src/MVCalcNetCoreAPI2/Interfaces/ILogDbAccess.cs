using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCalcNetCoreAPI2.Models;

namespace MVCalcNetCoreAPI2.Interfaces
{
    ///<summary>
    ///Интерфейс для доступа к базе данных. Используется в контейнере ConfigureServices. 
    ///</summary>
    public interface ILogDbAccess
    {
        int Add(DataModel model, string op1, string op, string op2);
        int Delete(int id);
        List<LogModel> List();
        LogModel Get(int id);
    }
}
