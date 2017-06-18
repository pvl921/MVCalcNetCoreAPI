using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCalcNetCoreAPI.Constants
{
    public class Messages
    {
        public const string MSG_UNDEFINED_RESULT = "Результат операции неопределен.";
        public const string MSG_WRONG_OPERAND_FORMAT = "Неверный формат операнда.";
        public const string MSG_OVERFLOW_OPERAND = "Значение операнда выходит за допустимые пределы.";
        public const string MSG_WRONG_OPERATOR = "Неизвестный символ оператора";
        public const string MSG_DB_FAILURE = "Ошибка доступа к базе данных.";
    }
}
