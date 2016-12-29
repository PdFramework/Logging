namespace PeinearyDevelopment.Framework.Logging.Contracts
{
    public class ActionTakenEvent : BaseLogEvent
    {
        private object _object;

        public ActionType Type { get; set; }
        /// <summary>
        /// object that will be JSON serialized and stored in the database as a JSON string
        /// </summary>
        public object Object
        {
            get
            {
                return _object;
            }
            set
            {
                ObjectNamespace = value.GetType().FullName;
                _object = value;
            }
        }
        public string ObjectNamespace { get; private set; }
        public string ObjectId { get; set; }
    }
}
