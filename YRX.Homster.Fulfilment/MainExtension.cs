using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YRX.Homster.Fulfilment
{
    public static class MainExtension
    {
        public static string ToStringOnSteroids(this IEnumerable<int> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var group = source.Select((v, i) => new { v, key = v - i }).GroupBy(g => g.key, g => g.v);

            return string.Join(",", group.Select(g => string.Join("-", new int[] { g.First(), g.Last() }.Distinct())));
        }
    }
}
