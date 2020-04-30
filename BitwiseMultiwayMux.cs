using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseMultiwayMux : Gate
    {
        public int Size { get; private set; }
        public int ControlBits { get; private set; }
        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        
        private BitwiseMux[] bwisemux;
        int j = 0; int i = 0; int z = 0; int k;

        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];



            for ( i = 0; i < Inputs.Length; i++)
            {
                Inputs[i] = new WireSet(Size);
                
            }

            bwisemux = new BitwiseMux[Inputs.Length - 1];
            int inputsize = Inputs.Length / 2;

            for ( i = 0; i < bwisemux.Length; i++)
            {
                bwisemux[i] = new BitwiseMux(Size);
            }

            int cGate = 0;

            for ( j = 0; j < inputsize; j++)    
            {
                int sum = j * 2;
                bwisemux[cGate].ConnectInput1(Inputs[sum]);
                bwisemux[cGate].ConnectInput2(Inputs[sum + 1]);
                bwisemux[cGate].ConnectControl(Control[0]);
                cGate++;
            }

           
            int temp = 0;
            int temp2 = 2;
            k = 1;
           while(k < cControlBits) 
         
            {

                for ( j = 0; j < Math.Pow(2, cControlBits - temp2); j++, inputsize++, temp = temp + 2)
                {
                    bwisemux[inputsize] = new BitwiseMux(Size);
                    bwisemux[inputsize].ConnectInput1(bwisemux[temp].Output);
                    bwisemux[inputsize].ConnectInput2(bwisemux[temp + 1].Output);
                    bwisemux[inputsize].ConnectControl(Control[k]);
                }
                temp2++;
                k++;
            }
            Output.ConnectInput(bwisemux[bwisemux.Length - 1].Output);
    }


        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }



        public override bool TestGate()
        {


            for(int i=0; i<4; i++)
            {
                Inputs[i].SetValue(0);
            }
            for (int i = 4; i < 8; i++)
            {
                Inputs[i].SetValue(1);
            }

            Control[0].Value = 1;
            Control[1].Value = 0;
            Control[2].Value = 1;



            if (Output.GetValue() != 1)
                return false;
            return true;
        }
    }
}
