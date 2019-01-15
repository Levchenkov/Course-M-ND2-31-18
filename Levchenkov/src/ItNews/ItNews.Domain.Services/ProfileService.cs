using System.Linq;
using AutoMapper;
using ItNews.Common.Contracts;
using ItNews.Data.Contracts;
using ItNews.Domain.Contracts;
using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IDbScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public ProfileService(IDbScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public ProfileViewModel GetProfile(long profileId)
        {
            throw new System.NotImplementedException();
        }

        public ShortProfileViewModel GetShortProfile(long profileId)
        {
            using (var scope = scopeFactory.GetSharedScope())
            {
                var profile = scope.CreateQuery<Data.Contracts.Entities.Profile>().FirstOrDefault(x => x.Id == profileId);
                if (profile == null)
                {
                    throw new BusinessException("Profile not found.");
                }

                var result = mapper.Map<ShortProfileViewModel>(profile);
                return result;
            }
        }
    }
}