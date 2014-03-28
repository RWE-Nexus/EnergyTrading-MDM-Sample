namespace Common.Events
{
    public class SearchResultsFound
    {
        public SearchResultsFound(int count)
        {
            this.Count = count;
        }

        public int Count { get; private set; }
    }
}