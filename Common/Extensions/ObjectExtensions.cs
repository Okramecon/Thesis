using System.Text.Json;

namespace Common.Extensions
{
    public static class ObjectExtention
    {
        public static bool IsNull(this object x)
        {
            return x == null;
        }

        public static bool IsNotNull(this object x)
        {
            return !x.IsNull();
        }

        public static string ToJson<T>(this T x)
        {
            return JsonSerializer.Serialize(x);
        }

        public static T FromJson<T>(this string x)
        {
            return JsonSerializer.Deserialize<T>(x);
        }
    }
}