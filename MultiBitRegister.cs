using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public Wire Load { get; private set; }
        public int Size { get; private set; }

        private SingleBitRegister[] SBreg;

        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            //your code here
            SBreg = new SingleBitRegister[Size];

            for (int i = 0; i < SBreg.Length; i++)
            {
                SBreg[i] = new SingleBitRegister();
            }

            for (int i = 0; i < SBreg.Length; i++)
            {
                SBreg[i].ConnectLoad(Load);
                SBreg[i].ConnectInput(Input[i]);
                
                Output[i].ConnectInput(SBreg[i].Output);
            }

       

        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        
        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
            Input.SetValue(8);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 8)
              return false;
            
            Input.SetValue(3);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 3)
                return false;
            
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.SetValue(4);
            if (Output.GetValue() != 3)
                return false;


            return true;
            
                
            
                
            

        
        
        
        }
    }
}
