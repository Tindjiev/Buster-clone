using UnityEngine;


namespace MathNM
{

    public static class MathLib
    {

        public const float PI = Mathf.PI;
        public const float TAU = 2f * PI;
        public const float RAD_TO_DEG = (float)(180.0 / PI);
        public const float DEG_TO_RAD = (float)(PI / 180.0);
        public const float SQRT2 = 1.4142135623730950488f;
        public const float INV_SQRT2 = 0.7071067811865475244f;

        public static int sq(this int number)
        {
            return number * number;
        }
        public static float sq(this float number)
        {
            return number * number;
        }
        public static double sq(this double number)
        {
            return number * number;
        }
        public static decimal sq(this decimal number)
        {
            return number * number;
        }
        public static float Sqrt(this int number)
        {
            return Mathf.Sqrt(number);
        }
        public static float Sqrt(this float number)
        {
            return Mathf.Sqrt(number);
        }
        public static double Sqrt(this double number)
        {
            return System.Math.Sqrt(number);
        }
        public static float Abs(this int number)
        {
            return Mathf.Abs(number);
        }
        public static float Abs(this float number)
        {
            return Mathf.Abs(number);
        }
        public static double Abs(this double number)
        {
            return System.Math.Abs(number);
        }
        public static decimal Abs(this decimal number)
        {
            return System.Math.Abs(number);
        }
        public static int Sign(this int number)
        {
            return System.Math.Sign(number);
        }
        public static float Sign(this float number)
        {
            return Mathf.Sign(number);
        }
        public static int Sign(this double number)
        {
            return System.Math.Sign(number);
        }
        public static int Sign(this decimal number)
        {
            return System.Math.Sign(number);
        }
        public static int SignNo0(this int number)
        {
            if (number == 0)
            {
                return 1;
            }
            return System.Math.Sign(number);
        }
        public static float SignNo0(this float number)
        {
            if (number == 0f)
            {
                return 1f;
            }
            return Mathf.Sign(number);
        }
        public static int SignNo0(this double number)
        {
            if (number == 0.0)
            {
                return 1;
            }
            return System.Math.Sign(number);
        }
        public static int SignNo0(this decimal number)
        {
            if (number == 0m)
            {
                return 1;
            }
            return System.Math.Sign(number);
        }

        public static Vector3 toVector2(this Vector3 v)
        {
            return new Vector3(v.x, v.z);
        }
        public static Vector3 toVector2(this Vector3 v, float zValue)
        {
            return new Vector3(v.x, v.z, zValue);
        }
        public static Vector3 ZeroY(this Vector3 v)
        {
            return new Vector3(v.x, 0f, v.z);
        }

        public static Vector2 MulOnXZ(this Vector3 v1, Vector3 v2)
        {
            return new Vector2(v1.x * v2.x - v1.z * v2.z, v1.x * v2.z + v1.z * v2.x);
        }

        public static Vector3 ConjugateOnXZ(this Vector3 v)
        {
            v.z = -v.z;
            return v;
        }

        public static Vector2 MulConjOnXZ(this Vector3 v1, Vector3 v2)
        {
            return v1.MulOnXZ(v2.ConjugateOnXZ());
        }

        public static float AngleOnXZrad(this Vector3 v)
        {
            return Mathf.Atan2(v.x, v.z);
        }
        public static float AngleOnXZdeg(this Vector3 v)
        {
            return v.AngleOnXZrad() * RAD_TO_DEG;
        }

        public static Vector3 MulElementWise(this Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }


    }

    public static class VectorLib
    {


        public static Vector2 polarVectorRad(float angle)
        {
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        public static Vector2 polarVectorDeg(float angle)
        {
            return polarVectorRad(angle * MathLib.DEG_TO_RAD);
        }

        public static Vector3 ToVector3OnXZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0f, vector.y);
        }

