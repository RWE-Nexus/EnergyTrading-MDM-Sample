namespace MDM.Sync
{
    public static class StringExtensions
    {
         public static string TrimNewLinesAndTabs(this string input)
         {
             return string.IsNullOrWhiteSpace(input) ? input : input.Replace("\n", " ").Replace("\t", " ").Replace("\r", " ");
         }
    }
}