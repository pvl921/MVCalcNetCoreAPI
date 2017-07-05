using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCalcNetCoreAPI2.Data.Evaluation
{
    public class PowerData : BaseEvaluationData
    {
        public override void ValidateAndThrow()
        {
            base.ValidateAndThrow();

            if ((Operand1 < 0) && (!int.TryParse(Op2, out int intOp2)))
                throw new ArgumentOutOfRangeException($"{nameof(Operand1)}, {nameof(Operand2)}", "Вычисления с мнимыми числами не поддерживаются.");
        }
    }
}
