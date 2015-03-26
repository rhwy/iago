namespace Iago.ConsoleWriter
{
    using System.Collections.Generic;
    
    public static class EnumerableExtensions
    {
        public static string ToEachLineString<T>(this IEnumerable<T> list)
        {
            var sb = new System.Text.StringBuilder();
            foreach(var item in list)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}
