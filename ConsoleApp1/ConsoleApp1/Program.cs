using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            int k = 3;
            int[,] matrix = new int[n, n];

            // заповнюємо матрицю випадковими числами від -9 до 9
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = rand.Next(-9, 10);
                }
            }

            // шукаємо підматрицю з максимальною сумою елементів за допомогою паралельного обчислення
            int[,] bestSubmatrix = FindSubmatrixParallel(matrix, k);

            // виводимо результат
            Console.WriteLine("Matrix:");
            PrintMatrix(matrix);
            Console.WriteLine("");
            Console.WriteLine($"Best submatrix of size {k}x{k} with sum {GetSubmatrixSum(bestSubmatrix)}:");
            PrintMatrix(bestSubmatrix);
            Console.ReadLine();
        }
        public static int[,] FindSubmatrixParallel(int[,] matrix, int k)
        {
            int n = matrix.GetLength(0);
            int bestSum = int.MinValue;
            int[,] bestSubmatrix = new int[k, k];

            Parallel.For(0, n - k + 1, i =>
            {
                for (int j = 0; j < n - k + 1; j++)
                {
                    int sum = 0;
                    for (int x = i; x < i + k; x++)
                    {
                        for (int y = j; y < j + k; y++)
                        {
                            sum += matrix[x, y];
                        }
                    }
                    if (sum > bestSum)
                    {
                        bestSum = sum;
                        bestSubmatrix = GetSubmatrix(matrix, i, j, k);
                    }
                }
            });

            return bestSubmatrix;
        }

        private static int[,] GetSubmatrix(int[,] matrix, int i, int j, int k)
        {
            int[,] submatrix = new int[k, k];
            for (int x = i; x < i + k; x++)
            {
                for (int y = j; y < j + k; y++)
                {
                    submatrix[x - i, y - j] = matrix[x, y];
                }
            }
            return submatrix;
        }

        private static int GetSubmatrixSum(int[,] submatrix)
        {
            int sum = 0;
            for (int i = 0; i < submatrix.GetLength(0); i++)
            {
                for (int j = 0; j < submatrix.GetLength(1); j++)
                {
                    sum += submatrix[i, j];
                }
            }
            return sum;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
            }

        }
    }
}

