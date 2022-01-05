using System;
using System.Diagnostics;

class GFG
{
    static int smallestSumSubarr(int[] arr, int n)
    {
        int last_idx = 0; 
        int start_idx = 0;
        int end_idx = 0;   

        // to store the minimum value that is ending up to the current index 
        int min_ending_here = 2147483647;
        // to store the minimum value encountered so far 
        int min_so_far = 2147483647;
        // traverse the array elements 
        for (int i = 0; i < n; i++)
        {
            // if min_ending_here > 0, then it could 
            // not possibly contribute to the 
            // minimum sum further 
            if (min_ending_here > 0)
            {
                min_ending_here = arr[i];
                last_idx = i;
            }
            // else add the value arr[i] to 
            // min_ending_here 
            else
            {
                min_ending_here += arr[i];
            }

            if (min_so_far > min_ending_here)
            {
                min_so_far = min_ending_here;
                start_idx = last_idx;
                end_idx = i;
            }
        }

        /*
        //Console.WriteLine("Start index = " + start_idx);
        //Console.WriteLine("End index = " + end_idx);
        Console.WriteLine("\n\nSmallest sum contiguous subarray ");
        for (int i = start_idx; i <= end_idx; i++)
            Console.Write(arr[i] + " ");
        */ 

        return min_so_far;
    }

    public static void Main()
    {
        int n = 500000000;
        int[] Num = new int[n];
        Random RandomNumber = new Random();
        for (int i = 0; i < n; i++)
        {
            Num[i] = RandomNumber.Next(-10000, 10000);
        }
        Console.WriteLine(n);
        long laikas;
        Stopwatch watch = Stopwatch.StartNew();

        /*
        int[] Num = { 4, 2, -2, 5, -5, -2, -5, -4, 4, 2, -2, 2, -5,
            -2, -5, -4, 4, 2, -2, 5, -5 };
        //int n = Num.Length;
        
        Console.WriteLine("Original array");
        for (int i = 0; i < n; i++)
            Console.Write(Num[i] + " ");
        */
        watch.Start();
        Console.WriteLine("\n\nSmallest sum: " + smallestSumSubarr(Num, n));
        watch.Stop();
        laikas = watch.ElapsedMilliseconds;
        Console.WriteLine(laikas + "ms" + "\n");
    }
}