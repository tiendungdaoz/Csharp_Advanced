using System;
using System.Numerics;

namespace Bai5
{
    //Thanh vien du lieu chi do Readonly
    class Student
    {
        public readonly string name;
        public Student(string _name)
        {
            this.name = _name;
        }
    }

    //Operator Overloading
    class Vector
    {
        public double x;
        public double y;
        public Vector(double _x, double _y) 
        {
            this.x = _x;
            this.y = _y;
        }
       
        public void Infor()
        {
            Console.WriteLine($"x= {x}, y = {y}");
        }

        //Dinh nghia phep cong tren 2 vector
        public static Vector operator+(Vector vec1, Vector vec2) 
        {
            return new Vector(vec1.x + vec2.x, vec1.y + vec2.y);
        }

        public static Vector operator +(Vector vec1, double value)
        {
            return new Vector(vec1.x + value, vec1.y + value);
        }

        //Indexer
        public double this[int index] 
        {
            get
            {
                switch (index)
                {
                    case 0:
                        {
                            return x;
                        }
                    case 1:
                        {
                            return y;
                        }
                    default:
                        throw new Exception("Chi so bi sai");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        {
                            x= value;
                            break;
                        }
                    case 1:
                        {
                            y= value; 
                            break;
                        }
                    default:
                        throw new Exception("Chi so bi sai");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args) 
        {
            Student stu1= new Student("Dao Tien Dung");
            //stu1.name = "Dao Tien Dung";    // Du lieu readonly chi doc, khong the gan gia tri
            Console.WriteLine(stu1.name);
            Console.WriteLine();

            Vector vec1 = new Vector(10, 9);
            Vector vec2 = new Vector(-12, 5);
            vec1.Infor();
            vec2.Infor();

            var vec3 = vec1 + vec2;
            var vec4 = vec1 + 10;
            vec3.Infor();
            vec4.Infor();
            Console.WriteLine();

            //Su dung indexer
            Vector vec5 = new Vector(1,1);
            //vec5[0] ~ truy cap den thuoc tinh x
            //vec5[1] ~ truy cap den thuoc tinh y
            Console.WriteLine($"Toa do cua vec5: x = {vec5[0]}, y = {vec5[1]}");
            vec5[0] = 5;
            vec5[1] = 6;
            vec5.Infor();
        }
    }
}