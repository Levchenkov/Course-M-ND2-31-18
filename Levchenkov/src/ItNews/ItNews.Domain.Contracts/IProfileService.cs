using ItNews.Domain.Contracts.ViewModels;

namespace ItNews.Domain.Contracts
{
    public interface IProfileService
    {
        ProfileViewModel GetProfile(long profileId);

        ShortProfileViewModel GetShortProfile(long profileId);
    }
}