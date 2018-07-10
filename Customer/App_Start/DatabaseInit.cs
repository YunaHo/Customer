using Customer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Customer
{


    public class DatabaseInit
    {
        static Random random = new Random();

        public static void RemoveAll()
        {
            using (var context = new 客戶資料Entities())
            {
                context.客戶銀行資訊.RemoveRange(context.客戶銀行資訊);
                context.客戶聯絡人.RemoveRange(context.客戶聯絡人);
                context.客戶資料.RemoveRange(context.客戶資料);
                context.SaveChanges();
            }
        }

        public static void Init()
        {
            using (var context = new 客戶資料Entities())
            {
                if (!context.客戶資料.Any())
                {
                    var customers = Random客戶資料(50).ToArray();
                    var blanks = customers.SelectMany(c => Random銀行資訊(random.Next(1, 10), c.客戶名稱, c.Id)).ToArray();
                    var concats = customers.SelectMany(c => Random客戶聯絡人(random.Next(1, 10), c.客戶名稱, c.Id)).ToArray();

                    context.客戶資料.AddOrUpdate(customers);
                    context.客戶聯絡人.AddOrUpdate(concats);
                    context.客戶銀行資訊.AddOrUpdate(blanks);
                    context.SaveChanges();
                }
            }
        }

        private static IEnumerable<客戶資料> Random客戶資料(int count)
        {
            return (from number in Enumerable.Range(1, count)
                    select new 客戶資料()
                    {
                        Id = number,
                        客戶名稱 = $"Company{number.ToString("00")}",
                        統一編號 = RandomNumber(8),
                        電話 = $"0{random.Next(1, 9)}-{RandomNumber(9)}",
                        傳真 = $"0{random.Next(1, 9)}-{RandomNumber(9)}",
                        Email = $"Company{number}@Company{number}.com",
                        地址 = RandomAddress()
                    });
        }

        private static IEnumerable<客戶銀行資訊> Random銀行資訊(int count, string 公司名稱, int 公司Id)
        {
            return (from number in Enumerable.Range(1, count)
                    select new 客戶銀行資訊()
                    {
                        Id = number,
                        客戶Id = 公司Id,
                        銀行名稱 = $"銀行{number.ToString("00")}",
                        分行代碼 = number * number,
                        銀行代碼 = number * 3 + random.Next(1, 9),
                        帳戶名稱 = $"{公司名稱}帳戶名稱{number.ToString("00")}",
                        帳戶號碼 = RandomNumber(10)
                    });
        }

        private static IEnumerable<客戶聯絡人> Random客戶聯絡人(int count, string 公司名稱, int 公司Id)
        {
            return (from number in Enumerable.Range(1, count)
                    let name = Random姓名()
                    select new 客戶聯絡人()
                    {
                        Id = number,
                        客戶Id = 公司Id,
                        姓名 = name,
                        Email = $"{RandomEnglishName()}{number.ToString("00")}@{公司名稱}.com",
                        手機 = $"09{RandomNumber(2)}-{RandomNumber(6)}",
                        職稱 = Random職稱(),
                        電話 = $"0{random.Next(1, 9)}-{RandomNumber(9)}"
                    });
        }

        private static string RandomNumber(int length)
        {

            return string.Join("",
                Enumerable.Range(0, length)
                    .Select(_ => random.Next(0, 9)));
        }

        private static string Random姓名()
        {
            string[] lastNames = new[] { "王", "林", "簡", "吳", "施", "彭", "劉", "陳", "習", "歐" };
            string[] firstNames = new[] { "正恩", "招弟", "中正", "德華", "紹涵", "英文", "英九", "金平", "巴馬" };

            int lastNameIndex = random.Next(0, lastNames.Length - 1);
            int firstNameIndex = random.Next(0, firstNames.Length - 1);
            return $"{lastNames[lastNameIndex]}{firstNames[firstNameIndex]}";
        }

        private static string RandomEnglishName()
        {
            string[] lastNames = new[] { "Wang", "Shih", "Lui", "Huang", "Li", "Smith", "Jones", "Taylor", "Brown", "Davies", "Evans", "Wilson", "Thomas", "Johnson", "Roberts", "Thompson", "Wright", "Thompson", "Walker", "White" };
            string[] firstNames = new[] { "Patrick", "Yun", "Cathy", "Mark", "Mei", "Rabin", "Lisa", "Joy", "Ryan", "Wade", "Arisa", "Winnie", "James", "Carol", "Amity", "Amy", "Alisa", "Belle", "Betty", "Basia", "Claire", "Cherry", "Yanny" };

            int lastNameIndex = random.Next(0, lastNames.Length - 1);
            int firstNameIndex = random.Next(0, firstNames.Length - 1);
            return $"{firstNames[firstNameIndex]}_{lastNames[lastNameIndex]}";
        }

        private static string RandomAddress()
        {
            string[] areas = new[] { "台北市", "新北市", "台中市", "高雄市", "花蓮市", "雲林縣", "桃園市", "宜蘭縣", "台東縣" };
            string[] roads = new[] { "中山路", "中和路", "永和路", "信義路", "忠孝路", "仁愛路", "禮節路" };
            string[] steps = new[] { "", "一段", "二段", "三段", "四段" };

            int areaIndex = random.Next(0, areas.Length - 1);
            int roadIndex = random.Next(0, roads.Length - 1);
            int setpIndex = random.Next(0, steps.Length - 1);
            return
                $"{areas[areaIndex]}{roads[roadIndex]}{steps[setpIndex]}{RandomNumber(2)}號{RandomNumber(1)}F-{RandomNumber(2)}";
        }

        private static string Random職稱()
        {
            string[] name = new[] { "經理", "工程師", "業務", "客服", "接線生", "警衛" };

            int index = random.Next(0, name.Length - 1);

            return name[index];
        }

    }
}