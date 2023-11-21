namespace Delegate_trong_Csharp
{
    //Khai bao kieu delegate ShowLog
    public delegate void ShowLog(string message);
    class Program
    {
        static void Infor(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;  //In mau xanh
            Console.WriteLine(s);
            Console.ResetColor();
        }

        static void Warning(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        static int Tong(int a, int b) => a + b;
        static int Hieu(int a, int b) => a - b;

        static void Tich(float a, float b, ShowLog log)
        {
            float kq = a * b;
            log?.Invoke($"Tich cua 2 so {a} va {b} la: {kq}");
        }
        static void Main(string[] args)
        {
            ShowLog log = null;
            log = Infor;  //log gan bang phuong thuc Infor

            //Thuc thi delegate
            log("Dao Tien Dung");
            log.Invoke("Alibaba");

            log = Warning;
            log.Invoke("Shizuka");
            Console.WriteLine();

            //Multicast 1 delegate
            log = Infor;
            log += Warning;
            log?.Invoke("Nobita");
            Console.WriteLine();

            //Action va Func
            Action action;                //~ delagate void Kieu_delegate();
            Action<string, int> action1;  //~ delagete void Kieu_delegate(string s, int i);

            //Khai bao bien delagate action2 co the gan cho Function Infor va Warning 
            Action<string> action2;
            action2 = Warning;
            action2 += Infor;
            action2.Invoke("Doraemon");

            Func<int> f1;                //~ delegate int Kieu_delegate();
            Func<int, string, bool> f2;  //~ delegate bool Kieu_delegate(int i, string s);

            //Khai bao bien delegate f3 co te gan cho Function Tong va Hieu
            Func<int, int, int> f3;
            f3 = Tong;
            Console.WriteLine("Ket qua: {0}", f3.Invoke(100, 9));
            Console.WriteLine();

            //Su dung delegate lam tham so cho ham
            Tich(100, 9, Infor);

        }
    }
}