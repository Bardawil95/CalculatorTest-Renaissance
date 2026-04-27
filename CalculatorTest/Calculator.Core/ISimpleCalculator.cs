using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Core
{
    public interface ISimpleCalculator
    {
        int Add(int start, int amount);
        int Subtract(int start, int amount);
    }
}
