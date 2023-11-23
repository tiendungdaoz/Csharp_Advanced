using MyLib;
using System;
using System.Linq;

namespace Extension_method
{
    //Lop static ~ tat ca cac phuong thuc trong lop la phuong thuc static
    static class Class1
    {
        //Phuong thuc mo rong cho lop string
        //Tham so dau tien la kieu string (Lop ma phuong thuc mo rong se them vao)
        // Them tu khoa this - cho biet mo rong lop string voi phuong thuc nay
        public static void Print(this string s, ConsoleColor color)     
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            string str = "Xin chao cac ban";

            //Goi phuong thuc mo rong thong qua doi tuong cua lop
            str.Print(ConsoleColor.DarkCyan);

            //Khong the thuc hien duoc nua,
            //Print(str, ConsoleColor.Green);
            //Print(str, ConsoleColor.Red);
            Console.ResetColor();

            //Vi du su dung cac phuong thuc mo rong cho lop Double
            double a = 2.5;
            Console.WriteLine($"Gia tri binh phuong, can ban 2, sin, cos cua {a} lan luot la: " +
                $" {a.BinhPhuong()}, {a.CanBacHai()}, {a.TinhSin()}, {a.TinhCos()}");
        }
    }
}