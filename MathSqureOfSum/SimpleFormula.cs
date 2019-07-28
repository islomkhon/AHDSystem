using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;

namespace MathSqureOfSum
{
    public class SimpleFormula : CodeActivity
    {
        //oy2equac3vmakb6yjt7q2h2jotweq7tebl4pu6qkdbtdre

        [Category("Input")]
        [RequiredArgument]
        public InArgument<double> FirstNumber { get; set; }

        [Category("Input")]
        public InArgument<double> SecondNumber { get; set; }

        [Category("Output")]
        public OutArgument<double> ResultNumber { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var firstNumber = FirstNumber.Get(context);
            var secondNumber = SecondNumber.Get(context);
            var result = System.Math.Pow(firstNumber + secondNumber, 2);
            ResultNumber.Set(context, result);
        }
    }
}
