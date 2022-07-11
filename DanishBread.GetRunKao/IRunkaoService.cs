using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanishBread.GetRunKao
{
    public interface IRunkaoService
    {
        public Task<bool> CheckSignIn(string provinceName);

        public Task<bool> CheckScore(string year, string times);
    }
}
