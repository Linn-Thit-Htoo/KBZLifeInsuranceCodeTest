namespace KBZLifeInsuranceCodeTest.Shared;

public static class DevCode
{
    public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

    public static T ToObject<T>(this string jsonStr) => JsonConvert.DeserializeObject<T>(jsonStr)!;

    public static IQueryable<TSource> Paginate<TSource>(
        this IQueryable<TSource> source,
        int pageNo,
        int pageSize
    )
    {
        return source.Skip((pageNo - 1) * pageSize).Take(pageSize);
    }

    public static bool IsNullOrEmpty(this string str) =>
        string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str);

    public static string GeneratePromoCode()
    {
        Random random = new Random();

        // Generate 6 random digits
        string digits = new string(
            Enumerable.Range(0, 6).Select(x => random.Next(0, 10).ToString()[0]).ToArray()
        );

        // Generate 5 random uppercase letters
        string alphabets = new string(
            Enumerable.Range(0, 5).Select(x => (char)random.Next('A', 'Z' + 1)).ToArray()
        );

        // Combine the digits and alphabets to create the promo code
        return digits + alphabets;
    }
}
