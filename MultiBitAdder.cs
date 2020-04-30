using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitAdder : Gate
    {
        public int Size { get; private set; }
        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        public Wire Overflow { get; private set; }

        private FullAdder[] arryOfFulladder;

        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(Size);
            Input2 = new WireSet(Size);
            Output = new WireSet(Size);
            //your code here
            arryOfFulladder = new FullAdder[Size];
            Overflow = new Wire();

            for (int i = 0; i < Size; i++)
            {
                arryOfFulladder[i] = new FullAdder();

            }


            for (int i = 0; i < Size; i++)
            {
                arryOfFulladder[i].ConnectInput1(Input1[i]);
                arryOfFulladder[i].ConnectInput2(Input2[i]);

                if (i == 0)
                {
                    arryOfFulladder[0].CarryInput.Value = 0;
                }
                else
                {
                    arryOfFulladder[i].CarryInput.ConnectInput(arryOfFulladder[i - 1].CarryOutput);
                }

                Output[i].ConnectInput(arryOfFulladder[i].Output);
            }
            Overflow.ConnectInput(arryOfFulladder[Size - 1].CarryOutput);


        }

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            for(int i=0; i < Input1.Size; i++)
            {
                for (int j = 0; j < Input2.Size; j++)
                {
                    Input1.SetValue(i);
                    Input2.SetValue(j);
                    int ans = Output.GetValue();
                    if (ans < (i + j) || ans > (i + j))
                        return false;
                }
            }


            return true;
        

       

           
        }
    }
}
