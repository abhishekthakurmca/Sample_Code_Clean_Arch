using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.Common.Models
{
    public class GoogleAPIResult
    {
        public List<Row> rows { get; set; }
        public string status { get; set; }

    }
    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public List<Element> elements { get; set; }
    }



}
