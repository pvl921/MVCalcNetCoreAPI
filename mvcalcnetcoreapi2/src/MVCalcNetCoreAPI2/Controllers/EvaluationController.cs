using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MVCalcNetCoreAPI2.Models;
using MVCalcNetCoreAPI2.Services;

namespace MVCalcNetCoreAPI2.Controllers
{
    public class EvaluationController : ControllerBase
    {
        ///<summary>
        ///Вычисляет результат математической операции. HTTP-запрос типа GET с параметром "/sum" , "/subtract", "/multiply", "/divide", "/power" и двумя операндами.
        ///При отсутствии в запросе одного или двух операндов будет возвращена ошибка 404.
        ///</summary>
        [HttpGet("{operation}/{op1}/{op2}")]
        public IActionResult Calculate(string op1, string op2)
        {
            string op = RouteData.Values["operation"].ToString().ToLowerInvariant();
            string result;
            double resultDouble;
            bool isResultOk = false;
            DataModel model = new DataModel();
            SqlDbService logCtrl = new SqlDbService();
            try
            {
                switch (op)
                {
                    case "sum":
                        resultDouble = (double.Parse(op1) + double.Parse(op2));
                        break;
                    case "subtract":
                        resultDouble = (double.Parse(op1) - double.Parse(op2));
                        break;
                    case "multiply":
                        resultDouble = (double.Parse(op1) * double.Parse(op2));
                        break;
                    case "divide":
                        resultDouble = (double.Parse(op1) / double.Parse(op2));
                        break;
                    case "power":
                        resultDouble = Math.Pow(double.Parse(op1), double.Parse(op2));
                        break;
                    default:
                        resultDouble = Double.NaN;
                        break;
                }
                result = double.IsNaN(resultDouble) ? "Результат операции неопределен." : resultDouble.ToString();
                isResultOk = !double.IsNaN(resultDouble);
            }
            catch (FormatException)
            { result = "Неверный формат операнда."; }
            catch (OverflowException)
            { result = "Значение операнда выходит за допустимые пределы."; }
            model.Result = result;
            model.IsResultOk = isResultOk;
            var output = new ObjectResult(model.ToString());
            bool isDbAccessSuccess = (logCtrl.Add(model, op1, op, op2) == 0) ? false : true;
            output.StatusCode = (model.IsResultOk && isDbAccessSuccess) ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
            return output;
        }

        ///<summary>
        ///Считывает весь лог из базы данных. 
        ///</summary>
        [HttpGet("list")]
        public IActionResult List()
        {
            var logCtrl = new SqlDbService();
           
            List<LogModel> logList = logCtrl.List();
            bool isDbAccessSuccess = (logList.Count > 0) ? true : false;
            string logToDisplay = "{" + string.Join("}\n{", logList) + "}";
            var output = new ObjectResult(logToDisplay);
            output.StatusCode = (isDbAccessSuccess) ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
            return output;
        }

        ///<summary>
        ///Считывает одну запись из лога по ID. 
        ///</summary>
        [HttpGet("get/{id:int}")]
        public IActionResult GetById(int id)
        {
            var logCtrl = new SqlDbService();
            LogModel logRow = logCtrl.Get(id);
            bool isDbAccessSuccess = (logRow.ID > 0) ? true : false;
            string logToDisplay = logRow.ToString();
            var output = new ObjectResult(logToDisplay);
            output.StatusCode = (isDbAccessSuccess) ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
            return output;
        }

        ///<summary>
        ///Удаляет запись из журнала событий по номеру записи. Возвращает количество успешно удаленных записей.
        ///</summary>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var logCtrl = new SqlDbService();
            bool isDbAccessSuccess = logCtrl.Delete(id) > 0 ? true : false;
            var output = new ObjectResult($"Удаление записи {id}: {isDbAccessSuccess}");
            output.StatusCode = (isDbAccessSuccess) ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
            return output;
        }

    }
}
