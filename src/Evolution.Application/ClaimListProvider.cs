using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Evolution.Application
{
    public class ClaimListProvider : IClaimListProvider
    {
        private ConcurrentDictionary<string, Claim> data = new ConcurrentDictionary<string, System.Security.Claims.Claim>();

        void IClaimListProvider.Add(string userId, Claim claim)
        {
            data.AddOrUpdate(userId, claim, (key, oldValue) => claim);
        }


        List<Claim> IClaimListProvider.Find(string userId)
        {
            return data.Where(t => t.Key == userId).Select(t => t.Value).ToList();
        }
    }
}
