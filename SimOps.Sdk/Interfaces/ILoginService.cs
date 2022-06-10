using Refit;
using SimOps.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOps.Sdk.Interfaces
{
    public interface ILoginService
    {
        [Post("/login")]
        Task<LoginResult> LoginAsync([Body]LoginRequest request);
    }
}
