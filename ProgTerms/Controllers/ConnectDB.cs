using Microsoft.EntityFrameworkCore;
using ProgTerms.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgTerms.Controllers
{
    internal static class ConnectDB
    {
        public static ProgTermsContext ProgTermsContext { get; set; } = new ProgTermsContext();
        public static void Connect()
        {
            ProgTermsContext.Database.EnsureCreated();
            ProgTermsContext.Terms.Load();
        }
    }
}
