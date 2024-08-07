
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.Common.Models
{
    public class GridResult<T>
    {
        public IList<T> Data { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int NeedHelpCount { get; set; }

    }
    public class GridWGSResult<M,W,P>
    {
        public IList<M> Miles { get; set; }
        public IList<W> Weights { get; set; }
        public IList<P> WeightPrice { get; set; }

    }

}
