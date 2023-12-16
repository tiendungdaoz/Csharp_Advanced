using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Dependency_injection
{
    interface IClassB
    {
        public void ActionB();
    }
    interface IClassC
    { 
        public void ActionC(); 
    }
    class ClassC : IClassC
    {
        public void ActionC() => Console.WriteLine("Action in ClassC");
    }

    class ClassB : IClassB
    {
        // Phụ thuộc của ClassB là ClassC
        IClassC c_dependency;

        public ClassB(IClassC classc) => c_dependency = classc;
        public void ActionB()
        {
            Console.WriteLine("Action in ClassB");
            c_dependency.ActionC();
        }
    }

    class ClassA
    {
        // Phụ thuộc của ClassA là ClassB
        IClassB b_dependency;

        public ClassA(IClassB classb) => b_dependency = classb;
        public void ActionA()
        {
            Console.WriteLine("Action in ClassA");
            b_dependency.ActionB();
        }
    }
    class ClassC1 : IClassC
    {
        public ClassC1() => Console.WriteLine("ClassC1 is created");
        public void ActionC()
        {
            Console.WriteLine("Action in C1");
        }
    }

    class ClassB1 : IClassB
    {
        IClassC c_dependency;
        public ClassB1(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB1 is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in B1");
            c_dependency.ActionC();
        }
    }
    class ClassB2 : IClassB
    {
        IClassC c_dependency;
        string message;
        public ClassB2(IClassC classc, string mgs)
        {
            c_dependency = classc;
            message = mgs;
            Console.WriteLine("ClassB2 is created");
        }
        public void ActionB()
        {
            Console.WriteLine(message);
            c_dependency.ActionC();
        }
    }

    //Cau hinh inject du lieu cho dich vu voi IOptions

    //MyServiceOptions chua cac thiet lap
    public class MyServiceOptions
    {
        public string data1 { get; set; }
        public int data2 { get; set; }
    }

    //Cau hinh inject du lieu cho dich vu voi IOptions
    public class MyService
    {
        public string data1 { get; set; }
        public int data2 { get; set; }

        // Tham số khởi tạo là IOptions, các tham số khởi tạo khác nếu có khai báo như bình thường
        public MyService(IOptions<MyServiceOptions> options)
        {
            // Đọc được MyServiceOptions từ IOptions
            MyServiceOptions opts = options.Value;
            data1 = opts.data1;
            data2 = opts.data2;
        }
        public void PrintData() => Console.WriteLine($"{data1} / {data2}");
    }
    class Program
    {
        public static ClassB2 CreateB2Factory(IServiceProvider provider)
        {
            var b2 = new ClassB2(provider.GetService<IClassC>(), "Thuc hien trong ClassB2");
            return b2;
        }
        static void Main(string[] args) 
        {
            //IClassC objectC = new ClassC1();            // new ClassC();
            //IClassB objectB = new ClassB1(objectC);     // new ClassB();
            //ClassA objectA = new ClassA(objectB);

            //objectA.ActionA();

            var services = new ServiceCollection();

            //Dang ky cac dich vu
            //ClassA
            //IClassB co trien khai ClassB, ClassB1, ClassB2
            //ClassB2 ham khoi tao co 2 tham so la IClassC va string mgs  => Su dung Delegate de dang ky dich vu
            //IClassC co trien khai ClassC, ClassC1

            #region Su dung rieng le cac loai dich vu
            //Dang ki dich cu loai AddSingleton
            //services.AddSingleton<IClassC,ClassC1>();

            //Dang ki dich vu loai Transient
            //services.AddTransient<IClassC, ClassC1>();

            //Dang ky dich vu loai Scoped
            services.AddScoped<IClassC, ClassC1>();


            //Phat sinh doi tuong ServiceProvider
            var serviceProvider = services.BuildServiceProvider();


            //Lay ra cac doi tuong dang ky trong ServiceCollection

            //Trong Scope toan cuc
            for (int i = 0; i < 5; i++)
            {
                IClassC classC = serviceProvider.GetService<IClassC>();

                Console.WriteLine(classC.GetHashCode());
            }

            //Trong Scope cuc bo
            using(var scoped1 = serviceProvider.CreateScope())
            {
                var serviceProvider1 = services.BuildServiceProvider();
                for (int i = 0; i < 5; i++)
                {
                    IClassC classC1 = serviceProvider1.GetService<IClassC>();

                    Console.WriteLine(classC1.GetHashCode());
                }
            }
            #endregion
            Console.WriteLine();

            #region Su dung delegate/factory dang ky dich vu
            // var services = new ServiceCollection();

            services.AddSingleton<ClassA, ClassA>();

            //services.AddSingleton<IClassB, ClassB1>();

            //Dang ky bang cach su dung delegate
            //services.AddSingleton<IClassB, ClassB2>((IServiceProvider provider) => 
            //                                                                       {
            //                                                                           var b2 = new ClassB2(provider.GetService<IClassC>(),
            //                                                                               "Thuc hien trong ClassB2");
            //                                                                           return b2;
            //                                                                       });

            //Co the dang ky bang Factoty
            services.AddSingleton<IClassB, ClassB2>(CreateB2Factory);

            services.AddSingleton<IClassC, ClassC1>();

            var serviceProvider2 = services.BuildServiceProvider();

            ClassA classA1 =  serviceProvider2.GetService<ClassA>();
            classA1.ActionA();
            #endregion
            Console.WriteLine();

            #region     Dang ky 1 lop cau hinh MyServiceOptions vao ServiceCollection
            services.AddSingleton<MyService>();
            services.Configure<MyServiceOptions>((MyServiceOptions options) =>
                                                                                {
                                                                                    options.data1 = "Dao Tien Dung";
                                                                                    options.data2 = 100;
                                                                                });

            MyService objMyService = services.BuildServiceProvider().GetService<MyService>();
            objMyService.PrintData();
            #endregion
            Console.WriteLine();

            #region Cau hinh tu file xml, json, ini cho DI Container

            //Su dung lop ConfigurationBuilder
            ConfigurationBuilder config = new ConfigurationBuilder();

            config.SetBasePath("C: \\Users\\Admin\\Desktop\\Tài li ? u h ? c l ? p trình\\C# Co b?n d?n Nâng cao\\Csharp_Advanced\\Bai13_Dependency_injection\\bin\\Debug\\net6.0\\cauhinh.json");
            config.AddJsonFile("cauhinh.json");

            IConfigurationRoot configroot = config.Build();

            //var key1 = configroot.GetSection("MyServiceOptions").GetSection("data1").Value;
            //Console.WriteLine(key1);

            services.AddOptions();
            services.Configure<MyServiceOptions>(configroot.GetSection("MyServiceOptions"));
            services.AddSingleton<MyService>();

            var provider = services.BuildServiceProvider();

            var myservice = provider.GetService<MyService>();
            myservice.PrintData();
            #endregion
        }
    }
}