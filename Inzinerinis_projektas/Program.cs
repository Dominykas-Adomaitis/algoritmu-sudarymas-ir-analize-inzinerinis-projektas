using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tesiogine_rekursija
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 50;
            long laikas;
            Stopwatch watch = Stopwatch.StartNew();
            
            Console.WriteLine("Kiekis n: " + n);
            Console.WriteLine("Rekursija: " + F1(n));
            watch.Stop();
            laikas = watch.ElapsedMilliseconds;
            Console.WriteLine(laikas + "ms" + "\n");
             
            watch.Reset();
            watch.Start();
            Console.WriteLine("Dinaminis: " + F2(n));
            watch.Stop();
            laikas = watch.ElapsedMilliseconds;
            Console.WriteLine(laikas + "ms" + "\n");
             
            watch.Reset();
            watch.Start();
            Console.WriteLine("Lygiagretus: " + F3(n));
            watch.Stop();
            laikas = watch.ElapsedMilliseconds;
            Console.WriteLine(laikas + "ms" + "\n");
            
        }


        //T(n) = F(n-1) + F(n-4) + 9*F(n/5) + 1
        static long F1(int n)
        {
            long sum = 0;
            if (n <= 1)
                return 1;
            long a = F1(n - 1);
            long b = F1(n - 4);
            for (int i = 0; i < 9; i++)
            {
                sum += F1(n / 5);
            }
            long d = 0;
            return d = a + b + sum + 1;
        }

        //T(n) = F(n-1) + F(n-4) + 9*F(n/5) + 1
        static long F2_aux(int n, List<long> r)
        {
            long q;
            if (n <= 1)
                return 1;
            //Tikrina ar reiksme yra turimame liste 
            //jeigu yra ima is list jei ne eina toliau
            if (r[n] > int.MinValue)
                return r[n];
            q = int.MinValue;

            long a = F2_aux(n - 1, r);
            long b = F2_aux(n - 4, r);
            long c1 = F2_aux(n / 5, r);
            long c2 = F2_aux(n / 5, r);
            long c3 = F2_aux(n / 5, r);
            long c4 = F2_aux(n / 5, r);
            long c5 = F2_aux(n / 5, r);
            long c6 = F2_aux(n / 5, r);
            long c7 = F2_aux(n / 5, r);
            long c8 = F2_aux(n / 5, r);
            long c9 = F2_aux(n / 5, r);
            q = a + b + c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + 1;
            r[n] = q;
            return q;
        }

        static long F2(int n)
        {
            if (n <= 1)
                return 1;
            List<long> r = new List<long>();
            for (int i = 0; i <= n; i++)
            {
                r.Add(int.MinValue);
            }
            return F2_aux(n, r);
        }

        class CustomData
        {
            public long TNum;
            public long TResult;
        }

        //T(n) = F(n-1) + F(n-4) + 9*F(n/5) + 1
        static long F3(int n)
        {
            long funkc = 0;
            if (n < 6) funkc = F1(n);
            else
            {
                int countCPU = 3;
                Task[] tasks = new Task[countCPU];
                tasks[0] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData; if (data == null) return;
                        data.TResult = F1(n - 1);
                    },
                        new CustomData() { TNum = 0 });


                tasks[1] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData; if (data == null) return;
                        data.TResult = F1(n - 4);
                    },
                        new CustomData() { TNum = 0 });

                tasks[2] = Task.Factory.StartNew(
                    (Object p) =>
                    {
                        var data = p as CustomData; if (data == null) return;
                        data.TResult = F1(n / 5);
                    },
                        new CustomData() { TNum = 0 });

                Task.WaitAll(tasks);
                funkc = (tasks[0].AsyncState as CustomData).TResult
                    + (tasks[1].AsyncState as CustomData).TResult
                    + 9 * (tasks[2].AsyncState as CustomData).TResult + 1;
            }
            return funkc;
        }
    }
}
