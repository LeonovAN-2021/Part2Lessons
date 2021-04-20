using System;

namespace Lesson_2_1
{
    class TestCase
    {
        public int Argument { get; set; }
        public string Expected { get; set; }
        public Exception ExpectedException { get; set; }
    }

    class Program
    {
        public static string IsSimple(int n)
        {
            int d = 0;
            int i = 2;
            while (i<n)
            {
                if (n%i==0) { d++;} 
                i++; 
            }
            return d == 0 ? "простое" : "не простое";
        }

        public static int StrangeSum(int[] inputArray)
        {
            int sum = 0;
            for (int i = 0; i < inputArray.Length; i++)                 //O(N)
            {
                for (int j = 0; j < inputArray.Length; j++)             //O(N)
                {
                    for (int k = 0; k < inputArray.Length; k++)         //O(N)
                    {
                        int y = 0;

                        if (j != 0)
                        {
                            y = k / j;                                  //O(1)
                        }

                        sum += inputArray[i] + i + k + j + y;           //O(1)
                    }
                }
            }

            return sum;                                                 //O(N^3) общая сложность
        }

        public static int FibonacciRec(int number)
        {
            if (number == 0) return 0;
            if (number == 1) return 1;

            return FibonacciRec(number-2)+FibonacciRec(number-1);
        }

        public static int Fibonacci(int number)
        {
            if (number == 0) return 0;
            if (number == 1) return 1;

            int[] fib = new int[number];
            fib[0] = 0;
            fib[1] = 1;

            for (int i = 2; i < number; i++)
            {
                fib[i] = fib[i - 2] + fib[i - 1];
            }

            return fib[number - 2] + fib[number - 1];
        }


        static void Testing(TestCase test)
        {
            try
            {
                string result = IsSimple(test.Argument);
                string expect = (result==test.Expected)?"VALID RESULT":"INVALID RESULT";
                Console.Write(test.Argument+": "+ result+" "+expect);
            } catch (Exception ex)
            {
                if ((test.ExpectedException!=null)&&(test.ExpectedException==ex))
                {
                    Console.Write("VALID EXCEPTION");
                }
                else { Console.Write("INVALID EXCEPTION"); }
            }
            Console.WriteLine();
        }

        static void TestingFib(TestCase test)
        {
            try
            {
                string result1 = Convert.ToString(Fibonacci(test.Argument));
                string result2 = Convert.ToString(FibonacciRec(test.Argument));
                string expect = ((result1 == test.Expected)&&(result2 == test.Expected)) ? "VALID RESULT" : "INVALID RESULT";
                Console.Write(test.Argument + ": " + result1+" "+result2 + " " + expect);
            }
            catch (Exception ex)
            {
                Console.Write(test.Argument + ": ");
                if ((test.ExpectedException != null) && (test.ExpectedException == ex))
                {
                    Console.Write("VALID EXCEPTION");
                }
                else { Console.Write("INVALID EXCEPTION " + ex.GetType()); }
            }
            Console.WriteLine();
        }


        static void Main(string[] args)
        {                   
            TestCase[] testarray = new TestCase[7];
            testarray[0] = new TestCase(); testarray[0].Argument = -5; testarray[0].Expected = "не простое"; testarray[0].ExpectedException = null;
            testarray[1] = new TestCase(); testarray[1].Argument = 0; testarray[1].Expected = "простое"; testarray[0].ExpectedException = null;
            testarray[2] = new TestCase(); testarray[2].Argument = 3; testarray[2].Expected = "простое"; testarray[0].ExpectedException = null;
            testarray[3] = new TestCase(); testarray[3].Argument = 7; testarray[3].Expected = "простое"; testarray[0].ExpectedException = null;
            testarray[4] = new TestCase(); testarray[4].Argument = 9; testarray[4].Expected = "не простое"; testarray[0].ExpectedException = null;
            testarray[5] = new TestCase(); testarray[5].Argument = 15000000; testarray[5].Expected = "не простое"; testarray[0].ExpectedException = null;
            testarray[6] = new TestCase(); testarray[6].ExpectedException = new NullReferenceException();
            foreach (var item in testarray)
            {
                Testing(item);
            }


            Console.WriteLine();
            Console.WriteLine("Fibonacci");
            TestCase n0 = new TestCase(); n0.Argument = 0; n0.Expected = Convert.ToString(0); TestingFib(n0);
            n0 = new TestCase(); n0.Argument = 1; n0.Expected = Convert.ToString(1); TestingFib(n0);
            n0 = new TestCase(); n0.Argument = 2; n0.Expected = Convert.ToString(1); TestingFib(n0);
            n0 = new TestCase(); n0.Argument = 3; n0.Expected = Convert.ToString(2); TestingFib(n0);
            n0 = new TestCase(); n0.Argument = 4; n0.Expected = Convert.ToString(3); TestingFib(n0);
            n0 = new TestCase(); n0.Argument = 5; n0.Expected = Convert.ToString(5); TestingFib(n0);
            n0 = new TestCase(); n0.Argument = 6; n0.Expected = "восемь"; TestingFib(n0);
            n0 = new TestCase(); n0.Argument = -6; TestingFib(n0); n0.ExpectedException = new ArithmeticException();  TestingFib(n0);



        }
    }
}
