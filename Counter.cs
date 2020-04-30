using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Counter : Gate
    {
        private int m_iValue;
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public Wire Load { get; private set; }
        public int Size { get; private set; }
        //more fields here
        private BitwiseMux bwMux;
        private MultiBitAdder multiBA;
        private MultiBitRegister mbReg;
        private WireSet one;
        private Wire loadOne;

        public Counter(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();


            //your code here
            multiBA = new MultiBitAdder(Size);
            bwMux = new BitwiseMux(Size);
            mbReg = new MultiBitRegister(Size);
            one = new WireSet(Size);
            loadOne = new Wire();

            
            bwMux.ConnectInput1(multiBA.Output);
            bwMux.ConnectInput2(Input);
            bwMux.ConnectControl(Load);

            mbReg.ConnectInput(bwMux.Output);

            one.SetValue(1);
            loadOne.ConnectInput(one[0]);
            mbReg.Load.ConnectInput(loadOne);

            Output.ConnectInput(mbReg.Output);

            multiBA.ConnectInput1(one);
            multiBA.ConnectInput2(Output);

        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }
        public void ConnectLoad(Wire w)
        {
            Load.ConnectInput(w);
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

          
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 9)
                return false;

            Input.SetValue(12);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 12)
                return false;

            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 13)
                return false;
            return true;
            /*
            Input.SetValue(8);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue())
                return false;

            //Input.SetValue(1);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue()+1)
                return false;

            Input.SetValue(0);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue())
                return false;

            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue()+1)
                return false;
            return true;
            */

        }
    }
}
