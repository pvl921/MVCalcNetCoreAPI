using Microsoft.AspNetCore.Mvc;
using MVCalcNetCoreAPI2.Interfaces;
using MVCalcNetCoreAPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MVCalcNetCoreAPI2.Controllers
{
    [Route("[controller]")] 
    public class LogController : ControllerBase
    {

        private readonly ILogDbAccess _logCtrl;
        private readonly ILogger<LogController> _logger;

        public LogController(ILogDbAccess logCtrl, ILogger<LogController> logger)
        {
            _logCtrl = logCtrl ?? throw new ArgumentNullException(nameof(logCtrl));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<summary>
        ///Считывает весь лог из базы данных. 
        ///</summary>
        [HttpGet] // GET ..../log
        public IActionResult List()
        {
            List<LogModel> logList = _logCtrl.List();
            bool isDbAccessSuccess = (logList.Count > 0) ? true : false;
            string logToDisplay = "{" + string.Join("}\n{", logList) + "}";

            _logger.LogInformation("Вывод полного лога.");

            return Ok(logToDisplay);
        }


        ///<summary>
        ///Считывает одну запись из лога по ID. 
        ///</summary>
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            LogModel logRow = _logCtrl.Get(id);

            if (logRow.ID == 0) throw new ArgumentOutOfRangeException(nameof(id));

            string logToDisplay = logRow.ToString();

            _logger.LogInformation($"Вывод записи лога №{id}");

            return Ok(logToDisplay);
        }


        ///<summary>
        ///Удаляет запись из журнала событий по номеру записи. Возвращает количество успешно удаленных записей.
        ///</summary>
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            int deleted = _logCtrl.Delete(id);

            if (deleted == 0) throw new ArgumentOutOfRangeException(nameof(id));

            _logger.LogInformation($"Удаление записи лога №{id}");

            return Accepted(id);
        }
    }
}
