using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatDocument(this RazorPage page,int PersonType,string Document)
        {
            return PersonType == 1 ? Convert.ToUInt64(Document)
                .ToString(@"000\.000\.000\-00") :
                Convert.ToUInt64(Document)
                 .ToString(@"00\.000\.000\/0000\-00");
        }        
    }
}
