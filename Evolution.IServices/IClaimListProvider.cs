using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Evolution.Application
{
    public interface IClaimListProvider
    {
        List<Claim> Find(string userId);
        void Add(string userId, Claim claim);
    }
}
