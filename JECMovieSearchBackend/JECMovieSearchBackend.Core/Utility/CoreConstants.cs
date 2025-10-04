namespace JECMovieSearchBackend.Core.Utility
{
    public abstract class CoreConstants
    {
        public static class CacheConstants
        {
            public static class Keys
            {
                public const string MovieSearchHistory = "moviesearchhistory";
            }

            public static class CacheTime
            {
                public const int DayInMinutes = 1440;
                public const int MonthInMinutes = 43800;
            }
        }
    }
}
