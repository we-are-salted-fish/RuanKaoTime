using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanishBread.GetRunKao
{
    public class AngleSharpService : IAngleSharpService
    {
        public async Task<IHtmlCollection<IElement>> QuerySelectorAll(string url, string selectors)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = url;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var nodeSelector = selectors;
            var nodes = document.QuerySelectorAll(nodeSelector);

            return nodes;
        }
    }
}
