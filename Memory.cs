using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Memory : SequentialGate
    {
        public int AddressSize { get; private set; }
        public int WordSize { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public WireSet Address { get; private set; }
        public Wire Load { get; private set; }

        private WireSet load;
        private MultiBitRegister[] nBR;
        private BitwiseMultiwayDemux demux;
        private BitwiseMultiwayMux mux;
     
        //your code here

        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();

            //your code here
            load = new WireSet(iWordSize);
            load[0].ConnectInput(Load);

            demux = new BitwiseMultiwayDemux(iWordSize, iAddressSize);
            demux.ConnectControl(Address);
            demux.ConnectInput(load);

            
            int numOfReg= (int)Math.Pow(2, iAddressSize);
            nBR = new MultiBitRegister[numOfReg];
            for (int i = 0; i < nBR.Length; i++)
            {
                nBR[i] = new MultiBitRegister(iWordSize);

            }
            mux = new BitwiseMultiwayMux(iWordSize, iAddressSize);
            mux.ConnectControl(Address);

            ////////////////////////////////////////////////////////////////////

            for (int i = 0; i < nBR.Length; i++)
            {
                nBR[i].ConnectInput(Input);
                nBR[i].Load.ConnectInput(demux.Outputs[i][0]);
                mux.Inputs[i].ConnectInput(nBR[i].Output);

            }
            Output.ConnectInput(mux.Output);

        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {

            /*
            Input.SetValue(0);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue())
                return false;


            Input.SetValue(1);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue())
                return false;





            Input.SetValue(8);
            Address.SetValue(3);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.GetValue());
            if (Output.GetValue() != 8)
                return false;

            Input.SetValue(12);
            Address.SetValue(3);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.GetValue());
            if (Output.GetValue() != 12)
                return false;

            Input.SetValue(9);
            Address.SetValue(3);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.GetValue());
            if (Output.GetValue() != 12)
                return false;
*/
            Input.SetValue(0);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue())
                return false;


            Input.SetValue(1);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != Input.GetValue())
                return false;

            Input.Set2sComplement(-1);
            Address.SetValue(4);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.Get2sComplement());
            if (Output.Get2sComplement() != -1)
                return false;

            Input.Set2sComplement(-9);
            Address.SetValue(4);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.Get2sComplement());
            if (Output.Get2sComplement() != -1)
                return false;

            Input.SetValue(1);
            Address.SetValue(4);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.GetValue());
            if (Output.GetValue() != 1)
                return false;

            Input.SetValue(8);
            Address.SetValue(4);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.GetValue());
            if (Output.GetValue() != 8)
                return false;

            Input.SetValue(4);
            Address.SetValue(4);
            Load.Value = 0;
            Clock.ClockDown();
            Clock.ClockUp();
            Console.WriteLine(Output.GetValue());
            if (Output.GetValue() != 8)
                return false;

   
            return true;
        }
    }
}
