using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Models.View;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Managers
{
    public class UserProfileManager : Core.Web.Security.Facades.Manager.IUserProfileManagerFacade
    {
        private readonly IEntityRepository<User, string> userRepository;

        public UserProfileManager(IEntityRepository<User, string> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IUserProfileViewModel GetUserProfile(string username)
        {
            var user = userRepository.Get(username);

            return new Models.View.UserProfileViewModel();
        }

        public void UpdateUserProfile(string username, IUserProfileViewModel profileModel)
        {
        }
    }
}
