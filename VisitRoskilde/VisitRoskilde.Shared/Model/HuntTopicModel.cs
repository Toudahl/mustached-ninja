namespace VisitRoskilde.Model
{
    public class HuntTopicModel
    {
        private string _time;
        private string _end;
        private string _start;
        private string _topic;
        private string _theme;
        private string _reward;

        public string Topic
        {
            get { return _topic; }
            set { _topic = value; }
        }

        public string Theme
        {
            get { return _theme; }
            set { _theme = value; }
        }

        public string Reward
        {
            get { return _reward; }
            set { _reward = value; }
        }

        public string Start
        {
            get { return _start; }
            set { _start = value; }
        }

        public string End
        {
            get { return _end; }
            set { _end = value; }
        }

        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public override string ToString()
        {
            return Topic.ToString();
        }
    }
}
