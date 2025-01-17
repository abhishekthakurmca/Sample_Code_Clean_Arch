﻿using Anubis.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.Common.Helper
{
    public static class Expressions
    {
        public static IOrderedQueryable<T> DynamicOrder<T>(this IQueryable<T> data, string sort, bool ascending = true)
        {
            var dataType = typeof(T);
            var dataProperties = dataType.GetProperties();
            var pe = Expression.Parameter(dataType, "t");

            var dataProp = dataProperties.Where(dp => dp.Name.ToLower() == sort?.ToLower()).FirstOrDefault();
            if (dataProp == null) dataProp = dataProperties.FirstOrDefault();
            var expProp = Expression.Property(pe, dataProp);

            if (dataProp.PropertyType == typeof(int))
            {
                var expOrder = Expression.Lambda<Func<T, int>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(int?))
            {
                var expOrder = Expression.Lambda<Func<T, int?>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(long))
            {
                var expOrder = Expression.Lambda<Func<T, long>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(long?))
            {
                var expOrder = Expression.Lambda<Func<T, long?>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(Boolean))
            {
                var expOrder = Expression.Lambda<Func<T, Boolean>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(decimal))
            {
                var expOrder = Expression.Lambda<Func<T, decimal>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(decimal?))
            {
                var expOrder = Expression.Lambda<Func<T, decimal?>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(DateTime))
            {
                var expOrder = Expression.Lambda<Func<T, DateTime>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(DateTime?))
            {
                var expOrder = Expression.Lambda<Func<T, DateTime?>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
            else if (dataProp.PropertyType == typeof(string))
            {
                var expOrder = Expression.Lambda<Func<T, string>>(expProp, new ParameterExpression[] { pe });
                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }

            else
            {
                var methToString = dataProp.PropertyType.GetMethod("ToString", Type.EmptyTypes);
                var expToString = Expression.Call(expProp, methToString);
                var expOrder = Expression.Lambda<Func<T, string>>(expToString, new ParameterExpression[] { pe });

                if (ascending)
                    return data.OrderBy(expOrder);
                else
                    return data.OrderByDescending(expOrder);
            }
        }

        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> data, Dictionary<string, string> filter)
        {
            if (filter != null)
            {
                var dataType = typeof(T);
                var dataProperties = dataType.GetProperties();
                var pe = Expression.Parameter(dataType, "t");
                Expression result = null;

                foreach (var filterProp in filter.Keys)
                {
                    var dataProp = dataProperties.Where(dp => dp.Name.ToLower() == filterProp.ToLower()).FirstOrDefault();
                    if (dataProp != null && !string.IsNullOrWhiteSpace(filter[filterProp]))
                    {
                        var expProp = Expression.Property(pe, dataProp);
                        if (dataProp.PropertyType == typeof(bool))
                        {
                            var val = (filter[filterProp].ToLower() == "true" || filter[filterProp].ToLower() == "1" || filter[filterProp].ToLower() == "yes" || filter[filterProp].ToLower() == "active" || filter[filterProp].ToLower() == "in");
                            var expressionForBool = Expression.MakeBinary(ExpressionType.Equal, expProp, Expression.Constant(val, typeof(bool)));
                            result = result == null ? expressionForBool : Expression.AndAlso(result, expressionForBool);
                        }
                        else if (dataProp.PropertyType == typeof(DateTime) || dataProp.PropertyType == typeof(DateTime?))
                        {
                            var dateFormats = new[] { "MM/d/yyyy", "MM/dd/yyyy", "MMM d, yyyy", "M/d/yyyy", "M/dd/yyyy", "MMM dd, yyyy", "dd MMM yyyy" };
                            var val = filter[filterProp].Trim();

                            if (DateTime.TryParseExact(val, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTimeValue))
                            {
                                var startDateTime = dateTimeValue.Date;
                                var endDateTime = startDateTime.AddDays(1).AddMilliseconds(-1);
                                var expStartValue = Expression.Constant(startDateTime, typeof(DateTime));
                                var expEndValue = Expression.Constant(endDateTime, typeof(DateTime));
                                var expStartEqual = Expression.GreaterThanOrEqual(Expression.Property(dataProp.PropertyType == typeof(DateTime) ? expProp : Expression.Property(expProp, "Value"), "Date"), Expression.Property(expStartValue, "Date"));
                                var expEndEqual = Expression.LessThanOrEqual(Expression.Property(dataProp.PropertyType == typeof(DateTime) ? expProp : Expression.Property(expProp, "Value"), "Date"), Expression.Property(expEndValue, "Date"));

                                var dateRangeExpression = Expression.AndAlso(expStartEqual, expEndEqual);
                                result = result == null ? dateRangeExpression : Expression.AndAlso(result, dateRangeExpression);
                            }
                            else
                            {
                                var defaultDateTime = default(DateTime);

                                var expDefaultDateTime = Expression.Constant(defaultDateTime, typeof(DateTime));
                                var expDefaultEqual = Expression.Equal(Expression.Property(dataProp.PropertyType == typeof(DateTime) ? expProp : Expression.Property(expProp, "Value"), "Date"), Expression.Property(expDefaultDateTime, "Date"));
                                result = result == null ? expDefaultEqual : Expression.AndAlso(result, expDefaultEqual);
                            }
                        }
                        else
                        {
                            var methToString = dataProp.PropertyType.GetMethod("ToString", Type.EmptyTypes);
                            var expToString = Expression.Call(expProp, methToString);
                            var methContains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                            var expConstant = Expression.Constant(filter[filterProp], typeof(string));
                            var expContains = dataProp.PropertyType == typeof(string)
                                ? Expression.Call(expProp, methContains, expConstant)
                                : Expression.Call(expToString, methContains, expConstant);
                            if (result == null)
                                result = expContains;
                            else
                                result = Expression.AndAlso(result, expContains);
                        }
                    }
                }
                if (result != null)
                {
                    return data.Where(Expression.Lambda<Func<T, bool>>(result, new ParameterExpression[] { pe }));
                }
            }
            return data;
        }

        public static async Task<GridResult<T>> DynamicPageAsync<T>(this IQueryable<T> data, GridQuery query, CancellationToken cancellationToken)
        {
            int start = (query.Page - 1) * query.PageSize;
            var res = data
                .DynamicWhere(query.Filter)
                .DynamicOrder(query.Sort, query.Ascending);
            int total = res.Count();
            int pages = (int)Math.Ceiling((double)total / query.PageSize);
            int page = Math.Min(pages, Math.Max(1, query.Page));
            int pageSize = Math.Min(Math.Max(1, total), query.PageSize);

            return new GridResult<T>
            {
                Data = await res
                    .Skip(start)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken),
                Total = total,
                Page = page
            };
        }
        public static async Task<GridResult<T>> DynamicExportPageAsync<T>(this IQueryable<T> data, GridQuery query, CancellationToken cancellationToken)
        {
            int start = (query.Page - 1) * query.PageSize;
            var res = data
                .DynamicWhere(query.Filter)
                .DynamicOrder(query.Sort, query.Ascending);
            int total = res.Count();
            int pages = (int)Math.Ceiling((double)total / query.PageSize);
            int page = Math.Min(pages, Math.Max(1, query.Page));
            int pageSize = Math.Min(Math.Max(1, total), query.PageSize);

            return new GridResult<T>
            {
                Data = await res
                    .Skip(0)
                    .Take(total)
                    .ToListAsync(cancellationToken),
                Total = total,
                Page = page
            };
        }
    }
}
