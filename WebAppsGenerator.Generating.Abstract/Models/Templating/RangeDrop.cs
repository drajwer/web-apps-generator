using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;

namespace WebAppsGenerator.Generating.Abstract.Models.Templating
{
    /// <summary>
    /// Interface for object containing Max and Min properties.
    /// </summary>
    public interface IRangeDrop
    {
        object Min { get; set; }
        object Max { get; set; }
    }

    public abstract class RangeDrop<TVal> : Drop, IRangeDrop where TVal : struct, IComparable
    {
        public object Min { get; set; }
        public object Max { get; set; }

        protected RangeDrop(TVal? min, TVal? max)
        {
            Min = min;
            Max = max;
        }
    }

    /// <summary>
    /// Object containing Max and Min integer properties.
    /// </summary>
    public class IntRangeDrop : RangeDrop<int>
    {
        public IntRangeDrop(int? min, int? max) : base(min, max)
        {
        }
    }

    /// <summary>
    /// Object containing Max and Min double properties.
    /// </summary>
    public class DoubleRangeDrop : RangeDrop<double>
    {
        public DoubleRangeDrop(double? min, double? max) : base(min, max)
        {
        }
    }
}
