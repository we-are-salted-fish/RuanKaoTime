using AngleSharp;
using DanishBread.GetRunKao.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DanishBread.GetRunKao
{
    class Program
    {
        private static MailConfig _mailConfig;
        private static string _baseDir;

        static void Init()
        {
            Type type = (new Program()).GetType();

            _baseDir = Path.GetDirectoryName(type.Assembly.Location);
            _mailConfig =
               NewLife.Serialization.JsonHelper.ToJsonEntity<MailConfig>(
                   File.ReadAllText(Path.Combine(_baseDir, "Config", "Mail.json")));
        }

        static async Task<List<TimeList>> Analysis()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://bm.ruankao.org.cn/sign/welcome";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var cellSelector = "div.selectorg";
            var cells = document.QuerySelectorAll(cellSelector);

            var timeList = new List<TimeList>();

            foreach (var cell in cells)
            {
                var ProvinceName = cell.ChildNodes[0].TextContent;
                var TimeRange = cell.ChildNodes[1].TextContent;

                timeList.Add(new TimeList { ProvinceName = ProvinceName, TimeRange = TimeRange });
            }

            return timeList;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Init();
            //var r = GetHtmlString();
            //Console.WriteLine(r);
            //MailHelper.SendMail(_mailConfig, "广东的软考报名时间出来了。");
            var flag = true;
            while (flag)
            {
                var rList = Analysis().Result;

                foreach (var item in rList)
                {
                    Console.WriteLine($"省份：{item.ProvinceName}   时间：{item.TimeRange}");
                }

                if (rList.Select(p => p.ProvinceName).Contains("广东"))
                {
                    MailHelper.SendMail(_mailConfig, "广东的软考报名时间出来了。");
                    flag = false;
                }

                Thread.Sleep(60 * 1000);
            }            

            Console.ReadKey();
        }


    }

    public class TimeList
    {
        public string ProvinceName { get; set; }

        public string TimeRange { get; set; }
    }
}
