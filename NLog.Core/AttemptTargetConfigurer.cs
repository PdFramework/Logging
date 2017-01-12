namespace PeinearyDevelopment.Framework.Logging.NLog.Core
{
    public class AttemptTargetConfigurer
    {
        public AttemptTargetConfigurer()
        {
            ShouldAttemptTarget = true;
        }

        public bool ShouldAttemptTarget { get; set; }
        public int? AttemptRank { get; set; }
        public TargetType Type { get; set; }
    }
}
