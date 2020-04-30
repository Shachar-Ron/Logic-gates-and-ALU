using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class ALU : Gate
    {
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Output { get; private set; }
        public Wire ZeroX { get; private set; }
        public Wire ZeroY { get; private set; }
        public Wire NotX { get; private set; }
        public Wire NotY { get; private set; }
        public Wire F { get; private set; }
        public Wire NotOutput { get; private set; }
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }
        public int Size { get; private set; }

        //your code here

        //zx  //zy
        private WireSet newzero;
        private BitwiseMux muxX;
        private BitwiseMux muxY;

        //nx
        private BitwiseNotGate notX;
        private BitwiseMux muxNX;
        //ny
        private BitwiseNotGate notY;
        private BitwiseMux muxNY;

        //f
        private BitwiseAndGate nxnyAnd;
        private MultiBitAdder nxnyMultiAdder;
        private BitwiseMux AndOrNotmux;

        //no
        private BitwiseNotGate NAndOrNot;
        private BitwiseMux noMux;


        //***************************************************

        //zero
        private BitwiseNotGate Nzero;
        private MultiBitAndGate andZero;

        //ng
        private MuxGate ngMux;
        private Wire one;
        private Wire zero;

        //private MuxGate m_gmuxZero;
        
        

        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            ZeroX = new Wire();
            ZeroY = new Wire();
            NotX = new Wire();
            NotY = new Wire();
            F = new Wire();
            NotOutput = new Wire();
            Negative = new Wire();            
            Zero = new Wire();

            //m_gmuxZero = new MuxGate();


            newzero = new WireSet(Size);
            //zx
            muxX = new BitwiseMux(Size);
            muxX.ConnectInput1(InputX);
            muxX.ConnectInput2(newzero);
            muxX.ConnectControl(ZeroX);

            //zy
            muxY = new BitwiseMux(Size);
            muxY.ConnectInput1(InputY);
            muxY.ConnectInput2(newzero);
            muxY.ConnectControl(ZeroY);


            //nx
            notX = new BitwiseNotGate(Size);
            notX.ConnectInput(muxX.Output);

            muxNX = new BitwiseMux(Size);
            muxNX.ConnectInput1(notX.Input);
            muxNX.ConnectInput2(notX.Output);
            muxNX.ConnectControl(NotX);



            //ny
            notY = new BitwiseNotGate(Size);
            notY.ConnectInput(muxY.Output);

            muxNY = new BitwiseMux(Size);
            muxNY.ConnectInput1(notY.Input);
            muxNY.ConnectInput2(notY.Output);
            muxNY.ConnectControl(NotY);



            //f
            nxnyAnd = new BitwiseAndGate(Size);
            nxnyAnd.ConnectInput1(muxNX.Output);
            nxnyAnd.ConnectInput2(muxNY.Output);

            nxnyMultiAdder = new MultiBitAdder(Size);
            nxnyMultiAdder.ConnectInput1(muxNX.Output);
            nxnyMultiAdder.ConnectInput2(muxNY.Output);

            AndOrNotmux = new BitwiseMux(Size);
            AndOrNotmux.ConnectInput1(nxnyAnd.Output);
            AndOrNotmux.ConnectInput2(nxnyMultiAdder.Output);
            AndOrNotmux.ConnectControl(F);

            NAndOrNot = new BitwiseNotGate(Size);
            NAndOrNot.ConnectInput(AndOrNotmux.Output);

            noMux = new BitwiseMux(Size);
            noMux.ConnectInput1(AndOrNotmux.Output);
            noMux.ConnectInput2(NAndOrNot.Output);
            noMux.ConnectControl(NotOutput);

            Output = new WireSet(Size);
            Output.ConnectInput(noMux.Output);

            ///////////////////////////////////////////////////////////////////////


            //not
            Nzero = new BitwiseNotGate(Size);
            Nzero.ConnectInput(Output);

            andZero = new MultiBitAndGate(Size);
            andZero.ConnectInput(Nzero.Output);
            Zero.ConnectInput(andZero.Output);

            //ng
            zero = new Wire();
            zero.Value = 0;
            one = new Wire();
            one.Value = 1;


            ngMux = new MuxGate();
            ngMux.ConnectInput1(zero);
            ngMux.ConnectInput2(one);
            ngMux.ConnectControl(Output[Size - 1]);//MSB
            Negative.ConnectInput(ngMux.Output);


        }

        public override bool TestGate()
        {
            InputX.Set2sComplement(10);
            InputY.Set2sComplement(8);


            ZeroX.Value = 1;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != 0)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 1;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != 1)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 1;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != -1 || Negative.Value != 1)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 0;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != InputX.Get2sComplement())
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 0;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != InputY.Get2sComplement())
                return false;


            /*
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 0;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != -11 || Negative.Value != 1)
                return false;

            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 0;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != -9 || Negative.Value != 1)
                return false;
                */
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 1;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != -10 || Negative.Value != 1)
                return false;
                
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != -8 || Negative.Value != 1)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 1;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 1;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != 11)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 1;
            F.Value = 1;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != 9)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 1;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != 9)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != 7)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != 18)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != 2)
                return false;

            /*
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 0;
            NotY.Value = 1;
            F.Value = 1;
            NotOutput.Value = 1;

            if (Output.Get2sComplement() != -12 || Negative.Value != 1)
                return false;
                */
            return true;
        }
    }
}
