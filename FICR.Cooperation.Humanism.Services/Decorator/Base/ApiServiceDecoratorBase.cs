using FICR.Cooperation.Humanism.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FICR.Cooperation.Humanism.Services.Decorator.Base
{
    public abstract class ApiServiceDecorator : IApiService
    {
        protected readonly IApiService _decoratedService;

        protected ApiServiceDecorator(IApiService decoratedService)
        {
            _decoratedService = decoratedService;
        }
        public virtual Task CallApiAsync(string endpoint, object parameters)
        {
            throw new NotImplementedException();
        }
    }


}
