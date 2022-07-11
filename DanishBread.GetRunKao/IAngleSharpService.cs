using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanishBread.GetRunKao
{
    public interface IAngleSharpService
    {
        public Task<IHtmlCollection<IElement>> QuerySelectorAll(string url, string selectors);
    }
}
