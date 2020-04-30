using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseOrGate : BitwiseTwoInputGate
    {
        private OrGate[] m_OrArray;

        public BitwiseOrGate(int iSize)
            : base(iSize)
        {
            m_OrArray = new OrGate[iSize];
            for (int i = 0; i < iSize; i++)
                m_OrArray[i] = new OrGate();
            for (int j = 0; j < iSize; j++)
            {
                m_OrArray[j].ConnectInput1(Input1[j]);
                m_OrArray[j].ConnectInput2(Input2[j]);
                Output[j].ConnectInput(m_OrArray[j].Output);
            }
        }


        public override string ToString()
        {
            return "Or " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int i = 0; i < m_OrArray.Length; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
                if (Output[i].Value != 0)
                    return false;
                Input1[i].Value = 0;
                Input2[i].Value = 1;
                if (Output[i].Value != 1)
                    return false;
                Input1[i].Value = 1;
                Input2[i].Value = 0;
                if (Output[i].Value != 1)
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




