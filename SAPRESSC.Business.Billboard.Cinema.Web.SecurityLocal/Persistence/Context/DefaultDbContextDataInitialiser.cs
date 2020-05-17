using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.SecurityLocal.Persistence.Context
{
    public class DefaultDbContextDataInitialiser : Core.Persistence.Context.IDbContextDataInitialiser
    {
        public void InitialiseData(DbContext context)
        {
            var roleDbSet = context.Set<Role>();

            // Category Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CategoryManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CategoryManagerViewer,
                    Description = "CategoryManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CategoryManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CategoryManagerCreator,
                    Description = "CategoryManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CategoryManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CategoryManagerEditor,
                    Description = "CategoryManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CategoryManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CategoryManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CategoryManagerRemover,
                    Description = "CategoryManagementRoleRemoverDescription"
                });
            }

            // Personality Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PersonalityManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerViewer,
                    Description = "PersonalityManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PersonalityManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerCreator,
                    Description = "PersonalityManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PersonalityManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerEditor,
                    Description = "PersonalityManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PersonalityManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PersonalityManagerRemover,
                    Description = "PersonalityManagementRoleRemoverDescription"
                });
            }

            // Genre Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.GenreManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "GenreManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.GenreManagerViewer,
                    Description = "GenreManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.GenreManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "GenreManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.GenreManagerCreator,
                    Description = "GenreManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.GenreManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "GenreManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.GenreManagerEditor,
                    Description = "GenreManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.GenreManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "GenreManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.GenreManagerRemover,
                    Description = "GenreManagementRoleRemoverDescription"
                });
            }

            // Award Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.AwardManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "AwardManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.AwardManagerViewer,
                    Description = "AwardManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.AwardManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "AwardManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.AwardManagerCreator,
                    Description = "AwardManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.AwardManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "AwardManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.AwardManagerEditor,
                    Description = "AwardManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.AwardManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "AwardManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.AwardManagerRemover,
                    Description = "AwardManagementRoleRemoverDescription"
                });
            }

            // Language Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "LanguageManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.LanguageManagerViewer,
                    Description = "LanguageManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "LanguageManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.LanguageManagerCreator,
                    Description = "LanguageManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "LanguageManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.LanguageManagerEditor,
                    Description = "LanguageManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.LanguageManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "LanguageManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.LanguageManagerRemover,
                    Description = "LanguageManagementRoleRemoverDescription"
                });
            }

            // ProducerHouse Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProducerHouseManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerViewer,
                    Description = "ProducerHouseManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProducerHouseManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerCreator,
                    Description = "ProducerHouseManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProducerHouseManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerEditor,
                    Description = "ProducerHouseManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProducerHouseManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProducerHouseManagerRemover,
                    Description = "ProducerHouseManagementRoleRemoverDescription"
                });
            }

            // Production Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProductionManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProductionManagerViewer,
                    Description = "ProductionManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProductionManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProductionManagerCreator,
                    Description = "ProductionManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProductionManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProductionManagerEditor,
                    Description = "ProductionManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.ProductionManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProductionManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.ProductionManagerRemover,
                    Description = "ProductionManagementRoleRemoverDescription"
                });
            }

            // Cinema Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CinemaManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CinemaManagerViewer,
                    Description = "CinemaManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CinemaManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CinemaManagerCreator,
                    Description = "CinemaManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CinemaManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CinemaManagerEditor,
                    Description = "CinemaManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.CinemaManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "CinemaManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.CinemaManagerRemover,
                    Description = "CinemaManagementRoleRemoverDescription"
                });
            }

            // Presentation Roles definition!!
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PresentationManagementRoleViewerTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PresentationManagerViewer,
                    Description = "PresentationManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerCreator)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PresentationManagementRoleCreatorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PresentationManagerCreator,
                    Description = "PresentationManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PresentationManagementRoleEditorTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PresentationManagerEditor,
                    Description = "PresentationManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(CinemaBillboardBusinessManagementRoleCodes.PresentationManagerRemover)).Result)
            {
                roleDbSet.Add(new Role
                {
                    Name = "PresentationManagementRoleRemoverTitle",
                    Code = CinemaBillboardBusinessManagementRoleCodes.PresentationManagerRemover,
                    Description = "PresentationManagementRoleRemoverDescription"
                });
            }

            if (context is Core.Persistence.Context.ITraceableDbContext)
            {
                (context as Core.Persistence.Context.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                context.SaveChanges();
            }
        }
    }
}
