public static class ZodiacHelper
{
    public static string GetZodiacSign(DateTime birthDate)
    {
        int day = birthDate.Day;
        int month = birthDate.Month;

        return (month, day) switch
        {
            (1, <= 19) or (12, >= 22) => "♑ თხის რქა",
            (1, _) or (2, <= 18) => "♒ მერწყული",
            (2, _) or (3, <= 20) => "♓ თევზები",
            (3, _) or (4, <= 19) => "♈ ვერძი",
            (4, _) or (5, <= 20) => "♉ კურო",
            (5, _) or (6, <= 20) => "♊ ტყუპები",
            (6, _) or (7, <= 22) => "♋ კირჩხიბი",
            (7, _) or (8, <= 22) => "♌ ლომი",
            (8, _) or (9, <= 22) => "♍ ქალწული",
            (9, _) or (10, <= 22) => "♎ სასწორი",
            (10, _) or (11, <= 21) => "♏ მორიელი",
            (11, _) or (12, <= 21) => "♐ მშვილდოსანი",
            _ => "უცნობი"
        };
    }
}

