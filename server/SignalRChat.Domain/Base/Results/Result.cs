namespace SignalRChat.Infra.Results
{
    public struct Result<TSuccess, TFailure>
    {
        public TFailure Failure { get; internal set; }
        public TSuccess Success { get; internal set; }

        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        internal Result(TFailure failure)
        {
            IsFailure = true;
            Failure = failure;
            Success = default(TSuccess);
        }

        internal Result(TSuccess success)
        {
            IsFailure = false;
            Failure = default(TFailure);
            Success = success;
        }

        public static implicit operator Result<TSuccess, TFailure>(TFailure failure)
            => new Result<TSuccess, TFailure>(failure);

        public static implicit operator Result<TSuccess, TFailure>(TSuccess success)
            => new Result<TSuccess, TFailure>(success);
    }
}
