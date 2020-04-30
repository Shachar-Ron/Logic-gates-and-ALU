using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        private NotGate[] m_NotArray;

        public BitwiseNotGate(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            m_NotArray = new NotGate[iSize];

            for (int i = 0; i < iSize; i++)
                m_NotArray[i] = new NotGate();
            for (int j = 0; j < iSize; j++)
            {
                m_NotArray[j].ConnectInput(Input[j]);
                Output[j].ConnectInput(m_NotArray[j].Output);
            }
        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }


        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int i = 0; i < m_NotArray.Length; i++)
            {

                Input[i].Value = 0;
                if (Output[i].Value != 1)
                    return false;
                Input[i].Value = 1;
                if (Output[i].Value != 0)
                    return false;


            }

            return true;
        }
    }
}

