using System;
namespace MyLib
{
    public static class MoRongChoLopDouble
    {
        public static double BinhPhuong(this double x)
        {
            return x * x;
        }
        public static double CanBacHai(this double x) => Math.Sqrt(x);
        public static double TinhSin(this double x) => Math.Sin(x);
        public static double TinhCos(this double x) => Math.Cos(x);


    }
}
