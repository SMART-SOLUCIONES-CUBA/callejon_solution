using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.SecurityShared;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.SecurityLocal.Persistence.Context
{
    public class DefaultDbContextDataInitialiser : Core.Persistence.Context.IDbContextDataInitialiser
    {
        public void InitialiseData(Microsoft.EntityFrameworkCore.DbContext context)
        {
            var roleDbSet = context.Set<Role>();

            // Advise Roles definition!!
            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerViewer)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "AdviseManagementRoleViewerTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.AdviseManagerViewer,
                    Description = "AdviseManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerCreator)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "AdviseManagementRoleCreatorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.AdviseManagerCreator,
                    Description = "AdviseManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerEditor)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "AdviseManagementRoleEditorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.AdviseManagerEditor,
                    Description = "AdviseManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.AdviseManagerRemover)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "AdviseManagementRoleRemoverTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.AdviseManagerRemover,
                    Description = "AdviseManagementRoleRemoverDescription"
                });
            }

            // Event Roles definition!!
            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.EventManagerViewer)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "EventManagementRoleViewerTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.EventManagerViewer,
                    Description = "EventManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.EventManagerCreator)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "EventManagementRoleCreatorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.EventManagerCreator,
                    Description = "EventManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "EventManagementRoleEditorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.EventManagerEditor,
                    Description = "EventManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.EventManagerRemover)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "EventManagementRoleRemoverTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.EventManagerRemover,
                    Description = "EventManagementRoleRemoverDescription"
                });
            }

            // FixScreenClientConfiguration Roles definition!!
            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerViewer)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "FixScreenClientConfigurationManagementRoleViewerTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerViewer,
                    Description = "FixScreenClientConfigurationManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerCreator)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "FixScreenClientConfigurationManagementRoleCreatorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerCreator,
                    Description = "FixScreenClientConfigurationManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerEditor)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "FixScreenClientConfigurationManagementRoleEditorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerEditor,
                    Description = "FixScreenClientConfigurationManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerRemover)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "FixScreenClientConfigurationManagementRoleRemoverTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.FixScreenClientConfigurationManagerRemover,
                    Description = "FixScreenClientConfigurationManagementRoleRemoverDescription"
                });
            }

            // EventComment Roles definition!!
            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.EventCommentManagerViewer)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "EventCommentManagementRoleViewerTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.EventCommentManagerViewer,
                    Description = "EventCommentManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.EventCommentManagerEditor)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "EventCommentManagementRoleEditorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.EventCommentManagerEditor,
                    Description = "EventCommentManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.EventCommentManagerRemover)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "EventCommentManagementRoleRemoverTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.EventCommentManagerRemover,
                    Description = "EventCommentManagementRoleRemoverDescription"
                });
            }

            // ProductionComment Roles definition!!
            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.ProductionCommentManagerViewer)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProductionCommentManagementRoleViewerTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.ProductionCommentManagerViewer,
                    Description = "ProductionCommentManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.ProductionCommentManagerEditor)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProductionCommentManagementRoleEditorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.ProductionCommentManagerEditor,
                    Description = "ProductionCommentManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.ProductionCommentManagerRemover)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "ProductionCommentManagementRoleRemoverTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.ProductionCommentManagerRemover,
                    Description = "ProductionCommentManagementRoleRemoverDescription"
                });
            }

            // PersonalityComment Roles definition!!
            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerViewer)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "PersonalityCommentManagementRoleViewerTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerViewer,
                    Description = "PersonalityCommentManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerEditor)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "PersonalityCommentManagementRoleEditorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerEditor,
                    Description = "PersonalityCommentManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerRemover)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "PersonalityCommentManagementRoleRemoverTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.PersonalityCommentManagerRemover,
                    Description = "PersonalityCommentManagementRoleRemoverDescription"
                });
            }

            // CommentClassifier Roles definition!!
            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerViewer)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "CommentClassifierManagementRoleViewerTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerViewer,
                    Description = "CommentClassifierManagementRoleViewerDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerCreator)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "CommentClassifierManagementRoleCreatorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerCreator,
                    Description = "CommentClassifierManagementRoleCreatorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerEditor)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "CommentClassifierManagementRoleEditorTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerEditor,
                    Description = "CommentClassifierManagementRoleEditorDescription"
                });
            }

            if (!roleDbSet.Any(role => role.Code.Equals(CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerRemover)))
            {
                roleDbSet.Add(new Role
                {
                    Name = "CommentClassifierManagementRoleRemoverTitle",
                    Code = CPCCBillboardBusinessManagementRoleCodes.CommentClassifierManagerRemover,
                    Description = "CommentClassifierManagementRoleRemoverDescription"
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
