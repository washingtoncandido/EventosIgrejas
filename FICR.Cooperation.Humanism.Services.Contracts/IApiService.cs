using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FICR.Cooperation.Humanism.Services.Contracts
{
   public interface  IApiService
    {
        Task CallApiAsync(string endpoint, object parameters);
    }
}
