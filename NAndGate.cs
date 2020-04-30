using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class NAndGate : TwoInputGate
    {

        private NotGate m_gNotOutput;
        private AndGate m_gAnd;

        public NAndGate()
        {
          

            m_gAnd = new AndGate();
            m_gNotOutput = new NotGate();
            Input1 = new Wire();
            Input2 = new Wire();
            Output = new Wire();

            m_gAnd.ConnectInput1(Input1);
            m_gAnd.ConnectInput2(Input2);
            m_gNotOutput.ConnectInput(m_gAnd.Output);
            Output = m_gNotOutput.Output;


        }

        public override string ToString()
        {
            return "NAnd " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        public override bool TestGate()
        {

            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }


    }
}
