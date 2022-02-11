using Microsoft.AspNetCore.Mvc.Rendering;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Models.Filter
{
    public class ProductionFilterModel : Core.Web.Management.Models.Filter.IEntityFilterModel<Persistence.Entities.Production, long>
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public int GenreId { get; set; }

        public List<SelectListItem> Genres { get; set; }

        public int InterpreterId { get; set; }

        public List<SelectListItem> Interpreters { get; set; }

        public long PersonalityId { get; set; }

        public List<SelectListItem> Personalities { get; set; }

        public Expression<System.Func<Persistence.Entities.Production, bool>> GetSelector()
        {
            Expression<System.Func<Persistence.Entities.Production, bool>> expression = null;

            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrWhiteSpace(Title))
            {
                expression = entity => entity.Title.Contains(Title.Trim()) || entity.LocalizedTitle.Contains(Title.Trim());
            }

            if (!string.IsNullOrEmpty(Year) && !string.IsNullOrWhiteSpace(Year))
            {
                try
                {
                    var parsedYear = int.Parse(Year);

                    Expression<System.Func<Persistence.Entities.Production, bool>> yearExpression = entity => entity.Year == parsedYear;

                    if (expression == null)
                    {
                        expression = yearExpression;
                    }
                    else
                    {
                        expression = expression.CombineWithAndAlso(yearExpression);
                    }
                }
                catch
                {
                    Year = string.Empty;
                }
            }

            if (CountryId > 0)
            {
                Expression<System.Func<Persistence.Entities.Production, bool>> countryExpression = entity => entity.Country.Id == CountryId || entity.CountryCoProductionBridges.Any(bridge => bridge.CountryId == CountryId);

                if (expression == null)
                {
                    expression = countryExpression;
                }
                else
                {
                    expression = expression.CombineWithAndAlso(countryExpression);
                }
            }

            if (GenreId > 0)
            {
                Expression<System.Func<Persistence.Entities.Production, bool>> genreExpression = entity => entity.ProductionGenreBridges.Any(bridge => bridge.GenreId == GenreId);

                if (expression == null)
                {
                    expression = genreExpression;
                }
                else
                {
                    expression = expression.CombineWithAndAlso(genreExpression);
                }
            }

            if (PersonalityId > 0)
            {
                Expression<System.Func<Persistence.Entities.Production, bool>> personalityExpression = entity => entity.Participations.Any(bridge => bridge.Personality.Id == PersonalityId);

                if (expression == null)
                {
                    expression = personalityExpression;
                }
                else
                {
                    expression = expression.CombineWithAndAlso(personalityExpression);
                }
            }

            return expression;
        }
    }
}
