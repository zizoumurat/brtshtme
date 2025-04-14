using System.Globalization;
using System.Linq.Expressions;

namespace BritishTime.Domain.Pagination;
public static class IQueryableSortExtensions
{
    public static IQueryable<T> MultiSort<T>(
        this IQueryable<T> query,
        List<string> multiSorts,
        List<int> multiOrders)
    {
        if (multiSorts == null || multiOrders == null || multiSorts.Count == 0 || multiOrders.Count == 0)
        {
            return query.OrderByDescending(GetPropertyExpression<T>("Id"));
        }

        IOrderedQueryable<T> orderedQuery = null;

        for (int i = 0; i < multiSorts.Count; i++)
        {
            var propertyName = Capitalize(multiSorts[i]);
            var propertyExpression = GetPropertyExpression<T>(propertyName);

            if (i == 0)
            {
                orderedQuery = multiOrders[i] == 1
                    ? query.OrderBy(propertyExpression)
                    : query.OrderByDescending(propertyExpression);
            }
            else
            {
                orderedQuery = multiOrders[i] == 1
                    ? orderedQuery.ThenBy(propertyExpression)
                    : orderedQuery.ThenByDescending(propertyExpression);
            }
        }

        return orderedQuery ?? query;
    }

    private static string Capitalize(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
        return char.ToUpper(text[0], CultureInfo.GetCultureInfo("en-US")) + text.Substring(1);
    }

    private static Expression<Func<TModel, object>> GetPropertyExpression<TModel>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(TModel));
        Expression propertyExpression = parameter;

        foreach (var property in propertyName.Split('.'))
        {
            propertyExpression = Expression.Property(propertyExpression, property);
        }

        var expression = Expression.Lambda<Func<TModel, object>>(Expression.Convert(propertyExpression, typeof(object)), parameter);

        return expression;
    }
}
