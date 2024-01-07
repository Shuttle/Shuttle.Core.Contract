using System;

namespace Shuttle.Core.Contract
{
    public class OperationEventArgs : EventArgs
    {
        public string Name { get; }
        public object Data { get; }

        public OperationEventArgs(string name, object data = null)
        {
            Name = Guard.AgainstNullOrEmptyString(name, nameof(name));
            Data = data;
        }
    }

    public class OperationEventArgs<T> : EventArgs
    {
        public string Name { get; }
        public T Data { get; }

        public OperationEventArgs(string name, T data)
        {
            Name = Guard.AgainstNullOrEmptyString(name, nameof(name));
            Data = data;
        }
    }
}