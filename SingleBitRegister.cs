using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class SingleBitRegister : Gate
    {
 
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        public Wire Load { get; private set; }

        private MuxGate mux;
        private DFlipFlopGate DFF;

        public SingleBitRegister()
        {

            Input = new Wire();
            Load = new Wire();
            Output = new Wire();
            //your code here 
            mux = new MuxGate();
            DFF = new DFlipFlopGate();
           
            mux.ConnectInput2(Input);
            mux.ConnectControl(Load);

            DFF.ConnectInput(mux.Output);
            mux.ConnectInput1(DFF.Output);
            Output.ConnectInput(DFF.Output);
          

        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

      

        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {

            Input.Value = 1;
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 0;
            if (Output.Value != 0)
                return false;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 1;
            if (Output.Value != 1)
                return false;
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Value != 0)
                return false;
            ///////////////////////////////////////////////
            Input.Value = 1;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 0;
            if (Output.Value != 1)
                return false;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 1;
            if (Output.Value != 0)
                return false;
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Value != 0)
                return false;
            
            return true;
        
        }
    }
}
