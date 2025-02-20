using System;

namespace IdentityProvider.Data.Base
{
    public class RepositoryActionResult
    {

    }

    public class RepositoryActionResult<T> : RepositoryActionResult where T : class
    {
        public T Entity { get; }
        public RepositoryActionStatus Status { get; }
        public Exception Exception { get; }

        public RepositoryActionResult(T entity, RepositoryActionStatus status)
        {
            Entity = entity;
            Status = status;
        }

        public RepositoryActionResult(T entity, RepositoryActionStatus status, Exception exception) :
            this(entity, status)
        {
            Exception = exception;
        }
    }

    public enum RepositoryActionStatus
    {
        Okay,
        Created,
        Updated,
        NotFound,
        Deleted,
        NothingModified,
        Error
    }
}
