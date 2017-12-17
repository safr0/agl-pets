using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assignment.Interface
{
    public interface IHttpClient
    {
         Task<string> GetAsync(Uri requestUri);
    }
}
