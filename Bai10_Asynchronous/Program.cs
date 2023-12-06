using System.Threading;
using System.Threading.Tasks;

namespace _Asynchronous
{
    class Program
    {
        //Xay dung phuong thuc (Phuong thuc khong co kieu tra ve) bat dong bo vs asyns/await
        static async Task Task2()
        {
            Task task2 = new Task(() => { Dosomething(10, "T2", ConsoleColor.Green); });

            task2.Start();
            await task2;

            Console.WriteLine("Hoan thanh Task2");
        }

        static async Task Task3() 
        {
            Task task3 = new Task((Object obj) =>
            {
                string tenTacVu = obj.ToString();
                Dosomething(4, tenTacVu, ConsoleColor.Yellow);
            }, "T3");

            task3.Start();
            await task3;

            Console.WriteLine("Hoan thanh task3");
        }

        //Xay dung phuong thuc bat dong bo ma phuong thuc do co kieu tra ve

        static async Task<string> Task4()
        {
            Task<string> task4 = new Task<string>(() => {
                Dosomething(10, "T4", ConsoleColor.Green);
                return "Return from Task4";
            });
            task4.Start();

            var kq = await task4;
            Console.WriteLine(kq);
            return kq;
        }

        static async Task<string> Task5() 
        {
            Task<string> task5 = new Task<string>((object obj) => {
                string tenTacVu = obj.ToString();
                Dosomething(4, tenTacVu, ConsoleColor.Yellow);
                return "Return from Task5";
            }, "T5");
            task5.Start();

            var kq = await task5;
            Console.WriteLine(kq);
            return kq;
        }

        //Tao phuong thuc de tai 1 trang web
      

        static void Dosomething(int soLan, string message, ConsoleColor color)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{message} ... Start");
                Console.ResetColor();
            }

            for (int i = 0; i < soLan; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{message} {i}");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
            }

            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{message} ... End");
                Console.ResetColor();
            }
        }

        static void Main(string[] args) 
        {
            Console.WriteLine("======================");

            //Khai bao cac tac vu bat dong bo
            Task task2 = Task2(); 

            Task task3 = Task3();

            Task<string> task4 = Task4();

            Task<string> task5 = Task5();

            //Chay da luong
            //task2.Start();
            //taks3.Start();

            Dosomething(6, "T1", ConsoleColor.Magenta);

            //Bao dam tac vu task2 va task3 phai hoan thanh truoc khi in Press any key
            //task2.Wait();
            //taks3.Wait();

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}