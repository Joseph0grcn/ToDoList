using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security;
using System.ComponentModel.Design;
using System.IO;

using Newtonsoft.Json;
using System.Configuration;
using Microsoft.Win32.SafeHandles;
namespace todolist
{
    internal class Program
    {
        static String password = "admin999";
        static void Main(string[] args)
        {
            int choice = 0;
            printMenu();
            while (choice != -1)
            {
                Console.WriteLine("lutfen seciminizi giriniz");
                choice = Int16.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("0 girildi");
                        printMenu();
                        break;
                    case 1:
                        Console.WriteLine("1 girildi");
                        addItem();
                        break;
                    case 2:
                        Console.WriteLine("2 girildi");
                        printList();
                        break;

                    case 3:
                        Console.WriteLine("3 girildi");
                        clearItems();
                        Console.WriteLine("liste temizlendi");
                        break;
                    case 4:
                        Console.WriteLine("4 girildi");
                        replaceItem();
                        break;
                    case 5:
                        Console.WriteLine("5 girildi");
                        removeItem();
                        break;

                }
            }

        }

        private static void printMenu()
        {
            Console.WriteLine(
                            "*****menu*****\n" +
                            "0-menuyu yazdır\n" +
                            "1-görev ekle\n" +
                            "2-liste yazdır\n" +
                            "3-liste temizle\n"+
                            "4-görev düzenle\n" +
                            "5-görev kaldır\n" +
                            ""
                            );
        }
        public static void printList()
        {

            List<ListItem> liste = getList();
            Console.WriteLine("{0,-2} {1,-15} {2,-4} {3,-3} {4,-3} {5,-3} ", "No", "Kontent", "Önem", "TT" , "CT" , "%CT");
            foreach (var item in liste)
            {
                if (item.Visibility == true)
                {
                    Console.WriteLine("{0,-2} {1,-15} {2,-4} {3,-3} {4,-3} {5,-3} ",
                    item.Index,
                    item.Content,
                    item.ImportanceLevel,
                    item.TotalTaskCount,
                    item.CompletedTaskCount,
                    "%" + ((double)item.CompletedTaskCount / (double)item.TotalTaskCount * 100).ToString("F2"));
                }
            }
        }
        public static void addItem()
        {
            List<ListItem> liste = getList();

            int indexHolder = liste.Count + 1;

            Console.Write("Yapılacak etkinliği yazınız :");
            string ContentHolder = Console.ReadLine();

            Console.Write("etkinliğin önemini tam sayı ile yazınız :");
            int ImportanceLevelHolder = Int16.Parse(Console.ReadLine());

            Console.Write("etkinliğin uzunluğunu tam sayı ile yazınız :");
            int TotalTaskCountHolder = Int16.Parse(Console.ReadLine());

            Console.Write("etkinliğin tamamlanan kısmını tam sayı ile yazınız :");
            int CompletedTaskCountHolder = Int16.Parse(Console.ReadLine());
            var item = new ListItem
            {
                Index = indexHolder,
                Content = ContentHolder,
                ImportanceLevel = ImportanceLevelHolder,
                TotalTaskCount = TotalTaskCountHolder,
                CompletedTaskCount = CompletedTaskCountHolder,
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
            };
            if (liste == null )
            {
                liste.Insert(0,item);
            }
            else
            {
                liste.Add(item);
            }
            setList(liste);
        }
        public static void clearItems()
        {
            Console.WriteLine("lutfen sifrenizi giriniz");
            String sifre = Console.ReadLine();
            if (password == sifre)
            {
                List<ListItem> liste = getList();
                liste.Clear();
                setList(liste);
            }
        }
        public static void replaceItem()
        {
            printList();
            Console.WriteLine("değiştirmek istediğiniz görevin no değerini giriniz");
            int index = Int16.Parse(Console.ReadLine())-1;

            List<ListItem> liste =  getList();
            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("değiştirmek istediğiniz alanın numarasını giriniz\n" +
                "1-Kontent\n" +
                "2-önem\n" +
                "3-Totat Task\n" +
                "4-Completed Task\n" +
                "0-çıkış");

                Console.Write("degistirmek istediginiz alan -> ");  choice = Int16.Parse(Console.ReadLine());
                
               

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("kontent değiştiriliyor \n" +
                               "iptal etmek için esc tuşuna basınız\n" +
                               "devam etmek için enter tuşuna basınız");

                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.Enter)
                            {
                                Console.Write("degistirme onaylandı yeni degeri giriniz :");
                                liste[index].Content = Console.ReadLine();
                                liste[index].UpdateDateTime = DateTime.Now;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    case 2:
                        try
                        {
                            Console.WriteLine("önem değiştiriliyor \n" +
                               "iptal etmek için esc tuşuna basınız\n" +
                               "devam etmek için enter tuşuna basınız");

                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.Enter)
                            {
                                Console.Write("degistirme onaylandı yeni degeri giriniz :");
                                liste[index].ImportanceLevel = Int16.Parse(Console.ReadLine());
                                liste[index].UpdateDateTime = DateTime.Now;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    case 3:
                        try
                        {
                            Console.WriteLine("total task değiştiriliyor \n" +
                                "iptal etmek için esc tuşuna basınız\n" +
                                "devam etmek için enter tuşuna basınız");

                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.Enter)
                            {
                                Console.Write("degistirme onaylandı yeni degeri giriniz :");
                                liste[index].TotalTaskCount = Int16.Parse(Console.ReadLine());
                                liste[index].UpdateDateTime = DateTime.Now;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    case 4:
                        try
                        {
                            Console.WriteLine("completed task değiştiriliyor \n" +
                               "iptal etmek için esc tuşuna basınız\n" +
                               "devam etmek için enter tuşuna basınız");

                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            else if (keyInfo.Key == ConsoleKey.Enter)
                            {
                                Console.Write("degistirme onaylandı yeni degeri giriniz :");
                                liste[index].CompletedTaskCount = Int16.Parse(Console.ReadLine());
                                liste[index].UpdateDateTime = DateTime.Now;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                }
            }
            setList(liste);
        }

        public static void removeItem()
        {
            printList();
            Console.WriteLine("Kaldirmak istediginiz görev no degerini giriniz");
            int index = Int16.Parse(Console.ReadLine())-1;
            string fileName = "list.json";
            string jsonString = File.ReadAllText(fileName);
            List<ListItem> liste = JsonConvert.DeserializeObject<List<ListItem>>(jsonString);

            liste[index].Visibility = false;
            liste[index].UpdateDateTime = DateTime.Now;
        
            jsonString = JsonConvert.SerializeObject(liste);
            File.WriteAllText (fileName, jsonString);


        }
        public static List<ListItem> getList()
        {
            string fileName = "list.json";
            string jsonString = File.ReadAllText(fileName);
            List<ListItem> liste = JsonConvert.DeserializeObject<List<ListItem>>(jsonString);
            return liste;
        }

        public static void setList(List<ListItem> liste)
        {
            string fileName = "list.json";
            string jsonString = JsonConvert.SerializeObject(liste);
            File.WriteAllText(fileName, jsonString);
        }

    }
}
