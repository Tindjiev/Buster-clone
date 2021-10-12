using MathNM;
using UnityEngine;

namespace RandNM
{

    /*
     * these need to be updated, they are slightly different now
        Debug.Log(new RandNM.RandFloat(0.5f));                              //prints 50% chance
        Debug.Log(new RandNM.RandFloat(0.5f) + 1f);                         //prints 100% chance
        Debug.Log(new RandNM.RandFloat(0.5f) + new RandNM.RandFloat(0.5f)); //prints 80% chance
        Debug.Log(new RandNM.RandFloat(0.5f) + 0.5f);                       //prints 80% chance
        Debug.Log(new RandNM.RandFloat(0.5f) + 0.1f);                       //prints 64% chance
        Debug.Log(new RandNM.RandFloat(0.5f) + 0.2f);                       //prints 69.23% chance
        Debug.Log(new RandNM.RandFloat(0.5f) + 0.25f);                      //prints 71,33% chance
        Debug.Log(new RandNM.RandFloat(0.5f) + 0.4f);                       //prints 76,74% chance
        Debug.Log(new RandNM.RandFloat(0.5f) * 2f);                         //prints 80% chance
        Debug.Log(new RandNM.RandFloat(0.5f) * 4f);                         //prints 94.12% chance
        Debug.Log(new RandNM.RandFloat(0.5f) * 16f);                        //prints 99.61% chance
     */
    public struct RandFloat
    {

        private float _x;

        private RandFloat(float x, bool irrelevantvalue)
        {
            if (x <= 0f)
            {
                _x = 0f;
                return;
            }
            _x = x;
        }
     
        public RandFloat(float possibility)
        {
            if (possibility >= 1f)
            {
                _x = float.PositiveInfinity;
            }
            else if (possibility <= 0f)
            {
                _x = 0f;
            }
            else
            {
               _x = possibilityFINV(possibility);
            }
        }

        public bool RollDice()
        {
            float p = possibility();
            if (p == 1f || Random.Range(0f, 1f) < p)
            {
                return true;
            }
            return false;
        }


        private float possibility()
        {
            if (_x == float.PositiveInfinity)
            {
                return 1f;
            }
            return possibilityF(_x);
        }


        public static RandFloat operator *(RandFloat p, float x)
        {
            return new RandFloat(p._x * x, true);
        }
        public static RandFloat operator *(float x, RandFloat p)
        {
            return p * x;
        }

        public static RandFloat operator /(RandFloat p, float x)
        {
            return new RandFloat(p._x / x, true);
        }

        public static RandFloat operator +(RandFloat p, float x)
        {
            return p + new RandFloat(x);
        }
        public static RandFloat operator +(float x, RandFloat p)
        {
            return p + x;
        }

        public static RandFloat operator -(RandFloat p, float x)
        {
            return p - new RandFloat(x);
        }
        public static RandFloat operator -(float x, RandFloat p)
        {
            return new RandFloat(x) - p;
        }



        public static RandFloat operator +(RandFloat p1, RandFloat p2)
        {
            return new RandFloat(p1._x + p2._x, true);
        }
        public static RandFloat operator -(RandFloat p1, RandFloat p2)
        {
            return new RandFloat(p1._x - p2._x, true);
        }

        public static implicit operator RandFloat(float x) => new RandFloat(x);

        /*
        public static RandFloat operator *(RandFloat p1, RandFloat p2)
        {
            return new RandFloat(p1._x * p2._x, true);
        }
        public static RandFloat operator /(RandFloat p1, RandFloat p2)
        {
            return new RandFloat(p1._x / p2._x, true);
        }
        */




        //private static readonly float P50PERCENT = possibilityFINV(0.5f);
        public static RandFloat Fifty
        {
            get
            {
                return new RandFloat(1f, true);
            }
        }
        public static RandFloat Zero
        {
            get
            {
                return new RandFloat(0f, true);
            }
        }
        public static RandFloat Hundrend
        {
            get
            {
                return new RandFloat(float.PositiveInfinity, true);
            }
        }


        private static readonly float P99p99PERCENT = possibilityFINV(0.9999f);
        private static readonly float P0p01PERCENT = possibilityFINV(0.0001f);

        public override string ToString()
        {
            if (_x == float.PositiveInfinity)
            {
                return "100%";
            }
            else if (_x > P99p99PERCENT || _x < P0p01PERCENT)
            {
                float p = possibility() * 100f;
                if (p <= 0f)
                {
                    return "0%";
                }
                else if (p >= 100f)
                {
                    return "100%";
                }
                string str = p.ToString("0.###");
                switch (str)
                {
                    case "0,000":
                        return "0,001%";
                    case "100,000":
                        return "99,999%";
                    default:
                        return str + "%";
                }
            }
            else
            {
                return (possibility() * 100f).ToString("0.##") + "%";
            }
        }



        private static float possibilityF(float x)
        {
            return f(x * x);
        }
        private static float possibilityFINV(float x)
        {
            return fINV(x).Sqrt();
        }

        //private const float OFFSET = 1f / 3f;
        //private const float ENDVALUE = 1f + OFFSET;
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static float f(float x)
        {
            return x / (x + 1);
        }


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static float fINV(float x)
        {
            return x / (1 - x);
        }
    }



    public static class Rand
    {

        public static bool RollDice(float chanceForSomethingToHappen)
        {
            if (chanceForSomethingToHappen >= 1f)
            {
                return true;
            }
            else if (chanceForSomethingToHappen <= 0f)
            {
                return false;
            }
            return Random.Range(0f, 1f) <= chanceForSomethingToHappen;
        }

    }


}
