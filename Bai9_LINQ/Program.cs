using Microsoft.VisualBasic;
using System.Linq;

namespace LINQ_trong_Csharp
{
    //LINQ (Language Integrated Query): ngon ngu truy van tich hop
    //Nguon du lieu: mang, collection, generic; XML, SQL Sever

    //Xay dung tap du lieu
    public class Product
    {
        public int ID { set; get; }
        public string Name { set; get; }         // tên
        public double Price { set; get; }        // giá
        public string[] Colors { set; get; }     // các màu sắc
        public int Brand { set; get; }           // ID Nhãn hiệu, hãng
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
           => $"ID: {ID} | Name: {Name} | Price: {Price} | Brand: {Brand} | Color: {string.Join(",", Colors)}";
    }
    public class Brand
    {
        public string Name { set; get; }
        public int ID { set; get; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>()
            {
                new Brand{ID = 1, Name = "Công ty AAA"},
                new Brand{ID = 2, Name = "Công ty BBB"},
                new Brand{ID = 4, Name = "Công ty CCC"},
            };
            var products = new List<Product>()
            {
                new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            };

            //Lay san pham co gia 400
            Console.WriteLine("Cac san pham co gia = 400");
            var ketQua = from product in products
                         where product.Price == 400
                         select product;
            foreach (var sanPham in ketQua)
            {
                Console.WriteLine(sanPham.ToString());
            }
            Console.WriteLine();

            //1.@@API LINQ

            //Select

            var danhSachTen = products.Select((Product p) => {
                return new
                { Ten = p.Name,    //Kieu du lieu vo danh
                    Gia = p.Price
                };
            });
            Console.WriteLine("Danh sach ten va gia san pham: ");
            foreach (var ten in danhSachTen)
            {
                Console.WriteLine($"{ten}");
            }
            Console.WriteLine();

            //Where
            //Lay ra danh sach cac product co Brand = 3, luu vao kq2
            var kq2 = products.Where((Product p) =>
                                                    {
                                                        return p.Brand == 3;
                                                    });
            Console.WriteLine("Cac phan tu co Brand = 3: ");
            foreach (var phanTu in kq2)
            {
                Console.WriteLine($"{phanTu.ToString()}");
            }
            Console.WriteLine();

            //SelectMany: gan giong voi Select,
            //Doi voi truong du lieu ma no la 1 tap hop   p.Colors la 1 mang chuoi cac mau sac
            //SD SelectMany de lay ra tat ca cac mau sac 

            var kq3 = products.SelectMany((Product p) =>
                                                    {
                                                        return p.Colors;
                                                    });
            foreach (var phanTu in kq3)
            {
                Console.WriteLine(phanTu);
            }
            Console.WriteLine();

            //Min,max,sum,average
            int[] numbers = new int[] { 1, 2, 3, 10, 29 };
            Console.WriteLine(numbers.Max());
            Console.WriteLine(numbers.Min());
            Console.WriteLine(numbers.Sum());
            Console.WriteLine(numbers.Average());

            Console.WriteLine(numbers.Where((int number) =>
                                                            {
                                                                return number % 2 == 0;       //In so chan lon nhat
                                                            }).Max());
            Console.WriteLine();
            Console.WriteLine("San pham co gia nho nhat: {0}", products.Min((Product p) => p.Price));
            Console.WriteLine();

            //Joint: ket hop giua cac nguon du lieu de lay ra du lieu phu hop
            var kq4 = products.Join(brands, p => p.Brand, b => b.ID, (p, b) =>
                                                                               {
                                                                                   return new
                                                                                   {
                                                                                       Ten = p.Name,
                                                                                       CongTySx = b.Name
                                                                                   };
                                                                               });
            Console.WriteLine("Ten san pham va Cong ty sx:");
            foreach (var data in kq4)
            {
                Console.WriteLine(data);
            }
            Console.WriteLine();

            //GroupJoint: hd gan giong joint, tuy nhien phantu tra ve la 1 nhom dc nhom lai theo nguon du lieu ban dau
            var kq5 = brands.GroupJoin(products, b => b.ID, p => p.Brand, (b, groupProduct) =>
                                                                                {
                                                                                    return new {
                                                                                        CongTySx = b.Name,
                                                                                        CacSanPham = groupProduct
                                                                                    };
                                                                                });
            foreach (var data in kq5)
            {
                Console.WriteLine(data.CongTySx);
                foreach (var sanPham in data.CacSanPham)
                {
                    Console.WriteLine(sanPham);
                }
            }
            Console.WriteLine();

            //Take
            products.Take(4); //Lay ra 4 san pham dau tien

            //Skip
            products.Skip(2); //Bo qua 2 phan tu dau tien, lay ra cac san pham con lai

            //OrderBy (tang dan) OrderByDescending (giam dan)
            Console.WriteLine("Sap xep cac san pham tang dan ve gia");
            products.OrderBy(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            //Reverse: dao nguoc du lieu trong danh sach
            numbers.Reverse();

            //GroupBy: tra ve tap hop, moi phan tu trong tap hop la 1 nhom theo 1 du lieu nao do

            var kq6 = products.GroupBy(p => p.Price); // Nhom lai cac san pham co gia bang nhau thanh cac nhom

            Console.WriteLine("Chia nhom cac san pham theo gia:");
            foreach (var group in kq6)  //kq6: la 1 tap hop. Moi phan tu co key: p.price
                                        //Tuong ung voi moi key la 1 nhom cac san pham co cung gia                    
            {
                Console.WriteLine(group.Key);
                foreach (var sanPham in group)
                {
                    Console.WriteLine(sanPham);
                }
            }
            Console.WriteLine();

            //Distinct: Loai bo cac phan tu co cung gia tri:
            products.SelectMany(p => p.Colors).Distinct();  //Loai bo cac mau sac trung nhau

            //Single
            Product p = products.Single(p => p.Price == 600); //Tra ve 1 phan tu co gia bang 600.
            Console.WriteLine(p);                             //Neu khong tim thay, hoac co nhieu hon 1phan tu thoa man => bao loi
            Console.WriteLine();

            //SingleOrDefault: giong Single, khac o cho khong tim thay thi se tra ve null.

            //Any: tra ve true neu t/m DK logic nao do
            bool kiemTra = products.Any(p => p.Price == 600); // True

            //All: tra ve bool, kiem tra tat ca cac phan tu trong danh sach co t/m dk logic hay khong
            bool kiemTra2 = products.All(p => p.Price > 200);  //Flase

            //Count: Dem so luong phan tu trong danh sach
            var kq7 = products.Count(p => p.Price >= 300);  //Dem so phan tu co gia >= 300

            //@@2.Vidu tong hop

            //In ra ten san pham, ten CongtySX co gia (300 - 400)
            //KQ IN theo gia giam dan
            Console.WriteLine("Vi du tong hop:");
            var kq8 = products.Where(p => p.Price >= 300 && p.Price <= 400).OrderByDescending(p => p.Price);
            var kq9 = kq8.Join(brands, p => p.Brand, b => b.ID, (sanPham, congTy) =>
                                                                          {
                                                                              return new
                                                                              {
                                                                                  TenSanPham = sanPham.Name,
                                                                                  TenCongTy = congTy.Name,
                                                                                  Gia = sanPham.Price
                                                                              };
                                                                          });
            foreach (var item in kq9)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //Truy van LINGQ
            var kq10 = from product in products
                       select $"Name: {product.Name} | Price: {product.Price}";
            kq10.ToList().ForEach((name) => Console.WriteLine(name));
            Console.WriteLine();

            //Menh de where   Loc ra san pham co gia = 400
            var kq11 = from product in products
                       where product.Price == 400
                       select new {
                           Ten = product.Name,
                           Gia = product.Price
                       };
            kq11.ToList().ForEach((infor) => Console.WriteLine(infor));
            Console.WriteLine();

            //From ket hop   Loc ra san pham co gia = 400 va co mau xanh
            var kq12 = from product in products
                       from color in product.Colors
                       where product.Price == 400 && color == "Xanh"
                       select product;
            kq12.ToList().ForEach((infor) => Console.WriteLine(infor));
            Console.WriteLine();

            //Menh de orderby

            var kq13 = from product in products
                       from color in product.Colors
                       orderby product.Price descending
                       where product.Price >= 200 && color == "Xanh"
                       select product;
            kq13.ToList().ForEach((infor) => Console.WriteLine(infor));
            Console.WriteLine();

            //Menh de group ... by

            //Nhom san pham theo gia
            var kq14 = from product in products
                       group product by product.Price;
            kq14.ToList().ForEach((group) =>
                                            {
                                                Console.WriteLine(group.Key);
                                                group.ToList().ForEach((sanPham) => Console.WriteLine(sanPham));
                                            });
            Console.WriteLine();

            //Tu khoa into, luu group trong bien tam
            var kq15 = from product in products
                       group product by product.Price into gr
                       orderby gr.Key
                       select gr;
            kq15.ToList().ForEach((group) =>
            {
                Console.WriteLine(group.Key);
                group.ToList().ForEach((sanPham) => Console.WriteLine(sanPham));
            });
            Console.WriteLine();

            //Khai bao bien trong truy van voi tu khoa let
            var kq16 = from product in products
                       group product by product.Price into gr
                       orderby gr.Key descending
                       let count = gr.Count()
                       select new
                       {
                           Gia = gr.Key,
                           SoLuong = count,
                           CacSanPham = gr.ToList()
                       };
            kq16.ToList().ForEach((infor) =>
                                             {
                                                 Console.WriteLine($"Gia: {infor.Gia}");
                                                 Console.WriteLine($"So luong: {infor.SoLuong}");
                                                 infor.CacSanPham.ForEach((sanPham) => Console.WriteLine(sanPham));
                                             });
            Console.WriteLine();

            //Joint ket hop cac nguon du lieu

            //Inner join
            var kq17 = from product in products
                       join brand in brands on product.Brand equals brand.ID
                       select new { 
                                    Ten = product.Name,
                                    Gia = product.Price,
                                    CtySX = brand.Name
                                    };
            kq17.ToList().ForEach((infor) =>
            {
                Console.WriteLine($"Ten: {infor.Ten}");
                Console.WriteLine($"Gia: {infor.Gia}");
                Console.WriteLine($"Cong ty SX: {infor.CtySX}");
                Console.WriteLine();
            });

            //Ky thuat left join
            var kq18 = from product in products
                       join brand in brands on product.Brand equals brand.ID into t
                       from bra in t.DefaultIfEmpty()
                       select new
                       {
                           Ten = product.Name,
                           Gia = product.Price,
                           CtySX = (bra == null) ? "No brand" : bra.Name
                       };
            kq18.ToList().ForEach((infor) => Console.WriteLine($"{infor.Ten} || {infor.Gia} || {infor.CtySX}"));
        }
    }
}