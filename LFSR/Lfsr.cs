using System;
using System.Collections.Generic;
using System.Text;

namespace LFSR
{
    public class Lfsr
    {
        protected bool[] Register { get; }
        protected bool[] InitialRegister { get; }

        public Lfsr(int len) 
        {
            Random random = new Random();
            Register = new bool[len];
            InitialRegister = new bool[len];

            // randomly init register values
            for(int i=0; i<len; i++)
            {
                Register[i] = (random.Next(0, 1) > 0 ? true : false);
            }

            // remember initial register value
            InitialRegister = Register;
        }

        // shift register each type
        public bool Shift()
        {
            bool result = Register[Register.Length - 1];

            for(int i= Register.Length - 1; i>0; i--)
            {
                Register[i] = Register[i - 1];
            }
            Register[0] = result;

            return result;
        }

        public override string ToString()
        {
            string result = "";

            for(int i=0; i<Register.Length; i++)
            {
                if (Register[i])
                    result += '1';
                else
                    result += '0';
            }

            return result;
        }

        public string InitialToString()
        {
            string result = "";

            for (int i = 0; i < InitialRegister.Length; i++)
            {
                if (InitialRegister[i])
                    result += '1';
                else
                    result += '0';
            }

            return result;
        }
    }
}
