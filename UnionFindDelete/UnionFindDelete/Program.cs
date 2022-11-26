using System;
using System.Collections.Generic;

namespace UnionFindDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            UnionSmallFindTest();
            DeleteSmallTest1();
            DeleteSmallTest2();
            DeleteTest1();
            DeleteTest2();
            DeleteTest3();
            DeleteTest4();
            DeleteTest5();
            DeleteTest6();
            FindTest1();
            FindTest2();
            FindTest3();
            FindTest4();
            RandomTest(1000);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void RandomTest(int count)
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            List<int> values = new List<int>();
            for (int i = 0; i < count; i++)
            {
                values.Add(i);
                tester.Make(i);
            }
            Random randomizer = new Random();
            while (0 < values.Count)
            {
                int op = randomizer.Next(100);
                int value = values[randomizer.Next(values.Count)];
                if (70 >= op)
                {
                    int value2 = values[randomizer.Next(values.Count)];
                    if ((value == value2) || (tester.IsSameTree(value, value2)))
                    {
                        continue;
                    }
                    Console.WriteLine(tester.ToString(tester.Union(value, value2).UFDValue));
                }
                else if (90 >= op)
                {
                    Console.WriteLine(tester.ToString(tester.Find(value).UFDValue));
                }
                else
                {
                    tester.Delete(value);
                    values.Remove(value);
                }
            }
        }

        private static void FindTest4()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            TestTreeBuilder builder = new TestTreeBuilder(tester);
            int root = builder.Build3(4, 1, 3);
            Console.WriteLine(tester.ToString(root));
            tester.Find(9);
            tester.Delete(5);
            tester.Find(14);
            tester.Find(13);
            tester.Find(12);
            tester.Find(11);
            tester.Union(builder.Build1(4), root);
            tester.Delete(11);
            tester.Union(builder.Build1(4), root);
            tester.Delete(6);
            tester.Union(builder.Build1(4), root);
            tester.Delete(8);
            tester.Union(builder.Build1(4), root);
            tester.Delete(7);
            tester.Union(builder.Build1(4), root);
            tester.Delete(4);
            tester.Union(builder.Build1(4), root);
            tester.Delete(3);
            tester.Union(builder.Build1(4), root);
            tester.Delete(2);
            tester.Union(builder.Build1(4), root);
            tester.Delete(1);
            Console.WriteLine(tester.ToString(root));
            var r1 = tester.Find(15);
            Console.WriteLine(tester.ToString(root));
        }

        private static void FindTest3()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            TestTreeBuilder builder = new TestTreeBuilder(tester);
            int root = builder.Build3(4, 1, 3);
            Console.WriteLine(tester.ToString(root));
            var r1 = tester.Find(23);
            Console.WriteLine(tester.ToString(root));
            var r2 = tester.Find(24);
            Console.WriteLine(tester.ToString(root));
        }

        private static void FindTest2()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            TestTreeBuilder builder = new TestTreeBuilder(tester);
            int root = builder.Build3(4, 1, 3);
            Console.WriteLine(tester.ToString(root));
            var r1 = tester.Find(23);
            Console.WriteLine(tester.ToString(root));
            var r2 = tester.Find(16);
            Console.WriteLine(tester.ToString(root));
        }

        private static void FindTest1()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildBigTestTree(tester);
            var r1 = tester.Find(9);
            Console.WriteLine(tester.ToString(0));
            var r2 = tester.Find(8);
            Console.WriteLine(tester.ToString(0));
            var r3 = tester.Find(6);
            Console.WriteLine(tester.ToString(0));
            var r4 = tester.Find(0);
            Console.WriteLine(tester.ToString(0));
        }

        private static void DeleteTest6()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildBigTestTree(tester);
            tester.Delete(10);
            Console.WriteLine(tester.ToString(0));
        }

        private static void DeleteTest5()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildBigTestTree(tester);
            tester.Delete(5);
            Console.WriteLine(tester.ToString(0));
            tester.Delete(11);
            Console.WriteLine(tester.ToString(0));
            tester.Delete(12);
            Console.WriteLine(tester.ToString(0));
        }

        private static void DeleteTest4()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildBigTestTree(tester);
            tester.Delete(0);
            Console.WriteLine(tester.ToString(6));
            tester.Delete(10);
            Console.WriteLine(tester.ToString(6));
        }

        private static void BuildBigTestTree(UnionFindDeleteTester<int> tester)
        {
            tester.Make(0);
            tester.Make(1);
            tester.Make(2);
            tester.Make(3);
            tester.Make(4);
            var r1 = tester.Union(1, 0);
            var r2 = tester.Union(2, 0);
            var r3 = tester.Union(3, 0);
            var r4 = tester.Union(4, 0);

            tester.Make(5);
            tester.Make(6);
            tester.Make(7);
            tester.Make(8);
            tester.Make(9);
            var r5 = tester.Union(6, 5);
            var r6 = tester.Union(7, 5);
            var r7 = tester.Union(8, 5);
            var r8 = tester.Union(9, 5);

            tester.Make(10);
            tester.Make(11);
            tester.Make(12);
            tester.Make(13);
            tester.Make(14);
            var r9 = tester.Union(11, 10);
            var r10 = tester.Union(12, 10);
            var r11 = tester.Union(13, 10);
            var r12 = tester.Union(14, 10);

            tester.Make(15);
            tester.Make(16);
            tester.Make(17);
            tester.Make(18);
            tester.Make(19);
            var r13 = tester.Union(16, 15);
            var r14 = tester.Union(17, 15);
            var r15 = tester.Union(18, 15);
            var r16 = tester.Union(19, 15);

            tester.Make(20);
            tester.Make(21);
            tester.Make(22);
            tester.Make(23);
            tester.Make(24);
            var r17 = tester.Union(21, 20);
            var r18 = tester.Union(22, 20);
            var r19 = tester.Union(23, 20);
            var r20 = tester.Union(24, 20);

            tester.Make(25);
            tester.Make(26);
            tester.Make(27);
            tester.Make(28);
            tester.Make(29);
            var r21 = tester.Union(26, 25);
            var r22 = tester.Union(27, 25);
            var r23 = tester.Union(28, 25);
            var r24 = tester.Union(29, 25);

            tester.Make(30);
            tester.Make(31);
            tester.Make(32);
            tester.Make(33);
            tester.Make(34);
            var r25 = tester.Union(31, 30);
            var r26 = tester.Union(32, 30);
            var r27 = tester.Union(33, 30);
            var r28 = tester.Union(34, 30);

            var rr1 = tester.Union(5, 0);
            var rr2 = tester.Union(10, 0);
            var rr3 = tester.Union(15, 0);
            var rr4 = tester.Union(20, 0);
            var rr5 = tester.Union(25, 0);
            var rr6 = tester.Union(30, 0);
            tester.Delete(4);
            tester.Delete(3);
            tester.Delete(2);
            tester.Delete(1);

            tester.Make(35);
            tester.Make(36);
            tester.Make(37);
            tester.Make(38);
            tester.Make(39);
            var rr7 = tester.Union(36, 35);
            var rr8 = tester.Union(37, 35);
            var rr9 = tester.Union(38, 35);
            var rr10 = tester.Union(39, 35);
            var rr11 = tester.Union(35, 0);
            Console.WriteLine(tester.ToString(7));
        }

        private static void DeleteTest3()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildDeleteTestTree(tester);
            tester.Delete(5);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(8);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(9);
            Console.WriteLine(tester.ToString(7));
        }

        private static void DeleteTest2()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildDeleteTestTree(tester);
            tester.Delete(7);
            Console.WriteLine(tester.ToString(9));
            tester.Delete(9);
            Console.WriteLine(tester.ToString(10));
            tester.Delete(0);
            Console.WriteLine(tester.ToString(10));
        }

        private static void DeleteTest1()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildDeleteTestTree(tester);
            tester.Delete(0);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(1);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(2);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(3);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(8);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(9);
            Console.WriteLine(tester.ToString(7));
            tester.Delete(7);
            Console.WriteLine(tester.ToString(10));
            tester.Delete(10);
            Console.WriteLine(tester.ToString(11));
            tester.Delete(11);
            Console.WriteLine(tester.ToString(14));
            tester.Delete(14);
            Console.WriteLine(tester.ToString(12));
            tester.Delete(12);
            Console.WriteLine(tester.ToString(5));
        }

        private static void BuildDeleteTestTree(UnionFindDeleteTester<int> tester)
        {
            tester.Make(0);
            tester.Make(5);
            tester.Make(3);
            tester.Make(2);
            tester.Make(12);
            var r1 = tester.Union(5, 0);
            var r2 = tester.Union(3, 0);
            var r3 = tester.Union(2, 0);
            var r4 = tester.Union(12, 0);

            tester.Make(1);
            tester.Make(8);
            tester.Make(6);
            tester.Make(4);
            tester.Make(13);
            var r5 = tester.Union(8, 1);
            var r6 = tester.Union(6, 1);
            var r7 = tester.Union(4, 1);
            var r8 = tester.Union(13, 1);

            tester.Make(7);
            tester.Make(9);
            tester.Make(10);
            tester.Make(11);
            tester.Make(14);
            var r9 = tester.Union(9, 7);
            var r10 = tester.Union(10, 7);
            var r11 = tester.Union(11, 7);
            var r12 = tester.Union(14, 7);

            var r13 = tester.Union(0, 7);
            var r14 = tester.Union(1, 7);
            Console.WriteLine(tester.ToString(7));
        }

        private static void DeleteSmallTest1()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildDeleteSmallTestTree(tester);
            tester.Delete(5);
            Console.WriteLine(tester.ToString(0));
            tester.Delete(0);
            Console.WriteLine(tester.ToString(3));
            tester.Delete(3);
        }

        private static void DeleteSmallTest2()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            BuildDeleteSmallTestTree(tester);
            tester.Delete(0);
            Console.WriteLine(tester.ToString(3));
            tester.Delete(5);
            Console.WriteLine(tester.ToString(3));
            tester.Delete(3);
        }

        private static void BuildDeleteSmallTestTree(UnionFindDeleteTester<int> tester)
        {
            tester.Make(0);
            tester.Make(5);
            tester.Make(3);
            var r1 = tester.Union(5, 0);
            var r2 = tester.Union(3, 0);
            Console.WriteLine(tester.ToString(0));
        }

        private static void UnionSmallFindTest()
        {
            UnionFindDeleteTester<int> tester = new UnionFindDeleteTester<int>();
            tester.Make(0);
            tester.Make(5);
            tester.Make(3);
            var r1 = tester.Union(5, 0);
            var r2 = tester.Union(3, 0);
            Console.WriteLine(tester.ToString(0));
            tester.Make(7);
            tester.Make(1);
            tester.Make(6);
            tester.Make(9);
            var r3 = tester.Union(9, 7);
            var r4 = tester.Union(6, 1);
            var r5 = tester.Union(1, 7);
            var r6 = tester.Union(0, 7);
            Console.WriteLine(tester.ToString(7));
            var r7 = tester.Find(5);
            Console.WriteLine(tester.ToString(7));
        }
    }
}
