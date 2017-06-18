using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCalcNetCoreAPI.Models
{
    ///<summary>
    ///Содержит информацию о состоянии модели.
    ///</summary> 
    public class DataModel
    {
        public string Result { get; set; }
        public bool IsResultOk { get; set; }

        public override string ToString()
        {
            return $"Result:{Result}, IsResultOk:{IsResultOk}";
        }
    }
}
