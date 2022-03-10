using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задайте размер массива случайных чисел");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(SumArray);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Action<Task<int[]>> action = new Action<Task<int[]>>(PrintArraymax);
            Task task3 = task2.ContinueWith(action);

            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;
        }

        static int[] SumArray(Task<int[]> task)
        {

            int sumArray=0;
            int[] array = task.Result;
            for (int i = 0; i <= array.Count() - 1; i++)
            {
                sumArray += array[i];
            }
            return array;
        }

        static void PrintArraymax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array.Max();
            for (int i = 0; i < array.Count()-1; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine($"\nМаксимальное число массива:\n{max}");

        }
    }
}
