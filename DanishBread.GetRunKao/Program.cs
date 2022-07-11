using DanishBread.GetRunKao.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace DanishBread.GetRunKao
{
    class Program
    {        
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(Path.Combine("Config", "Mail.json"), false, false);
            IConfigurationRoot config = configurationBuilder.Build();

            var services = new ServiceCollection();
            services.AddSingleton<IAngleSharpService, AngleSharpService>();
            services.AddSingleton<IRunkaoService, RunkaoService>();
            services.AddSingleton<MailHelper>();
            services.AddOptions().Configure<MailConfig>(x => config.GetSection("Config").Bind(x));

            using var serviceProvider = services.BuildServiceProvider();
            var runkaoService = serviceProvider.GetService<IRunkaoService>();
            var mailHelper = serviceProvider.GetService<MailHelper>();

            var index = 1;
            var year = "2022";
            var times = "上";

            while (true)
            {
                var flag = await runkaoService.CheckScore(year, times);

                Console.WriteLine($"检测第{index}次，查询结果是否已出：{(flag ? "是" : "否")}");

                if (flag)
                {
                    mailHelper.SendMail($"{year}年{times}半年软考成绩已出....开始查询成绩");
                    break;
                }

                await Task.Delay(60 * 1000);
                index++;
            }

            //var provinceName = "广东";
            //while (true)
            //{
            //    var flag = await runkaoService.CheckSignIn(provinceName);

            //    Console.WriteLine($"检测第{index}次，{provinceName}是否可以报名？：{(flag ? "是" : "否")}");

            //    if (flag)
            //    {
            //        mailHelper.SendMail($"软考报名：{provinceName}已经可以开始报名了....");
            //        break;
            //    }
            //    await Task.Delay(60 * 1000);
            //    index++;
            //}
        }
    }
}
