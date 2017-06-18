using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCalcNetCoreAPI.Constants;
using MVCalcNetCoreAPI.Models;


namespace MVCalcNetCoreAPI.Controllers
{
    public class EvaluationController
    {
        ///<summary>
        ///Вычисляет сумму двух операндов. 
        ///</summary>
        public static DataModel Sum(string op1, string op2)
        {
            string result;
            double resultDouble;
            bool isResultOk = false;
            DataModel model = new DataModel();
            try
            {
                resultDouble = (double.Parse(op1) + double.Parse(op2));
                result = double.IsNaN(resultDouble) ? Messages.MSG_UNDEFINED_RESULT : resultDouble.ToString();
                isResultOk = !double.IsNaN(resultDouble);
            }
            catch (FormatException)
            { result = Messages.MSG_WRONG_OPERAND_FORMAT; }
            catch (OverflowException)
            { result = Messages.MSG_OVERFLOW_OPERAND; }
            model.Result = result;
            model.IsResultOk = isResultOk;
            return model;
        }

        ///<summary>
        ///Вычисляет разность двух операндов. 
        ///</summary>
        public static DataModel Subtract(string op1, string op2)
        {
            string result;
            double resultDouble;
            bool isResultOk = false;
            DataModel model = new DataModel();
            try
            {
                resultDouble = (double.Parse(op1) - double.Parse(op2));
                result = double.IsNaN(resultDouble) ? Messages.MSG_UNDEFINED_RESULT : resultDouble.ToString();
                isResultOk = !double.IsNaN(resultDouble);
            }
            catch (FormatException)
            { result = Messages.MSG_WRONG_OPERAND_FORMAT; }
            catch (OverflowException)
            { result = Messages.MSG_OVERFLOW_OPERAND; }
            model.Result = result;
            model.IsResultOk = isResultOk;
            return model;
        }

        ///<summary>
        ///Вычисляет произведение двух операндов. 
        ///</summary>
        public static DataModel Multiply(string op1, string op2)
        {
            string result;
            double resultDouble;
            bool isResultOk = false;
            DataModel model = new DataModel();
            try
            {
                resultDouble = (double.Parse(op1) * double.Parse(op2));
                result = double.IsNaN(resultDouble) ? Messages.MSG_UNDEFINED_RESULT : resultDouble.ToString();
                isResultOk = !double.IsNaN(resultDouble);
            }
            catch (FormatException)
            { result = Messages.MSG_WRONG_OPERAND_FORMAT; }
            catch (OverflowException)
            { result = Messages.MSG_OVERFLOW_OPERAND; }
            model.Result = result;
            model.IsResultOk = isResultOk;
            return model;
        }

        ///<summary>
        ///Вычисляет частное двух операндов. 
        ///</summary>
        public static DataModel Divide(string op1, string op2)
        {
            string result;
            double resultDouble;
            bool isResultOk = false;
            DataModel model = new DataModel();
            try
            {
                resultDouble = (double.Parse(op1) / double.Parse(op2));
                result = double.IsNaN(resultDouble) ? Messages.MSG_UNDEFINED_RESULT : resultDouble.ToString();
                isResultOk = !double.IsNaN(resultDouble);
            }
            catch (FormatException)
            { result = Messages.MSG_WRONG_OPERAND_FORMAT; }
            catch (OverflowException)
            { result = Messages.MSG_OVERFLOW_OPERAND; }
            model.Result = result;
            model.IsResultOk = isResultOk;
            return model;
        }

        ///<summary>
        ///Вычисляет возведение в степень первого операнда. Второй операнд определяет показатель степени.
        ///</summary>
        public static DataModel Power(string op1, string op2)
        {
            string result;
            double resultDouble;
            bool isResultOk = false;
            DataModel model = new DataModel();
            try
            {
                resultDouble = Math.Pow(double.Parse(op1), double.Parse(op2));
                result = double.IsNaN(resultDouble) ? Messages.MSG_UNDEFINED_RESULT : resultDouble.ToString();
                isResultOk = !double.IsNaN(resultDouble);
            }
            catch (FormatException)
            { result = Messages.MSG_WRONG_OPERAND_FORMAT; }
            catch (OverflowException)
            { result = Messages.MSG_OVERFLOW_OPERAND; }
            model.Result = result;
            model.IsResultOk = isResultOk;
            return model;
        }

        ///<summary>
        ///Определяет результат при неизвестном символе оператора.
        ///</summary>
        public static DataModel Undefined(string op)
        {
            // присваиваем значения сразу при инициализации объекта
            DataModel model = new DataModel
            {
                Result = $"{Messages.MSG_WRONG_OPERATOR} ({op}).\n",
                IsResultOk = false
            };
            return model;
        }
    }
}
