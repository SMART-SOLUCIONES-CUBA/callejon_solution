namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Helpers
{
    public static class DashboardManagementItemIconographyHelper
    {
        public static string GetIconography(Core.Web.Management.Models.View.DashboardManageItemViewModel model)
        {
            if (model.Controller == "users") return "<span class=\"glyphicon glyphicon-user\"></span>";

            if (model.Controller == "groups") return "<span class=\"glyphicon glyphicon-tower\"></span>";

            if (model.Controller == "advises") return "<span class=\"glyphicon glyphicon-bullhorn\"></span>";

            if (model.Controller == "events") return "<span class=\"glyphicon glyphicon-file\"></span>";

            if (model.Controller == "productions") return "<span class=\"glyphicon glyphicon-film\"></span>";

            if (model.Controller == "presentations") return "<span class=\"glyphicon glyphicon-blackboard\"></span>";

            if (model.Controller == "fixscreenclientconfigurations") return "<span class=\"glyphicon glyphicon-cog\"></span>";

            if (model.Controller == "genres") return "<span class=\"glyphicon glyphicon-bookmark\"></span>";

            if (model.Controller == "awards") return "<span class=\"glyphicon glyphicon-grain\"></span>";

            if (model.Controller == "countries") return "<span class=\"glyphicon glyphicon-globe\"></span>";

            if (model.Controller == "languages") return "<span class=\"glyphicon glyphicon-flag\"></span>";

            if (model.Controller == "cinemas") return "<span class=\"glyphicon glyphicon-map-marker\"></span>";

            if (model.Controller == "producerhouses") return "<span class=\"glyphicon glyphicon-certificate\"></span>";

            if (model.Controller == "personalities") return "<span class=\"glyphicon glyphicon-education\"></span>";

            if (model.Controller == "categories") return "<span class=\"glyphicon glyphicon-star\"></span>";

            if (model.Controller == "commentclassifiers") return "<span class=\"glyphicon glyphicon-magnet\"></span>";

            if (model.Controller.EndsWith("comments")) return "<span class=\"glyphicon glyphicon-comment\"></span>";

            if (model.Controller.EndsWith("delations")) return "<span class=\"glyphicon glyphicon-thumbs-down\"></span>";

            if (model.Controller.EndsWith("collections")) return "<span class=\"glyphicon glyphicon-list\"></span>";

            if (model.Controller.EndsWith("tags")) return "<span class=\"glyphicon glyphicon-tags\"></span>";

            return "<span class=\"glyphicon glyphicon-file\"></span>";
        }
    }
}
