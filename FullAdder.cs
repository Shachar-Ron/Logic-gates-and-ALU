using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }

        //your code here
        private OrGate Or;
        private HalfAdder Half1;
        private HalfAdder Half2;


        public FullAdder()
        {
            CarryInput = new Wire();
            //your code here
            Or = new OrGate();
            Half1 = new HalfAdder();
            Half2 = new HalfAdder();


            Half1.ConnectInput1(Input1);
            Half1.ConnectInput2(Input2);

            Half2.ConnectInput2(CarryInput);
            Half2.ConnectInput1(Half1.Output);

            Or.ConnectInput1(Half1.CarryOutput);
            Or.ConnectInput2(Half2.CarryOutput);

            CarryOutput = Or.Output;
            Output = Half2.Output;


        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if (Output.Value != 0 || CarryOutput.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;

            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;

            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 1;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;

            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 1;
            if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;

            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (Output.Value != 1 || CarryOutput.Value != 1)
                return false;
            return true;
        }
    }
}
