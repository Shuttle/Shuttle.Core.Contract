using System;
using System.Globalization;

namespace Shuttle.Core.Contract
{
    public class Guard
    {
        public static void Against<TException>(bool assertion, string message) where TException : Exception
        {
            if (!assertion)
            {
                return;
            }

            Exception exception;

            try
            {
                exception = (TException) Activator.CreateInstance(typeof(TException), message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(Resources.InvalidGuardExceptionType,
                    typeof(TException).FullName, ex.Message), ex);
            }

            throw exception;
        }

        public static void AgainstNull(object value, string name)
        {
            if (value == null)
            {
                throw new NullReferenceException(string.Format(CultureInfo.CurrentCulture, Resources.NullValueException,
                    name));
            }
        }

        public static void AgainstNullOrEmptyString(string value, string name)
        {
            AgainstNull(value, name);

            if (value.Length == 0)
            {
                throw new NullReferenceException(string.Format(CultureInfo.CurrentCulture,
                    Resources.EmptyStringException, name));
            }
        }
    }
}