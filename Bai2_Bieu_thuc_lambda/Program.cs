namespace Bieu_thuc_lambda
{
    class Program
    {
        /*
         * Cu phap: (cac_tham_so) =>
         *          {
         *              //Cac cau lenh
         *              //Sd return neu co gia tri tra ve
         *          }
         */
        static void Main(string[] args)
        {
            //Can gan 1 bien delegate cho bieu thuc lambda
            Action<string> action1;
            action1 = (string s) => Console.WriteLine(s);  //~ delegate void Ten_kieu_delegate(string s);
                                                           //~ Action<string> ten_bien_delagate; 
            action1?.Invoke("Hello world");

            Action<string, string> action2;
            action2 = (string message, string name) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message + " " + name);
                Console.ResetColor();
            };
            action2?.Invoke("Xin chao", "Dao Tien Dung");
            Console.WriteLine();

            Func<int, int, int> tinhToan;
            tinhToan = (int a, int b) =>
            {
                int kq = a + b;
                return kq;
            };
            Console.WriteLine($"Tong cua 2 so la: {tinhToan?.Invoke(100, 1)}");

            //Su dung bieu thuc Lambda trong 1 so thu vien .NET
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 9 };

            var kq = arr.Select((int x) =>      //Phuong thuc Select nhan tham so dau vao la 1 bien delegate
            {
                return Math.Sqrt(x);
            });

            foreach (var result in kq)
            {
                Console.Write($"{result} ");
            }
            Console.WriteLine();

            arr.ToList().ForEach((int x) =>  //Phuong thuc ForEach nhan tham so dau vao la 1 bien delegate
            {
                if (x % 2 == 0)
                    Console.Write($"{x} ");
            });
            Console.WriteLine();

            var chia4 = arr.Where((int x) =>
            {
                return x % 4 == 0;
            });

            foreach(var item in chia4) 
            {
                Console.WriteLine(item);
            }
        }
    }
}
