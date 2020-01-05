using System;

namespace LFSR
{
    public class Lfsr
    {
        private readonly bool[] register;   // register value
        protected bool[] function; // feedback loop function

        public Lfsr(int len) 
        {
            register = new bool[len];
            function = new bool[len];

            // randomly init register values
            Random random = new Random();

            for (int i = 0; i < len; i++)
                register[i] = (random.Next(0, 2) > 0 ? true : false);

            // remember initial register value
            function = register;
        }

        // shift the register
        public bool Shift()
        {
            bool result = register[^1]; // ^1 == Register.Length - 1

            for (int i=1; i<function.Length; i++)
            {
                result ^= function[i];
            }

            for(int i= register.Length - 1; i>0; i--)
            {
                register[i] = register[i - 1];
            }

            register[0] = result;

            return result;
        }

        public override string ToString()
        {
            string result = "";

            for(int i=0; i<register.Length; i++)
            {
                if (register[i])
                    result += '1';
                else
                    result += '0';
            }

            return result;
        }

        public string FunctionToString()
        {
            string result = "";

            for (int i = 0; i < function.Length; i++)
            {
                if (function[i])
                    result += '1';
                else
                    result += '0';
            }

            return result;
        }
    }
}
