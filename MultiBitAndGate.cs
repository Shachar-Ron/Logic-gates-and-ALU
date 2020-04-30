using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitAndGate : MultiBitGate
    {
        private AndGate[] m_AndArray;
        private int size;

        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            size = iInputCount;
            m_AndArray = new AndGate[size];
            m_AndArray[0] = new AndGate();
            m_AndArray[0].ConnectInput1(m_wsInput[0]);
            m_AndArray[0].ConnectInput2(m_wsInput[1]);



            int i = 1;
            while(i < iInputCount - 1)
            {
                m_AndArray[i] = new AndGate();
                m_AndArray[i].ConnectInput1(m_AndArray[i - 1].Output);
                m_AndArray[i].ConnectInput2(m_wsInput[i + 1]);
                i++;
            }

            Output.ConnectInput(m_AndArray[i - 1].Output);

        }



        public override bool TestGate()
        {

        for(int i = 0; i < size; i++)
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
