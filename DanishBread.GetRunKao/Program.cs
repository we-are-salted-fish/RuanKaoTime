using AngleSharp;
using DanishBread.GetRunKao.Config;
using Microsoft.Extensions.DependencyInjection;
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

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Init();

            var services = new ServiceCollection();
            services.AddSingleton<IAngleSharpService, AngleSharpService>();
            services.AddSingleton<IRunkaoService, RunkaoService>();

            using var serviceProvider = services.BuildServiceProvider();
            var runkaoService = serviceProvider.GetService<IRunkaoService>();

            var index = 1;
            var year = "2022";
            var times = "上";

            while (true)
            {
                var flag = await runkaoService.CheckScore(year, times);

                Console.WriteLine($"检测第{index}次，查询结果是否已出：{(flag ? "是" : "否")}");

                if (flag)
                {
                    MailHelper.SendMail(_mailConfig, $"{year}年{times}半年软考成绩已出....开始查询成绩");
                    break;
                }

                await Task.Delay(60 * 1000);
                index++;
            }

            //var r = GetHtmlString();
            //Console.WriteLine(r);
            //MailHelper.SendMail(_mailConfig, "广东的软考报名时间出来了。");
            //var flag = true;
            //var loopIndex = 1;
            //do
            //{
            //    Console.WriteLine(loopIndex);
            //    flag = SignUp("广东");
            //    loopIndex++;
            //    Thread.Sleep(60 * 1000);
            //} while (flag);
                   

            //Console.ReadKey();
        }

        static bool SignUp(string provinceName)
        {
            var flag = true;
            var rList = Analysis().Result;

            var index = 1;
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("-省份\t\t 时间");
            foreach (var item in rList)
            {
                if (item.ProvinceName.Length > 2)
                {
                    Console.WriteLine($"{index}.{item.ProvinceName}\t{item.TimeRange}");
                }
                else
                {
                    Console.WriteLine($"{index}.{item.ProvinceName}\t\t{item.TimeRange}");
                }

                index++;
            }

            if (rList.Select(p => p.ProvinceName).Contains(provinceName))
            {
                MailHelper.SendMail(_mailConfig, "广东的软考报名时间出来了。");
                flag = false;
            }

            Console.WriteLine();

            return flag;
        }
    }

    public class TimeList
    {
        public string ProvinceName { get; set; }

        public string TimeRange { get; set; }
    }
}
