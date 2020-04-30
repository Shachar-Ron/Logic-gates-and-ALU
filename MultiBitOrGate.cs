using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitOrGate : MultiBitGate
    {
        private OrGate[] m_OrArray;
        private int size;

        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            size = iInputCount;
            m_OrArray = new OrGate[size];
            m_OrArray[0] = new OrGate();
            m_OrArray[0].ConnectInput1(m_wsInput[0]);
            m_OrArray[0].ConnectInput2(m_wsInput[1]);


            int i = 1;
            while (i < iInputCount - 1)
            {
                m_OrArray[i] = new OrGate();
                m_OrArray[i].ConnectInput1(m_OrArray[i - 1].Output);
                m_OrArray[i].ConnectInput2(m_wsInput[i + 1]);
                i++;
            }

            Output.ConnectInput(m_OrArray[i - 1].Output);

        }



        public override bool TestGate()
        {

            for (int i = 0; i < size; i++)
            {
                m_wsInput[i].Value = 1;
            }
            if (Output.Value != 1)
                return false;

            for (int i = 0; i < size; i++)
            {
                m_wsInput[i].Value = 0;
            }
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
