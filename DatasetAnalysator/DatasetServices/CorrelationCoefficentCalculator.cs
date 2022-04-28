using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.DatasetServices
{
    public class CorrelationCoefficentCalculator
    {
        public static float calculateCorrelationCoefficent(double[] X, double[] Y)
        {
            validateValueLenght(X, Y);

            int n = X.Length;

            double sum_X = 0, sum_Y = 0, sum_XY = 0;
            double squareSum_X = 0, squareSum_Y = 0;

            for (int i = 0; i < n; i++)
            {
                sum_X = sum_X + X[i];
                sum_Y = sum_Y + Y[i];
                sum_XY = sum_XY + X[i] * Y[i];

                squareSum_X = squareSum_X + X[i] * X[i];
                squareSum_Y = squareSum_Y + Y[i] * Y[i];
            }

            float corr = CalculateCorrelation(n, sum_X, sum_Y, squareSum_X, squareSum_Y, sum_XY);

            return corr;
        }

        private static void validateValueLenght(double[] X, double[] Y)
        {
            if (X.Length != Y.Length)
            {
                throw new ArgumentException("Values cannot be different lenghts");
            }
        }

        private static float CalculateCorrelation(int n, double sum_X, double sum_Y, double squareSum_X, double squareSum_Y, double sum_XY)
        {
            return (float)(n * sum_XY - sum_X * sum_Y) /
                         (float)(Math.Sqrt((n * squareSum_X -
                         sum_X * sum_X) * (n * squareSum_Y -
                         sum_Y * sum_Y)));
        }

    }
}
