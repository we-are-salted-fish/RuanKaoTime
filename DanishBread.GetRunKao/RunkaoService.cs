using System;
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

            if(years.Any(x=>x.StartsWith(year) && x.Contains(times)))
            {
                result = true;
            }

            return result;
        }

        public Task<bool> CheckSignIn(string provinceName)
        {
            throw new NotImplementedException();
        }
    }
}
