using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCalcNetCoreAPI2.Models;
using Microsoft.Extensions.Logging;
using MVCalcNetCoreAPI2.Data.Evaluation;

namespace MVCalcNetCoreAPI2.Controllers
{
    [Route("[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly ILogger<EvaluationController> _logger;

        public EvaluationController(ILogger<EvaluationController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<summary>
        ///Операция арифметического деления двух операндов.
        ///</summary>
        [HttpPost("divide")]
        public IActionResult Divide([FromBody] DivideData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.ValidateAndThrow();

            var result = data.Operand1 / data.Operand2;

            _logger.LogInformation($"{data.Operand1} / {data.Operand2} = {result}");

            return Ok(new DataModel() { Result = result });
        }

        ///<summary>
        ///Операция арифметического сложения двух операндов.
        ///</summary>
        [HttpPost("sum")]
        public IActionResult Sum([FromBody] GeneralEvaluationData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.ValidateAndThrow();

            var result = data.Operand1 + data.Operand2;

            _logger.LogInformation($"{data.Operand1} + {data.Operand2} = {result}");

            return Ok(new DataModel() { Result = result });
        }


        ///<summary>
        ///Операция арифметического вычитания двух операндов.
        ///</summary>
        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] GeneralEvaluationData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.ValidateAndThrow();

            var result = data.Operand1 - data.Operand2;

            _logger.LogInformation($"{data.Operand1} - {data.Operand2} = {result}");

            return Ok(new DataModel() { Result = result });
        }

        ///<summary>
        ///Операция арифметического умножения двух операндов.
        ///</summary>
        [HttpPost("multiply")]
        public IActionResult Multiply([FromBody] GeneralEvaluationData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.ValidateAndThrow();

            var result = data.Operand1 * data.Operand2;

            _logger.LogInformation($"{data.Operand1} * {data.Operand2} = {result}");

            return Ok(new DataModel() { Result = result });
        }

        ///<summary>
        ///Операция возведения в степень.
        ///</summary>
        [HttpPost("power")]
        public IActionResult Power([FromBody] PowerData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            data.ValidateAndThrow();

            var result = Math.Pow(data.Operand1, data.Operand2);

            _logger.LogInformation($"{data.Operand1} ^ {data.Operand2} = {result}");

            return Ok(new DataModel() { Result = result });
        }

    }
}
