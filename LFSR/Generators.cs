namespace LFSR
{
    // class contains all the generators methods
    public class Generators
    {
        private readonly Lfsr lfsr1;
        private readonly Lfsr lfsr2;
        private readonly Lfsr lfsr3;

        public Generators(Lfsr lfsr1, Lfsr lfsr2, Lfsr lfsr3)
        {
            this.lfsr1 = lfsr1;
            this.lfsr2 = lfsr2;
            this.lfsr3 = lfsr3;
        }

        public string GeffeGenerator(int len)
        {
            string result = "";

            for (int i = 0; i < len; i++)
            {
                bool reg1 = lfsr1.Shift();
                bool reg2 = lfsr2.Shift();
                bool reg3 = lfsr3.Shift();

                bool bit = (reg1 & reg2) ^ (!reg2 & reg3);

                result += bit ? "1" : "0";
            }

            return result;
        }

        public string StopAndGoGenerator(int len)
        {
            string result = "";

            bool reg1;
            bool reg2 = false;
            bool reg3 = false;
            bool bit;

            while (result.Length < len)
            {
                reg1 = lfsr1.Shift();

                if (reg1)
                    lfsr2.Shift();
                else
                    reg3 = lfsr3.Shift();

                bit = reg2 ^ reg3;

                result += bit ? "1" : "0";
            }

            return result;
        }

        public string ShrinkingGenerator(int len)
        {
            string result = "";

            while (result.Length < len)
            {
                bool reg1 = lfsr1.Shift();
                bool reg2 = lfsr2.Shift();
                bool bit;

                if (reg1)
                {
                    bit = reg2;
                    result += bit ? "1" : "0";
                }
            }

            return result;
        }
    }
}
