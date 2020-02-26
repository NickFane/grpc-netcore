using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace ShopperProfileServer
{
    public class ShopperProfileService : ShopperProfile.ShopperProfileBase
    {
        private List<ShopperDetails> _shopperProfiles => new List<ShopperDetails>()
        {
            new ShopperDetails() { ShopperId = "1", Name = "Luke Skywalker" },
            new ShopperDetails() { ShopperId = "2", Name = "Ben Solo" },
            new ShopperDetails() { ShopperId = "3", Name = "Kylo Ren" },
            new ShopperDetails() { ShopperId = "4", Name = "Rey P." },
        };

        public ShopperProfileService()
        {

        }

        public override async Task<ShopperProfileResponse> GetShopperProfile(ShopperProfileRequest request, ServerCallContext context)
        {
            var shopperDetails = _shopperProfiles.Single(s => s.ShopperId == request.ShopperId);
            return new ShopperProfileResponse() { ShopperDetails = shopperDetails };
        }

    }
}
