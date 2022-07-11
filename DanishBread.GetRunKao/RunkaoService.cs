using DanishBread.GetRunKao.Config;
using DanishBread.GetRunKao.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanishBread.GetRunKao
{
    public class RunkaoService : IRunkaoService
    {
        private readonly IAngleSharpService _angleSharpService;

        public RunkaoService(IAngleSharpService angleSharpService)
        {
            this._angleSharpService = angleSharpService;
        }

        public async Task<bool> CheckScore(string year, string times)
        {
            var result = false;
            var selects = await _angleSharpService.QuerySelectorAll("https://query.ruankao.org.cn/score", "body > div.main > div > div:nth-child(1) > div.contentIn > div.item.one > div > ul");

            var years = selects.Select(x => x.TextContent);

            if (years.Any(x => x.StartsWith(year) && x.Contains(times)))
            {
                result = true;
            }

            return result;
        }

        public async Task<bool> CheckSignIn(string provinceName)
        {
            var result = false;
            var cells = await _angleSharpService.QuerySelectorAll("https://bm.ruankao.org.cn/sign/welcome", "div.selectorg");

            var timeList = new List<TimeList>();

            foreach (var cell in cells)
            {
                var ProvinceName = cell.ChildNodes[0].TextContent;
                var TimeRange = cell.ChildNodes[1].TextContent;

                timeList.Add(new TimeList { ProvinceName = ProvinceName, TimeRange = TimeRange });
            }

            //var index = 1;
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //Console.WriteLine("-省份\t\t 时间");
            //foreach (var item in timeList)
            //{
            //    if (item.ProvinceName.Length > 2)
            //    {
            //        Console.WriteLine($"{index}.{item.ProvinceName}\t{item.TimeRange}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{index}.{item.ProvinceName}\t\t{item.TimeRange}");
            //    }
            //    index++;
            //}

            if (timeList.Select(p => p.ProvinceName).Contains(provinceName))
            {
                result = true;
            }

            return result;
        }
    }
}
