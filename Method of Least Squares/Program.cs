using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemAnalis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("X= ");
            int[] argumentsX = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
            Console.Write("Y= ");
            double[] argumentsY = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .ToArray();

            int[,] matrixAPolinomOne = new int[argumentsX.Length, 2];
            int[,] matrixPolinomTwo = new int[argumentsX.Length, 3];
            FullMatrixA(matrixAPolinomOne, matrixPolinomTwo, argumentsX);
            Console.WriteLine("First Look At matrix of polinom 1: ");
            PrintMatrix(matrixAPolinomOne);
            Console.Write("First Look At matrix of polinom 2: ");
            Console.WriteLine();
            PrintMatrix(matrixPolinomTwo);

            int[,] transportMatrixAPolinomOne = TransopotingAlgorithm(matrixAPolinomOne);
            int[,] transportMatrixAPolinomTwo = TransopotingAlgorithm(matrixPolinomTwo);
            Console.WriteLine("===============");

            Console.WriteLine("Second step after transport matrix of polinom 1: ");
            PrintMatrix(transportMatrixAPolinomOne);
            Console.WriteLine("Second step after transport matrix of polinom 2: ");
            PrintMatrix(transportMatrixAPolinomTwo);
            Console.WriteLine("===============");

            List<int> firstRowResutOfMultoplication = MultiplicateMatrix(matrixAPolinomOne, transportMatrixAPolinomOne);
            List<int> secondRowResutOfMultoplication = MultiplicateMatrix(matrixPolinomTwo, transportMatrixAPolinomTwo);
            int[,] multiplcateMatrixPolinomOne = new int[matrixAPolinomOne.GetLength(1), matrixAPolinomOne.GetLength(1)];
            int[,] multiplicatePolinomTwo = new int[matrixPolinomTwo.GetLength(1), matrixPolinomTwo.GetLength(1)];
            FullMatrixFromList(firstRowResutOfMultoplication, multiplcateMatrixPolinomOne);
            FullMatrixFromList(secondRowResutOfMultoplication, multiplicatePolinomTwo);
            Console.WriteLine("After multiplication matrixA on matrixAT of polinom 1:");
            PrintMatrix(multiplcateMatrixPolinomOne);
            Console.WriteLine("After multiplication matrixA on matrixAT of polinom 2:");
            PrintMatrix(multiplicatePolinomTwo);
            Console.WriteLine("===============");

            Console.WriteLine("Determinanant at polinom One:");
            int determinantOfPolinomOne = GetDeterminantAtPolinomOne(multiplcateMatrixPolinomOne);
            Console.WriteLine(determinantOfPolinomOne);
            Console.WriteLine("Determinanant at polinom Two:");
            int determinantOfPolinomTwo = RuleOfTriangle(multiplicatePolinomTwo);
            Console.WriteLine(determinantOfPolinomTwo);
            Console.WriteLine("===============");

            double quadraticErrorOfPolinomOne = double.MaxValue;
            double quadraticErrorOfPolinomTwo = double.MaxValue;

            bool isZeroOfPolinomOne = CheckIsDeterminantDiffrentOfZero(determinantOfPolinomOne);
            if (!isZeroOfPolinomOne)
            {
                Console.WriteLine("Reverse Matrix of PolinomOne:");
                double[,] reverseMatrixOfPolinomOne = AlgorithmFindReverseMatrixOfPolinmOne
                    (multiplcateMatrixPolinomOne, determinantOfPolinomOne);
                PrintDoubleMatrix(reverseMatrixOfPolinomOne);
                Console.WriteLine("===============");

                double[,] multplicateMatrixYOfPolinomTwo = multiplicationTransoprtMatrixOnMatrixB
                  (transportMatrixAPolinomOne, argumentsY);
                Console.WriteLine("Multiplication of transportMatrixA on argumentsY of PolinomOne:");
                PrintDoubleMatrix(multplicateMatrixYOfPolinomTwo);
                Console.WriteLine("===============");

                double[,] multplicateMatrixYOfPolinomOne = multiplicationTransoprtMatrixOnMatrixB
                    (transportMatrixAPolinomOne, argumentsY);
                Console.WriteLine("Coefficient of PolynomOne:");
                double[,] coefficientsOfPolynomOne = CalculateCoefficients
                    (reverseMatrixOfPolinomOne, multplicateMatrixYOfPolinomOne);
                PrintDoubleMatrix(coefficientsOfPolynomOne);
                double[] grades = GetGradesOfPolynomOne(coefficientsOfPolynomOne, argumentsX);
                quadraticErrorOfPolinomOne = CalculateQuadraticError(grades, argumentsY);
                Console.WriteLine($"The quadraticError of PolinomOne is: {quadraticErrorOfPolinomOne:f2}");

            }
            else
            {
                Console.WriteLine("No solition of polynomOne!!!");
            }
            bool isZeroOfPolinomTwo = CheckIsDeterminantDiffrentOfZero(determinantOfPolinomTwo);

            if (!isZeroOfPolinomTwo)
            {
                Console.WriteLine("Reverse Matrix of PolinomTwo:");
                List<int> valuesOfReverseMatrixOfPolinomTwo = AlgorithmFindReverseMatrixOfPolinmTwo
                  (multiplicatePolinomTwo);
                int[,] reverseMatrixOfPolinomTwo = new int[3, 3];
                FullMatrixFromList(valuesOfReverseMatrixOfPolinomTwo, reverseMatrixOfPolinomTwo);
                double[,] finaleReverseMatrixOfPolinomTwo =
                    CalcualateFinaleReverseMatrix(reverseMatrixOfPolinomTwo, determinantOfPolinomTwo);
                PrintDoubleMatrix(finaleReverseMatrixOfPolinomTwo);
                Console.WriteLine("===============");

                double[,] multplicateMatrixYOfPolinomTwo = multiplicationTransoprtMatrixOnMatrixB
                   (transportMatrixAPolinomTwo, argumentsY);
                Console.WriteLine("Multiplication of transportMatrixA on argumentsY of PolinomTwo:");
                PrintDoubleMatrix(multplicateMatrixYOfPolinomTwo);
                Console.WriteLine("===============");

                Console.WriteLine("Coefficient of PolynomTwo:");
                double[,] coefficientsOfPolynomTwo = CalculateCoefficients
                    (finaleReverseMatrixOfPolinomTwo, multplicateMatrixYOfPolinomTwo);
                PrintDoubleMatrix(coefficientsOfPolynomTwo);
                double[] grades = GetGradesOfPolynomTwo(coefficientsOfPolynomTwo, argumentsX);
                quadraticErrorOfPolinomTwo = CalculateQuadraticError(grades, argumentsY);
                Console.WriteLine("===============");
                Console.WriteLine($"The quadraticError of PolinomTwo is: {quadraticErrorOfPolinomTwo:f2}");
                if (quadraticErrorOfPolinomOne < quadraticErrorOfPolinomTwo)
                {
                    Console.WriteLine("A first-order polynomial is the better approximation!");
                }
                else
                {
                    Console.WriteLine("A second-order polynomial is the better approximation!");
                }

            }
            else
            {
                Console.WriteLine("No solition of polinomTwo!!!");
            }



        }

        static void FullMatrixA(int[,] matrixAPolinomOne, int[,] matrixAPolinomTwo,
            int[] argumentsX)
        {

            int col = 0;
            for (int row = 0; row < argumentsX.Length; row++)
            {
                matrixAPolinomOne[row, col] = argumentsX[row];
            }

            for (int row = 0; row < argumentsX.Length; row++)
            {
                matrixAPolinomOne[row, col + 1] = 1;

            }

            col = 0;

            for (int row = 0; row < argumentsX.Length; row++)
            {
                matrixAPolinomTwo[row, col] = (int)(Math.Pow(argumentsX[row], 2));
            }

            for (int row = 0; row < argumentsX.Length; row++)
            {
                matrixAPolinomTwo[row, col + 1] = argumentsX[row];
            }

            for (int row = 0; row < argumentsX.Length; row++)
            {
                matrixAPolinomTwo[row, col + 2] = 1;
            }

        }
        static void PrintMatrix(int[,] matrixA)
        {
            for (int row = 0; row < matrixA.GetLength(0); row++)
            {
                for (int col = 0; col < matrixA.GetLength(1); col++)
                {
                    Console.Write($"{matrixA[row, col]} ");
                }
                Console.WriteLine();
            }
        }
        static void PrintDoubleMatrix(double[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]:f2} ");
                }
                Console.WriteLine();
            }
        }
        static int[,] TransopotingAlgorithm(int[,] matrixA)
        {
            int[,] matrixTransport = new int[matrixA.GetLength(1), matrixA.GetLength(0)];

            for (int col = 0; col < matrixA.GetLength(1); col++)
            {
                for (int row = 0; row < matrixA.GetLength(0); row++)
                {
                    matrixTransport[col, row] = matrixA[row, col];
                }


            }
            return matrixTransport;
        }
        static List<int> MultiplicateMatrix(int[,] matrixAPolinom, int[,] transportMatrixAPolinom)
        {
            int[] currentColum = new int[10];
            List<int> resultOfMultiplication = new List<int>(0);
            for (int col = 0; col < matrixAPolinom.GetLength(1); col++)
            {
                for (int row = 0; row < matrixAPolinom.GetLength(0); row++)
                {
                    currentColum[row] = matrixAPolinom[row, col];
                }
                int currentResult = 0;
                for (int currentRow = 0; currentRow < transportMatrixAPolinom.GetLength(0); currentRow++)
                {
                    for (int currentCol = 0; currentCol < transportMatrixAPolinom.GetLength(1); currentCol++)
                    {

                        currentResult += currentColum[currentCol] * transportMatrixAPolinom[currentRow, currentCol];
                    }
                    resultOfMultiplication.Add(currentResult);
                    currentResult = 0;
                }

            }

            return resultOfMultiplication;


        }
        static void FullMatrixFromList(List<int> results, int[,] resultMatrix)
        {
            int currentIndex = 0;
            for (int row = 0; row < resultMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < resultMatrix.GetLength(1); col++)
                {
                    resultMatrix[row, col] = results[currentIndex];
                    ++currentIndex;
                }
            }
        }
        static int GetDeterminantAtPolinomOne(int[,] multiplcateMatrixPolinomOne)
        {
            int size = multiplcateMatrixPolinomOne.GetLength(1);
            int firstDiagonal = 1;
            for (int cur = 0; cur < size; cur++)
            {
                firstDiagonal *= multiplcateMatrixPolinomOne[cur, cur];
            }
            int secondDiagonal = 1;
            int currentRow = 0;
            int currentCol = size - 1;
            for (int cur = size - 1; cur >= 0; cur--)
            {

                secondDiagonal *= multiplcateMatrixPolinomOne[currentRow, currentCol];
                ++currentRow;
                --currentCol;

            }
            int determinant = firstDiagonal - secondDiagonal;
            return determinant;
        }
        static bool CheckIsDeterminantDiffrentOfZero(int determinant)
        {
            bool isZero = false;
            if (determinant == 0)
            {
                isZero = true;
            }
            return isZero;
        }
        static int RuleOfTriangle(int[,] matrix)
        {
            int determinant = (matrix[0, 0] * matrix[1, 1] * matrix[2, 2]) +
                (matrix[0, 1] * matrix[1, 2] * matrix[2, 0])
                + (matrix[0, 2] * matrix[1, 0] * matrix[2, 1]) -
                (matrix[0, 2] * matrix[1, 1] * matrix[2, 0]) -
                (matrix[0, 0] * matrix[1, 2] * matrix[2, 1]) -
                (matrix[0, 1] * matrix[1, 0] * matrix[2, 2]);
            return determinant;
        }
        static double[,] AlgorithmFindReverseMatrixOfPolinmOne(int[,] multiplcateMatrixPolinomOne, int determinantOfPolinomOne)
        {
            double[,] reverseMatirx = new double[multiplcateMatrixPolinomOne.GetLength(0),
               multiplcateMatrixPolinomOne.GetLength(1)];
            int swamp = multiplcateMatrixPolinomOne[0, 0];
            multiplcateMatrixPolinomOne[0, 0] = multiplcateMatrixPolinomOne[1, 1];
            multiplcateMatrixPolinomOne[1, 1] = swamp;
            multiplcateMatrixPolinomOne[0, 1] *= -1;
            multiplcateMatrixPolinomOne[1, 0] *= -1;
            double valueOfDetA = (1.00 / determinantOfPolinomOne);
            for (int row = 0; row < multiplcateMatrixPolinomOne.GetLength(0); row++)
            {
                for (int col = 0; col < multiplcateMatrixPolinomOne.GetLength(1); col++)
                {
                    reverseMatirx[row, col] = (multiplcateMatrixPolinomOne[row, col] * valueOfDetA);

                }
            }
            return reverseMatirx;

        }
        static List<int> CalculateCurrentResult(int[,] multiplicatePolinomTwo, int currentRow, int currentCol)
        {

            List<int> valuesOfSquareMatrix = new List<int>();
            for (int row = 0; row < multiplicatePolinomTwo.GetLength(1); row++)
            {
                for (int col = 0; col < multiplicatePolinomTwo.GetLength(0); col++)
                {
                    if (currentRow == row || col == currentCol)
                    {

                        continue;

                    }
                    valuesOfSquareMatrix.Add(multiplicatePolinomTwo[row, col]);

                }
            }
            return valuesOfSquareMatrix;

        }
        static List<int> AlgorithmFindReverseMatrixOfPolinmTwo(int[,] multiplicatePolinomTwo)
        {
            int[,] currentSquareMatrix = new int[2, 2];
            List<int> results = new List<int>();
            for (int row = 0; row < multiplicatePolinomTwo.GetLength(0); row++)
            {
                for (int col = 0; col < multiplicatePolinomTwo.GetLength(1); col++)
                {
                    List<int> valuesOfCurrentRow = CalculateCurrentResult(multiplicatePolinomTwo, row, col);
                    FullMatrixFromList(valuesOfCurrentRow, currentSquareMatrix);
                    int sumOfColAndRow = (col + 1) + (row + 1);
                    int determinantOfSquare = GetDeterminantAtPolinomOne(currentSquareMatrix);
                    int currentValue = (int)Math.Pow(-1, sumOfColAndRow) * (determinantOfSquare);
                    results.Add(currentValue);

                }
            }
            return results;


        }
        static double[,] CalcualateFinaleReverseMatrix(int[,] reverseMatrixOfPolinomTwo,
            int determinantOfPolinomTwo)
        {
            double[,] oppisteMatrix = new double[3, 3];
            for (int row = 0; row < oppisteMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < oppisteMatrix.GetLength(1); col++)
                {
                    oppisteMatrix[row, col] = (1.00 / determinantOfPolinomTwo) *
                        reverseMatrixOfPolinomTwo[row, col];
                }
            }
            return oppisteMatrix;

        }
        static double[,] multiplicationTransoprtMatrixOnMatrixB(int[,] transportMatrixA, double[] argumentsY)
        {
            double[,] resultMatrix = new double[transportMatrixA.GetLength(0), 1];
            for (int row = 0; row < transportMatrixA.GetLength(0); row++)
            {
                double currentResult = 0;
                for (int col = 0; col < argumentsY.Length; col++)
                {
                    currentResult += (transportMatrixA[row, col] * argumentsY[col]);
                }
                resultMatrix[row, 0] = currentResult;
            }
            return resultMatrix;
        }
        static double[,] CalculateCoefficients(double[,] reverseMatrix, double[,] multplicateMatrixY)
        {
            double[,] coefficients = new double[multplicateMatrixY.GetLength(0),
                multplicateMatrixY.GetLength(1)];
            for (int row = 0; row < reverseMatrix.GetLength(0); row++)
            {
                double currentResult = 0;
                for (int col = 0; col < reverseMatrix.GetLength(1); col++)
                {
                    currentResult += reverseMatrix[row, col] * multplicateMatrixY[col, 0];

                }
                coefficients[row, 0] = currentResult;
            }
            return coefficients;

        }
        static double[] GetGradesOfPolynomOne(double[,] coefficientsOfPolynomOne, int[] argumentsX)
        {
            double[] grades = new double[argumentsX.Length];
            for (int cur = 0; cur < argumentsX.Length; cur++)
            {
                grades[cur] = (coefficientsOfPolynomOne[0, 0] * argumentsX[cur]) +
                    coefficientsOfPolynomOne[1, 0];
            }
            return grades;
        }
        static double[] GetGradesOfPolynomTwo(double[,] coefficientsOfPolynomTwo, int[] argumentsX)
        {
            double[] grades = new double[argumentsX.Length];
            for (int cur = 0; cur < argumentsX.Length; cur++)
            {
                grades[cur] = (Math.Pow(argumentsX[cur], 2) * coefficientsOfPolynomTwo[0, 0])
                    + (argumentsX[cur] * coefficientsOfPolynomTwo[1, 0])
                    + coefficientsOfPolynomTwo[2, 0];
            }
            return grades;
        }
        static double CalculateQuadraticError(double[] grades, double[] argumentsY)
        {

            double[] calculateGrades = new double[argumentsY.Length];
            for (int cur = 0; cur < argumentsY.Length; cur++)
            {
                calculateGrades[cur] = Math.Pow((argumentsY[cur] - grades[cur]), 2);
            }
            double sum = calculateGrades.Sum();
            double quadraticError = Math.Sqrt(sum);
            return quadraticError;
        }
    }
}
