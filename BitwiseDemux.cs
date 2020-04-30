using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }


        public Demux[] demuxs;
        

        public BitwiseDemux(int iSize)
        {

            Control = new Wire();
            Input = new WireSet(iSize);
            Output1 = new WireSet(iSize);
            Output2= new WireSet(iSize);
            demuxs = new Demux[iSize];


            for (int i = 0; i< iSize; i++)
            {
                demuxs[i] = new Demux();
                demuxs[i].ConnectControl(Control);
                demuxs[i].ConnectInput(Input[i]);
                Output1[i].ConnectInput(demuxs[i].Output1);
                Output2[i].ConnectInput(demuxs[i].Output2);
             }

}

    public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        public override bool TestGate()
        {
            Input[0].Value = 1;
            Input[1].Value = 1;
            Input[2].Value = 1;

            Control.Value = 0;
            if ((Output1[0].Value != 0) || (Output1[1].Value != 1) || (Output1[2].Value != 1) || (Output2[0].Value != 0) || (Output2[1].Value != 0) || (Output2[2].Value != 0))
                return false;
            return true;
        }
    }
}
