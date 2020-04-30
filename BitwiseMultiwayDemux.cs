using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseMultiwayDemux : Gate
    {
        public int Size { get; private set; }
        public int ControlBits { get; private set; }
        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }


        private BitwiseDemux[] bwiseDmux;
        int j = 0; int i = 0; int z = 0;


       

        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {

            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];

            int outsize = Outputs.Length;
            bwiseDmux = new BitwiseDemux[outsize - 1];

            for (int i = 0; i < bwiseDmux.Length; i++)
            {
                bwiseDmux[i] = new BitwiseDemux(Size);
            }

            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }

            bwiseDmux[0].ConnectInput(Input);
            bwiseDmux[0].ConnectControl(Control[cControlBits - 1]);

            int cIndex = cControlBits - 2;
            int currMuxes = 2;

            for (int i = 0; i < bwiseDmux.Length / 2 - 1; i++)
            {
                if (i == (currMuxes - 1))
                {
                    cIndex--;
                    currMuxes = currMuxes * 2;
                }

                bwiseDmux[2 * i + 1].ConnectInput(bwiseDmux[i].Output1);
                bwiseDmux[2 * i + 1].ConnectControl(Control[cIndex]);

                bwiseDmux[2 * (i + 1)].ConnectInput(bwiseDmux[i].Output2);
                bwiseDmux[2 * (i + 1)].ConnectControl(Control[cIndex]);
            }

            int demIndex = bwiseDmux.Length / 2;
            for (int i = 0; i < Outputs.Length; i = i + 2)
            {
                Outputs[i].ConnectInput(bwiseDmux[demIndex].Output1);
                Outputs[i + 1].ConnectInput(bwiseDmux[demIndex].Output2);
                demIndex++;
            }
        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }


        public override bool TestGate()
        {

          
            Input[0].Value = 1;
            Input[1].Value = 0;
            Input[2].Value = 0;
            Input[3].Value = 0;
            Control[0].Value = 1;
            Control[1].Value = 1;
            Control[2].Value = 0;

            if (Outputs[0].GetValue() != 0 && Outputs[1].GetValue() != 0 && Outputs[2].GetValue() != 0 && Outputs[3].GetValue() != 0 && Outputs[1].GetValue() != 0 && Outputs[1].GetValue() != 0 && Outputs[1].GetValue() != 1 && Outputs[1].GetValue() != 0)

            {
                return false;
            }

            return true;



        }
    }
}
