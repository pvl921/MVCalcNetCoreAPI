using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCalcNetCoreAPI2.Data.Evaluation
{
    public class DivideData : BaseEvaluationData
    {

        public override void ValidateAndThrow()
        {
            base.ValidateAndThrow();

            if (Operand2 == 0)
                throw new ArgumentOutOfRangeException(nameof(Operand2));
        }
    }
}
