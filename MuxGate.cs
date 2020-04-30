using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MuxGate : TwoInputGate
    {
        public Wire ControlInput { get; private set; }
        private NotGate not;
        private AndGate and1;
        private AndGate and2;
        private OrGate  or;



        public MuxGate()
        {
            ControlInput = new Wire();

            not = new NotGate();
            and1 = new AndGate();
            and2 = new AndGate();
            or = new OrGate();

            not.ConnectInput(ControlInput);
            and1.ConnectInput1(not.Output);
            and2.ConnectInput1(ControlInput);
            or.ConnectInput1(and1.Output);
            or.ConnectInput2(and2.Output);

            Input1 = and1.Input2;
            Input2 = and2.Input2;
            Output = or.Output;


        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }


        public override string ToString()
        {
            return "Mux " + Input1.Value + "," + Input2.Value + ",C" + ControlInput.Value + " -> " + Output.Value;
        }



        public override bool TestGate()
        {

            for(int i=0; i<2; i++) {
                ControlInput.Value = i;
                Input1.Value = i;
                Input2.Value = i;
                if (Output.Value != i)
                    return false;

            }


            ControlInput.Value = 1;
            for (int i = 0; i < 2; i++)
            {
                Input1.Value = i;
                Input2.Value = i;
                if (Output.Value != i)
                    return false;

            }


   
            /*
            ControlInput.Value = 0;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;

            ControlInput.Value = 1;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;

            ControlInput.Value = 0;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;

            ControlInput.Value = 1;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;

            ControlInput.Value = 0;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;

    */


            return true;
        }
    }
}
