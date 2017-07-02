using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MVCalcNetCoreAPI2.Data.Evaluation
{
    public abstract class BaseEvaluationData
    {
        public string Op1 { get; set; }
        public string Op2 { get; set; }

        public double Operand1 { get; private set; }
        public double Operand2 { get; private set; }

        public virtual void ValidateAndThrow()
        {
            if (String.IsNullOrWhiteSpace(Op1))
                throw new ArgumentNullException(nameof(Op1));

            if (String.IsNullOrWhiteSpace(Op2))
                throw new ArgumentNullException(nameof(Op2));
           
            Operand1 = double.Parse(Op1);
            Operand2 = double.Parse(Op2);
        }
    }
}
