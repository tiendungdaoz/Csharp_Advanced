﻿namespace Event_trong_Csharp
{
    class UserInput
    {
        public delegate void SuKienNhapSo(int x);

        //Event la delegate, nhung khi khai bao co them tu khoa event
        // => Bien chi co the thuc hien toan tu += hoac -=
        public event SuKienNhapSo sukiennhapso;

        //public event EventHandler sukiennhapso;   //~ delegate void EventHandler(object? sender, EventArgs e);

        public void Input()
        {
            do
            {
                Console.Write("Nhap vao 1 so nguyen: ");
                string s = Console.ReadLine();
                int i = Int32.Parse(s);

                //Goi phuong thuc trong delegate tuong duong voi viec phat di su kien
                sukiennhapso?.Invoke(i);
            } while (true);
        }
    }
    class TinhCanBac2
    {
        public void Sub(UserInput input)
        {
            input.sukiennhapso += TinhCan;
        }
        public void TinhCan(int x)
        {
            Console.WriteLine($"Can ban 2 cua {x} la: {Math.Sqrt(x)}");
        }
    }

    class TinhBinhPhuong
    {
        public void Sub(UserInput input)
        {
            input.sukiennhapso += BinhPhuong;
        }
        public void BinhPhuong(int x)
        {
            Console.WriteLine($"Binh phuong cua {x} la: {x*x}");
        }
    }


    class Program
    {
        static void Main(string[] args) 
        {
            //Publisher
            UserInput user1 = new UserInput();

            //Subsriber
            TinhCanBac2 tinhCan = new TinhCanBac2();
            TinhBinhPhuong binhPhuong = new TinhBinhPhuong();

            user1.sukiennhapso += (x) => 
            {
                Console.WriteLine("Ban vua nhap so: {0}", x);       //Gan event cho bieu thuc lambda phu hop
            };

            tinhCan.Sub(user1);
            binhPhuong.Sub(user1);

            user1.Input();
        }
    }
}