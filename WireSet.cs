using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //this class represents a set of wires (a cable)
    class WireSet
    {
        private Wire[] m_aWires;
        public int Size { get; private set; }
        public Boolean InputConected { get; private set; }
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        
        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {

            for (int i = 0; i < m_aWires.Length; i++)
            {
                m_aWires[i].Value = 0;
            }
            int temp = iValue;
            int Wire = 0;
                for (int j = 0; j < m_aWires.Length && temp!=0 ; j++)
                {
                    if (temp != 0)
                    {
                        Wire = temp % 2;
                    }
                    m_aWires[j].Value = Wire;
                    temp = temp / 2;
                    Wire = 0;
                }

        }

        //transform the binary code into a positive integer
        public int GetValue()
        {
            int result = 0;
            int temp = 0;
            for (int i = 0; i < m_aWires.Length; i++)
            {
                temp = (int)(Math.Pow(2, i));
                int temp2 = temp * (m_aWires[i].Value);
                result = result + temp2;
            }
            return result;
        }

        //transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            if (iValue > 0)
                SetValue(iValue);
            else
            {
                 SetValue(-1 * iValue);
                for (int i = 0; i < Size; i++)
                {
                    if (m_aWires[i].Value == 1)
                        m_aWires[i].Value = 0;
                    else if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                }
                //add 1

                int temp = GetValue();
                temp = temp + 1;
                SetValue(temp);
            }
        }

        //transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            int result = 0;
            if (m_aWires[m_aWires.Length - 1].Value == 0)
                return GetValue();
            else
            {
                result = GetValue();
                int temp = (int)(Math.Pow(2, m_aWires.Length));
                int ans = result - temp;
                return ans;
            }

        }

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if(wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;
            
        }

    }
}
