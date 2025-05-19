using UnityEngine;
using Quest_Studio;

namespace Quest_Studio
{
    public class DrawArray
    {
        // Base True / False
        public static bool GetRandomBool()
        {
            bool randomBool;
            int randomIndex = Random.Range(0, 2);
            if (randomIndex == 0)
            {
                randomBool = false;
            }
            else
            {
                randomBool = true;
            }

            return randomBool;
        }

        // True / False Method 1
        public static bool GetDrawResult(int targetSamples, int maxSamples)
        {
            // Variables
            bool[] samples = new bool[0];
            bool refSample;

            // Add samples in array
            for (int i = 0; i < maxSamples; i++)
            {
                switch (i <= targetSamples)
                {
                    default:
                    case true:
                        refSample = true;
                        ArrayExtension.AddElement(ref samples, ref refSample);
                        break;

                    case false:
                        refSample = false;
                        ArrayExtension.AddElement(ref samples, ref refSample);
                        break;
                }
            }

            // Draw random in samples
            bool drawSample = ArrayExtension.GetRandomElement(ref samples);
            return drawSample;
        }

        // True / False Method 2
        public static bool GetDrawResult(float targetPercentage)
        {
            bool drawResult;

            if (targetPercentage <= 0)
            {
                // Equal zero (always false)
                drawResult = false;
            }
            else if (targetPercentage >= 1)
            {
                // Equal one (always true)
                drawResult = true;
            }
            else
            {
                // Between zero to one
                // Variables
                int numerator = GetNumerator(GetFraction(targetPercentage));
                int denominator = GetDenominator(GetFraction(targetPercentage));

                //Debug.Log(GetFraction(targetPercentage));
                //Debug.Log(numerator + "/" + denominator);

                bool[] samples = new bool[0];
                bool refSample;

                // Add samples in array
                for (int i = 0; i < denominator; i++)
                {
                    switch (i <= numerator)
                    {
                        default:
                        case true:
                            refSample = true;
                            ArrayExtension.AddElement(ref samples, ref refSample);
                            break;

                        case false:
                            refSample = false;
                            ArrayExtension.AddElement(ref samples, ref refSample);
                            break;
                    }
                }

                // Draw random in samples
                bool drawSample = ArrayExtension.GetRandomElement(ref samples);
                drawResult = drawSample;
            }

            return drawResult;
        }

        // Method for more than 2 Options
        public static Type CustomDrawArray<Type>(ref Type[] types, int[] sampleNumber)
        {
            Type drawType = drawType = default(Type);

            if (types.Length != sampleNumber.Length)
            {
                // Not valid for action
                Debug.Log("Types[] & SampleNumber[] not equal length!");
            }
            else
            {
                Type[] samples = new Type[0];
                for (int i = 0; i < sampleNumber.Length; i++)
                {
                    for (int j = 0; j < sampleNumber[i]; j++)
                    {
                        ArrayExtension.AddElement(ref samples, ref types[i]);
                    }
                }
                drawType = ArrayExtension.GetRandomElement(ref samples);
            }

            return drawType;
        }

        // Get fraction, numerator and denominator
        private static string GetFraction(float percentage)
        {
            string fraction = "/";
            if (percentage <= 0)
            {
                fraction = "0/1";
            }
            else if (percentage >= 1)
            {
                fraction = "1/1";
            }
            else
            {
                // Variables
                string stringPercentage = percentage.ToString();
                int stringDecimalLength = stringPercentage.Length - 2;

                string stringDecimal = stringPercentage.Substring(stringPercentage.IndexOf(".") + 1);
                //Debug.Log(stringDecimal);
                string stringDivideNumber = "1";
                for (int i = 0; i < stringDecimalLength; i++)
                {
                    stringDivideNumber += "0";
                }

                int numerator = int.Parse(stringDecimal);
                int denominator = int.Parse(stringDivideNumber);

                float remainder = denominator % numerator;

                if (remainder == 0)
                {
                    //Debug.Log("Fraction: " + "1" + "/" + (denominator / numerator).ToString());
                    //numerator = 1;
                    denominator = denominator / numerator;
                    fraction = "1/" + denominator;
                }
                else
                {
                    int minNumerator = numerator;
                    int minDenominator = denominator;

                    while (minNumerator > 1)
                    {
                        if (IsNoRemainder(minNumerator, minDenominator, 2))
                        {
                            minNumerator = minNumerator / 2;
                            minDenominator = minDenominator / 2;
                        }
                        else if (IsNoRemainder(minNumerator, minDenominator, 3))
                        {
                            minNumerator = minNumerator / 3;
                            minDenominator = minDenominator / 3;
                        }
                        else if (IsNoRemainder(minNumerator, minDenominator, 5))
                        {
                            minNumerator = minNumerator / 5;
                            minDenominator = minDenominator / 5;
                        }
                        else if (IsNoRemainder(minNumerator, minDenominator, 7))
                        {
                            minNumerator = minNumerator / 7;
                            minDenominator = minDenominator / 7;
                        }
                        else if (IsNoRemainder(minNumerator, minDenominator, 9))
                        {
                            minNumerator = minNumerator / 9;
                            minDenominator = minDenominator / 9;
                        }
                        else
                        {
                            //Debug.Log("Fraction: " + minNumerator + "/" + minDenominator);
                            numerator = minNumerator;
                            denominator = minDenominator;
                            fraction = numerator + "/" + denominator;
                            break;
                        }
                    }
                }
            }

            return fraction;
        }

        private static bool IsNoRemainder(float numerator, float denominator, int divideNumber)
        {
            if (numerator % divideNumber == 0 && denominator % divideNumber == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int GetNumerator(string fraction)
        {
            string stringNumerator = fraction.Substring(0, fraction.IndexOf("/"));
            //Debug.Log(stringNumerator);
            return int.Parse(stringNumerator);
        }
        private static int GetDenominator(string fraction)
        {
            string stringDenominator = fraction.Substring(fraction.IndexOf("/") + 1);
            //Debug.Log(stringDenominator);
            return int.Parse(stringDenominator);
        }
    }
}