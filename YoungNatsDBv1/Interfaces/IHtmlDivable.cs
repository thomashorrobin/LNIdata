using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.Interfaces
{
    public interface IHtmlDivable
    {
         string GetHtml(string[] elementsToExclude);
    }
}
