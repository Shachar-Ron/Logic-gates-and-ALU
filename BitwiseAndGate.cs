using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        private AndGate[] m_AndArray;

        public BitwiseAndGate(int iSize)
            : base(iSize)
        {
            m_AndArray = new AndGate[iSize];

            for (int i = 0; i < iSize; i++)
                m_AndArray[i] = new AndGate();
            for (int j = 0; j < iSize; j++)
            {
                m_AndArray[j].ConnectInput1(Input1[j]);
                m_AndArray[j].ConnectInput2(Input2[j]);
                Output[j].ConnectInput(m_AndArray[j].Output);
            }
        }


        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int i = 0; i < m_AndArray.Length; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                if (Output[i].Value != 0)
                    return false;
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                if (Output[i].Value != 0)
                    return false;
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                if (Output[i].Value != 0)
                    return false;
                Input1[i].Value = 1;
                Input2[i].Value = 1;
                if (Output[i].Value != 1)
                    return false;
            }

            return true;
        }
    }
}
