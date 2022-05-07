namespace Dtat.Result
{
    public class Result
    {
        public Result() : base()
        {
            IsSuccess = true;
            IsFailed = false;

            _errors =
                new List<string>();

            _successes =
                new List<string>();
        }

        public bool IsFailed { get; set; }

        public bool IsSuccess { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        private readonly List<string> _errors;

        public IReadOnlyList<string> Errors
        {
            get
            {
                return _errors;
            }
        }

        [System.Text.Json.Serialization.JsonIgnore]
        private readonly List<string> _successes;

        public IReadOnlyList<string> Successes
        {
            get
            {
                return _successes;
            }
        }

        public void AddErrorMessage(string message)
        {

            if (message == null)
            {
                return;
            }

            if (_errors.Contains(message))
            {
                return;
            }

            _errors.Add(message);

            IsFailed = true;
            IsSuccess = false;
        }

        public void RemoveErrorMessage(string message)
        {

            if (message == null)
            {
                return;
            }

            _errors.Remove(message);

            if (_errors.Count == 0)
            {
                IsFailed = false;
                IsSuccess = true;
            }
        }

        public void ClearErrorMessages()
        {
            IsFailed = false;
            IsSuccess = true;

            _errors.Clear();
        }

        public void AddSuccessMessage(string message)
        {

            if (message == null)
            {
                return;
            }

            if (_successes.Contains(message))
            {
                return;
            }

            _successes.Add(message);
        }

        public void RemoveSuccessMessage(string message)
        {

            if (message == null)
            {
                return;
            }

            _successes.Remove(message);
        }

        public void ClearSuccessMessages()
        {
            _successes.Clear();
        }

        public void ClearAllMessages()
        {
            ClearErrorMessages();
            ClearSuccessMessages();
        }
    }
}