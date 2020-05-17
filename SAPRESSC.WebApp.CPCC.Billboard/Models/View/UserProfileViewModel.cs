using Microsoft.AspNetCore.Mvc;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Models.View;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Models.View
{
    [ModelMetadataType(typeof(IUserProfileViewModel))]
    public class UserProfileViewModel : IUserProfileViewModel
    {
        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
