using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MVCalcNetCoreAPI2.Models;
using MVCalcNetCoreAPI2.Services;
using MVCalcNetCoreAPI2.Interfaces;
using Microsoft.Extensions.Logging;
using MVCalcNetCoreAPI2.Data.Evaluation;

namespace MVCalcNetCoreAPI2.Controllers
{
    [Route("[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly ILogDbAccess _logCtrl;

        public EvaluationController(ILogDbAccess logCtrl)
        {
            _logCtrl = logCtrl ?? throw new ArgumentNullException(nameof(logCtrl));
        }

        ///<summary>
        ///Вычисляет результат математической операции. HTTP-запрос типа GET с параметром "/sum" , "/subtract", "/multiply", "/divide", "/power" и двумя операндами.
        ///При отсутствии в запросе одного или двух операндов будет возвращена ошибка 404.
        ///</summary>
        [HttpPost("divide")]
        public IActionResult Divide([FromBody] DivideData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.ValidateAndThrow();

            var result = data.Operand1 / data.Operand2;

            return Ok(new DataModel() { Result = result });
        }

    }
}
