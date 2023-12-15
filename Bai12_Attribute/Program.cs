using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Attribute_trong_C_sharp
{
    class Program
    {
        [Mota("Lop chua thong tin ve nguoi dung")]
        class User
        {
            [Mota("Ten nguoi dung")]
            public string Name { get; set; }

            [Mota("Tuoi nguoi dung")]
            public int Age { get; set; }
            [Mota("So dien thoai nguoi dung")]
            public string PhoneNumber { get; set; }

            [Mota("Dia chi email nguoi dung")]
            public string Email { get; set; }

            //Attribute
            [Obsolete("Phuong thuc nay khong nen su dung nua")]
            public void PrintInfor()
            {
                Console.WriteLine(Name);
            }
        }

        //Tao Attribute mo ta 1 doi tuong

        //Chi ra thuoc tinh chu thich Mota ap dung duoc cho thanh phan nao

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
        public class MotaAttribute : Attribute
        {
            public string ThongTin { get; set; }
            public MotaAttribute(string _thongTin)
            {
                ThongTin= _thongTin;
            }
        }

        //Vi du su dung cac Attribute co san
        public class Employer
        {
            [Required(ErrorMessage = "Employee {0} is required")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Ten tu 3 den 100 ky tu")]
            [DataType(DataType.Text)]
            public string Name { get; set; }

            [Range(18, 99, ErrorMessage = "Age should be between 18 and 99")]
            public int Age { get; set; }


            [DataType(DataType.PhoneNumber)]
            [Phone]
            public string PhoneNumber { set; get; }

            [DataType(DataType.EmailAddress)]
            [EmailAddress]
            public string Email { get; set; }

        }

        static void Main(string[] args) 
        {
            User user1 = new User()
            {
                Name = "Dao Tien Dung",
                Age = 22,
                PhoneNumber = "0326872975",
                Email = "daotiendung0607@gmail.com"
            };

            user1.PrintInfor();

            //Doc Atrtribute cua lop
            foreach (var attri in user1.GetType().GetCustomAttributes(false))
            {
                MotaAttribute mota = attri as MotaAttribute;
                if (mota != null)
                {
                    string name = user1.GetType().Name;
                    Console.WriteLine($"{name} - {mota.ThongTin}");
                }
            }

            //Doc Attribute cac thuoc tinh cua lop

            PropertyInfo[] properties = user1.GetType().GetProperties();
            foreach (PropertyInfo property in properties) 
            {
                foreach (var attri in property.GetCustomAttributes(false))
                {
                    //Neu doi tuong attri thuoc lop Mota thi moi in
                    MotaAttribute mota = attri as MotaAttribute;
                    if (mota != null)
                    {
                        var name_property = property.Name;
                        var value_property = property.GetValue(user1);
                        Console.WriteLine($"{name_property} - {mota.ThongTin}: {value_property}");
                    }
                }
            }
            Console.WriteLine("=================================");

            //Vi du kiem tra du lieu co phu hop thiet lap boi Attribute cua lop Employer
            Employer employer1 = new Employer();
            employer1.Name = "XT";
            employer1.Age = 6;
            employer1.PhoneNumber = "1234as";
            employer1.Email = "test@re";

            ValidationContext context = new ValidationContext(employer1);

            // results - danh sach cac ValidationResult, ket qua kiem tra
            List<ValidationResult> results = new List<ValidationResult>();

            //Thuc hien kiem tra du lieu
            bool valid = Validator.TryValidateObject(employer1, context, results, true);

            if (!valid)
            {
                foreach (ValidationResult result in results)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{result.MemberNames.First(),13}");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"    {result.ErrorMessage}");
                    Console.ResetColor();
                }
            }   
        }
    }
}