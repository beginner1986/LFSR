using System;

namespace LFSR
{
    // class contains all the FIPS tests methods
    public class Tests
    {
        public bool MonobitTest(string sample)
        {
            // bits with "1" value counter
            int onesCount = 0;

            // if current bit is equal to 1 increment the counter
            foreach (char c in sample)
            {
                if (c == '1')
                    onesCount++;
            }

            // check if repetioions are over or below the limit - test failed
            if (onesCount < 9725 || onesCount > 10275)
                return false;
            // test passed
            else
                return true;
        }

        public bool PokerTest(string sample)
        {
            // all 16 possible bit configurations occurances count - f(i)
            int[] counts = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // split sample bit stream into substrings
            for (int i = 0; i < 20000; i += 4)
            {
                // current substring
                string bits = sample.Substring(i, 4);
                // decimal value of the current binary substring
                int bitsValue = 0;

                // count decimal value of the current substring
                for (int j = 0; j < 4; j++)
                {
                    if (bits[j] == '1')
                        bitsValue += (int)Math.Pow(2, 3 - j);
                }

                // increment adequate occurances counter
                counts[bitsValue]++;
            }

            // calculate x - the reference value for the test
            double x = 0;
            for (int i = 0; i < 16; i++)
                x = (double)counts[i] * (double)counts[i];

            x *= (16.0 / 5000.0);
            x -= 5000.0;

            // check the condition and return the result
            return (2.16 < x) && (x < 46.17);
        }

        public bool LongRunsTest(string sample)
        {
            // current run length
            int repetitionsCount = 0;

            for (int i = 1; i < sample.Length; i++)
            {
                // check if bit is repeted
                if (sample[i] == sample[i - 1])
                {
                    // if repetitions count is above the limit test is failed
                    repetitionsCount++;
                    if (repetitionsCount > 26)
                        return false;
                }
                else
                    // if current bit differs from previous reset the count
                    repetitionsCount = 0;
            }

            // if test didn't fail it's passed
            return true;
        }
    }
}
