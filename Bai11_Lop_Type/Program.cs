using System.Reflection;

namespace Lop_type
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    class Program
    {
        static void Main(string[] args) 
        {
            int[] a = new int[] { 1, 2, 3, 9, 10 };
            Type t1 = a.GetType();    // ~ typeof(Arr)
            Console.WriteLine(t1.FullName);

            Console.WriteLine("-------------Cac thuoc tinh cua Arr-----------");
            t1.GetProperties().ToList().ForEach((PropertyInfo p) => Console.WriteLine(p.Name));

            Console.WriteLine("============================");

            User user1 = new User() { 
                                      Name = "Dao Tien Dung",
                                      Age = 22,
                                      PhoneNumber = "0326872975",
                                      Email = "daotiendung0607@gmail.com"
                                     };

            PropertyInfo[] properties = user1.GetType().GetProperties();

            Console.WriteLine("Cac thuoc tinh cua doi tuong user1 cua lop User:");
            foreach (var property in properties)
            {
                string name = property.Name;             //Lay ten thuoc tinh
                var value = property.GetValue(user1);    //Lay gia tri cua thuoc tinh
                Console.WriteLine($"Thuoc tinh {name} co gia tri la: {value}");
            }

            properties[0].SetValue(user1, "Do Thi Van");
            Console.WriteLine(user1.Name);
        }
    }
}