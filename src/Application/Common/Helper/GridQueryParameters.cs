using Anubis.Application.Common.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Anubis.Application.Common.Helper
{
    public static class GridQueryParameters
    {
        public static DynamicParameters Parameters<T>(this T request, bool isApproved = false) where T : GridQuery
        {
            var parameters = new DynamicParameters();

            if (request.Filter.Count > 0)
            {
                foreach (var filterProp in request.Filter)
                {
                    string paramName = "@" + filterProp.Key;
                    object paramValue = string.IsNullOrEmpty(filterProp.Value) ? (object)DBNull.Value : filterProp.Value;

                    if (paramValue == DBNull.Value)
                    {
                        parameters.Add(paramName, null, DbType.String);
                    }
                    else
                    {
                        parameters.Add(paramName, paramValue);
                    }
                }
            }

            parameters.Add("@PageSize", request.PageSize);
            parameters.Add("@Page", request.Page);
            parameters.Add("@Sort", request.Sort);
            parameters.Add("@Ascending", request.Ascending);
            parameters.Add("@TotalRecordCount", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            return parameters;
        }
    }
}
