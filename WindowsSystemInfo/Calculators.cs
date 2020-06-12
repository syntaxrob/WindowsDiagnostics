using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace WindowsSystemInfo
{
    public class Calculators
    {
        public static string DiskSpaceBytesCalc(decimal input, bool convert)
        {
            decimal kbs;
            decimal mbs;
            decimal gbs;
            decimal tbs;

            if (convert)
            {
                kbs = input;
                mbs = input / 1000;
                gbs = input / 1000000;
                tbs = input / 1000000000; ;
            }
            else
            {
                kbs = input / 1000;
                mbs = input / 100000;
                gbs = input / 1000000000;
                tbs = input / 1000000000000;
            }

            var kbsCount = Math.Floor(Math.Log10(Convert.ToDouble(decimal.Floor(kbs))) + 1);
            var mbsCount = Math.Floor(Math.Log10(Convert.ToDouble(decimal.Floor(mbs))) + 1);
            var gbsCount = Math.Floor(Math.Log10(Convert.ToDouble(decimal.Floor(gbs))) + 1);
            var tbsCount = Math.Floor(Math.Log10(Convert.ToDouble(decimal.Floor(tbs))) + 1);

            if (kbsCount <= 3)
            {
                return Math.Round(kbs, 3) + " kb";
            }
            if (mbsCount <= 3)
            {
                return Math.Round(mbs, 3) + " mb";
            }
            if (gbsCount <= 3)
            {
                return Math.Round(gbs, 3) + " gb";
            }
            if (tbsCount <= 3)
            {
                return Math.Round(tbs, 3) + " tb";
            }

            return Math.Round(gbs, 3) + " gb";
        }
    }
}