        public static Vector2 Rotate90(this Vector2 vector)
        {
            return new Vector2(-vector.y, vector.x);
        }
        public static Vector3 Rotate90OnXZ(this Vector3 vector)
        {
            return new Vector3(-vector.z, vector.y, vector.x);
        }
        public static Vector3 Rotate90OnYZ(this Vector3 vector)
        {
            return new Vector3(vector.x, -vector.z, vector.y);
        }
        public static Vector3 Rotate90OnXY(this Vector3 vector)
        {
            return new Vector3(-vector.y, vector.x, vector.z);
        }



    }



    public static class MathToString
    {

        public static string ToString(this int number, bool FullWords)
        {
            if (!FullWords)
            {
                return number.ToString();
            }
            if (number == 0)
            {
                return ToStringFullWordFirstNumbers[0];
            }
            else if (number < 0)
            {
                return "negative " + (-number).ToString(true);
            }
            var num = number;
            string str = "";
            int power10third = 0;
            do
            {

                if (power10third == ToStringFullWord10Power3rd.Length)
                {
                    return "too big number: " + number;
                }

                string newThousands = "";
                if (num % 1000 == 1 && num / 1000 != 0)
                {
                    newThousands = ToStringFullWord10Power3rd[power10third];
                }
                else if (num % 1000 != 0)
                {
                    newThousands = ToStringFullWord10Power3rd[power10third];
                    if (num / 1000 != 0 && num / 100 % 10 == 1)
                    {
                        newThousands = newThousands.addStringToFrontWithSpace(ToStringLessThan100(num % 100).addStringToFrontWithSpace(ToStringFullWord10Power[2]));
                    }
                    else
                    {
                        newThousands = newThousands.addStringToFrontWithSpace(ToStringLessThan1000(num % 1000));
                    }
                }

                str = str.addStringToFrontWithSpace(newThousands);
                num /= 1000;
                power10third++;
            } while (num != 0);

            return str;
        }

        private static string ToStringLessThan100(int lessThan100)
        {
            if (lessThan100 == 0)
            {
                return "";
            }
            string strLessthan100;
            if (lessThan100 < ToStringFullWordFirstNumbers.Length)
            {
                strLessthan100 = ToStringFullWordFirstNumbers[lessThan100];
            }
            else
            {
                strLessthan100 = ToStringFullWordDecad[lessThan100 / 10];
                int unit = lessThan100 % 10;
                if (unit != 0)
                {
                    strLessthan100 += " " + ToStringFullWordFirstNumbers[unit];
                }
            }

            return strLessthan100;
        }

        private static string ToStringLessThan1000(int lessThan1000)
        {
            if (lessThan1000 == 0)
            {
                return "";
            }
            string newThousands = ToStringLessThan100(lessThan1000 % 100);
            int hundrends = lessThan1000 / 100;
            if (hundrends != 0)
            {
                newThousands = newThousands.addStringToFrontWithSpace(ToStringFullWordFirstNumbers[hundrends] + " " + ToStringFullWord10Power[2]);
            }
            return newThousands;
        }

        private static string addStringToFrontWithSpace(this string existingString, string newString)
        {
            if (newString == "")
            {
                return existingString;
            }
            else if (existingString == "")
            {
                return newString;
            }
            else
            {
                return newString + " " + existingString;
            }
        }


        public static string ToString(this int num, bool FullWords, bool Ordinal)
        {
            if (!Ordinal)
            {
                return num.ToString(FullWords);
            }
            if (!FullWords)
            {
                if (num <= 0)
                {
                    return "negative ordinal: " + num;
                }

                switch (num % 100)
                {
                    case 11:
                    case 12:
                    case 13:
                        return num + "th";
                }

                switch (num % 10)
                {
                    case 1:
                        return num + "st";
                    case 2:
                        return num + "nd";
                    case 3:
                        return num + "rd";
                    default:
                        return num + "th";
                }
            }
            else
            {
                if (num < ToStringOrdinalFullWordLessFirstNumbers.Length)
                {
                    return ToStringOrdinalFullWordLessFirstNumbers[num];
                }
                else
                {
                    return num.ToString(false, true);
                }
                switch (num)
                {
                    case 1000000:
                        return "milionth";
                }
                if (num < 20)
                {
                    return ToStringOrdinalFullWordLessFirstNumbers[num];
                }
                else if (num % 10 == 0)
                {

                }
                return "big number for fullword ordinal: " + num;
            }
        }



        private static string[] ToStringFullWordFirstNumbers = new string[]
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen",
        };

        private static string[] ToStringFullWordDecad = new string[]
        {
            "",
            ToStringFullWordFirstNumbers[10],
            "twenty",
            "thirty",
            "forty",
            "fifty",
            ToStringFullWordFirstNumbers[6]+"ty",
            ToStringFullWordFirstNumbers[7]+"ty",
            "eighty",
            ToStringFullWordFirstNumbers[9]+"ty",
        };

        private static string[] ToStringFullWord10Power = new string[]
        {
            "",
            ToStringFullWordFirstNumbers[10],
            "hundred",
            "thousand",
        };

        private static string[] ToStringFullWord10Power3rd = new string[]
        {
            "",
            ToStringFullWord10Power[3],
            "million",
            "billion",
            "trillion",
            "quadrillion",
            "quintillion",
            "sextillion",
            "septillion",
            "octillion",
            "nonillion",
            "decillion",
        };


        private static string[] ToStringOrdinalFullWordLessFirstNumbers = new string[]
        {
            ToStringFullWordFirstNumbers[0]+"th",
            "first",
            "second",
            "third",
            ToStringFullWordFirstNumbers[4]+"th",
            "fifth",
            ToStringFullWordFirstNumbers[6]+"th",
            "seventh",
            "eighth",
            "ninth",
            ToStringFullWordFirstNumbers[10]+"th",
            "eleventh",
            "twelfth",
            ToStringFullWordFirstNumbers[13]+"th",
            ToStringFullWordFirstNumbers[14]+"th",
            ToStringFullWordFirstNumbers[15]+"th",
            ToStringFullWordFirstNumbers[16]+"th",
            ToStringFullWordFirstNumbers[17]+"th",
            ToStringFullWordFirstNumbers[18]+"th",
            ToStringFullWordFirstNumbers[19]+"th",
        };


    }

    /*
        Debug.Log((num = 100).ToString(true) + " " + num);
        Debug.Log((num = 101).ToString(true) + " " + num);
        Debug.Log((num = 200).ToString(true) + " " + num);
        Debug.Log((num = 201).ToString(true) + " " + num);
        Debug.Log((num = 301).ToString(true) + " " + num);
        Debug.Log((num = 401).ToString(true) + " " + num);
        Debug.Log((num = 1000).ToString(true) + " " + num);
        Debug.Log((num = 1001).ToString(true) + " " + num);
        Debug.Log((num = 1010).ToString(true) + " " + num);
        Debug.Log((num = 1100).ToString(true) + " " + num);
        Debug.Log((num = 1111).ToString(true) + " " + num);
        Debug.Log((num = 1000000).ToString(true) + " " + num);
        Debug.Log((num = 1000001).ToString(true) + " " + num);
        Debug.Log((num = 1000010).ToString(true) + " " + num);
        Debug.Log((num = 1000100).ToString(true) + " " + num);
        Debug.Log((num = 1001000).ToString(true) + " " + num);
        Debug.Log((num = 1010000).ToString(true) + " " + num);
        Debug.Log((num = 1100000).ToString(true) + " " + num);
        Debug.Log((num = 1111111).ToString(true) + " " + num);
     */
}

