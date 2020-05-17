using System.Collections.Generic;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Models.View
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            WeekStatisticsColumns = new List<string>();
            MonthStatisticsColumns = new List<string>();
            SemesterStatisticsColumns = new List<string>();
            YearStatisticsColumns = new List<string>();

            WeekTotalCommentStatisticsValues = new List<int>();
            WeekPresentationCommentStatisticsValues = new List<int>();
            WeekEventCommentStatisticsValues = new List<int>();
            WeekPersonalityCommentStatisticsValues = new List<int>();

            MonthTotalCommentStatisticsValues = new List<int>();
            MonthPresentationCommentStatisticsValues = new List<int>();
            MonthEventCommentStatisticsValues = new List<int>();
            MonthPersonalityCommentStatisticsValues = new List<int>();

            SemesterTotalCommentStatisticsValues = new List<int>();
            SemesterPresentationCommentStatisticsValues = new List<int>();
            SemesterEventCommentStatisticsValues = new List<int>();
            SemesterPersonalityCommentStatisticsValues = new List<int>();

            YearTotalCommentStatisticsValues = new List<int>();
            YearPresentationCommentStatisticsValues = new List<int>();
            YearEventCommentStatisticsValues = new List<int>();
            YearPersonalityCommentStatisticsValues = new List<int>();

            WeekMobileDatabaseUpdateStatisticsValues = new List<int>();

            MonthMobileDatabaseUpdateStatisticsValues = new List<int>();

            SemesterMobileDatabaseUpdateStatisticsValues = new List<int>();

            YearMobileDatabaseUpdateStatisticsValues = new List<int>();
    }

        public Dictionary<string, ICollection<Core.Web.Management.Models.View.DashboardManageItemViewModel>> Groups { get; set; }

        public long PersistenceSecurityUserCount { get; set; }
        public long PersistenceSecurityGroupCount { get; set; }
        public long PersistenceSecurityRoleCount { get; set; }
        public long PersistenceSecurityTraceCount { get; set; }

        public long PersistenceBillboardProductionCount { get; set; }
        public long PersistenceBillboardPresentationCount { get; set; }
        public long PersistenceBillboardHighlightCount { get; set; }
        public long PersistenceBillboardEventCount { get; set; }
        public long PersistenceBillboardConfigCount { get; set; }
        public long PersistenceBillboardProductionCommentCount { get; set; }
        public long PersistenceBillboardEventCommentCount { get; set; }
        public long PersistenceBillboardPersonalityCommentCount { get; set; }

        public long PersistenceSupportCountryCount { get; set; }
        public long PersistenceSupportCategoryCount { get; set; }
        public long PersistenceSupportPersonalityCount { get; set; }
        public long PersistenceSupportGenreCount { get; set; }
        public long PersistenceSupportAwardCount { get; set; }
        public long PersistenceSupportLanguageCount { get; set; }
        public long PersistenceSupportProductionHouseCount { get; set; }
        public long PersistenceSupportCinemaCount { get; set; }
        public long PersistenceSupportClasifiersCount { get; set; }

        public string CurrentPortableDatabaseDateTimeFormated { get; set; }

        public int NextWeekBillboardEventCount { get; set; }

        public int NextWeekBillboardPresentationCount { get; set; }


        public List<string> WeekStatisticsColumns { get; set; }
        public List<string> YearStatisticsColumns { get; set; }
        public List<string> SemesterStatisticsColumns { get; set; }
        public List<string> MonthStatisticsColumns { get; set; }


        public List<int> WeekTotalCommentStatisticsValues { get; set; }

        public List<int> WeekPresentationCommentStatisticsValues { get; set; }

        public List<int> WeekEventCommentStatisticsValues { get; set; }

        public List<int> WeekPersonalityCommentStatisticsValues { get; set; }


        public List<int> MonthTotalCommentStatisticsValues { get; set; }

        public List<int> MonthPresentationCommentStatisticsValues { get; set; }

        public List<int> MonthEventCommentStatisticsValues { get; set; }

        public List<int> MonthPersonalityCommentStatisticsValues { get; set; }


        public List<int> SemesterTotalCommentStatisticsValues { get; set; }

        public List<int> SemesterPresentationCommentStatisticsValues { get; set; }

        public List<int> SemesterEventCommentStatisticsValues { get; set; }

        public List<int> SemesterPersonalityCommentStatisticsValues { get; set; }


        public List<int> YearTotalCommentStatisticsValues { get; set; }

        public List<int> YearPresentationCommentStatisticsValues { get; set; }

        public List<int> YearEventCommentStatisticsValues { get; set; }

        public List<int> YearPersonalityCommentStatisticsValues { get; set; }


        public List<int> WeekMobileDatabaseUpdateStatisticsValues { get; set; }


        public List<int> MonthMobileDatabaseUpdateStatisticsValues { get; set; }


        public List<int> SemesterMobileDatabaseUpdateStatisticsValues { get; set; }

        
        public List<int> YearMobileDatabaseUpdateStatisticsValues { get; set; }
    }
}
