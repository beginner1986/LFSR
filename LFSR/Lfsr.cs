using System;

namespace LFSR
{
    public class Lfsr
    {
        protected bool[] Register { get; }  // register value
        protected bool[] Function { get; }  // feedback loop function

        public Lfsr(int len) 
        {
            Register = new bool[len];
            Function = new bool[len];

            // randomly init register values
            Random random = new Random();

            for (int i = 0; i < len; i++)
                Register[i] = (random.Next(0, 2) > 0 ? true : false);

            // remember initial register value
            Function = Register;
        }

        // shift the register
        public bool Shift()
        {
            bool result = Register[^1]; // ^1 == Register.Length - 1

            for (int i=1; i<Function.Length; i++)
            {
                result ^= Function[i];
            }

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

        public string FunctionToString()
        {
            string result = "";

            for (int i = 0; i < Function.Length; i++)
            {
                if (Function[i])
                    result += '1';
                else
                    result += '0';
            }

            return result;
        }
    }
}
