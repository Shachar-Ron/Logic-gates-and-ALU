using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; private set; }

        //your code here
        private AndGate And;
        private XorGate Xor;


        public HalfAdder()
        {
            //your code here
            And = new AndGate();
            Xor = new XorGate();

            And.ConnectInput1(Input1);
            And.ConnectInput2(Input2);
            Xor.ConnectInput1(And.Input1);
            Xor.ConnectInput2(And.Input2);

            CarryOutput = And.Output;
            Output = Xor.Output;

        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;

            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;
            return true;
        }
    }
}
