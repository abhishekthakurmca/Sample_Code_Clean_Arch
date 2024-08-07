using Anubis.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Anubis.Application.Common.Helper
{
    public static class GridCustomFilter
    {
        public static (DateTime agingDays, string receivingFormType, int routeShipmentId, string stateCity, string inBoundArrangedByCustomer) CustomFilterValue<T>(this T request) where T : GridQuery
        {
            DateTime agingDays = default(DateTime);
            string receivingFormType = string.Empty;
            int routeShipmentId = 0;
            string stateCity = string.Empty;
            string inBoundArrangedByCustomer = string.Empty;
            DateTime orderDate = new DateTime();
            var dateFormats = new[] { "MM/d/yyyy", "MM/dd/yyyy", "M/d/yyyy", "M/dd/yyyy", "dd/M/yyyy" };
            if (request.Filter.TryGetValue("orderDateAgingDays", out string filterValue))
            {
                if (int.TryParse(filterValue, out int agingDaysNumeric))
                {
                    agingDays = DateTime.Now.AddDays(-agingDaysNumeric);
                    request.Filter.Remove("orderDateAgingDays");
                }
                else if (DateTime.TryParseExact(filterValue, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out orderDate))
                {
                    request.Filter.Remove("orderDateAgingDays");
                    request.Filter.Add("orderCreateDate", orderDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    request.Filter.Add("orderDate", request.Filter["orderDateAgingDays"]);
                }
                request.Filter.Remove("orderDateAgingDays");
            }

            if (request.Filter.ContainsKey("receivingFormType") && !string.IsNullOrEmpty(request.Filter["receivingFormType"]))
            {
                receivingFormType = request.Filter["receivingFormType"].ToString().ToLower();
                request.Filter.Remove("receivingFormType");
            }

            if (request.Filter.ContainsKey("routeShipmentId") && request.Filter.ContainsKey("routeShipmentId") && !string.IsNullOrEmpty(request.Filter["routeShipmentId"]))
            {
                request.Filter["routeShipmentId"] = request.Filter["routeShipmentId"].StartsWith("R") && request.Filter["routeShipmentId"].Length > 1 ? request.Filter["routeShipmentId"].Substring(1) : request.Filter["routeShipmentId"];
                routeShipmentId = Convert.ToInt32(request.Filter["routeShipmentId"]);
                request.Filter.Remove("routeShipmentId");
            }

            if (request.Filter.ContainsKey("cityState") && !string.IsNullOrEmpty(request.Filter["cityState"]))
            {
                stateCity = request.Filter["cityState"].ToLower();
                request.Filter.Remove("cityState");
            }

            if (request.Filter.ContainsKey("inBoundArrangedByCustomer") && !string.IsNullOrEmpty(request.Filter["inBoundArrangedByCustomer"]))
            {
                inBoundArrangedByCustomer = request.Filter["inBoundArrangedByCustomer"].ToLower();
                request.Filter.Remove("inBoundArrangedByCustomer");
            }

            return (agingDays, receivingFormType, routeShipmentId, stateCity, inBoundArrangedByCustomer);
        }
    }
}
