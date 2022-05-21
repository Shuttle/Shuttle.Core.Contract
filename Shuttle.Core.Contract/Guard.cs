using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
                exception = (TException) Activator.CreateInstance(typeof(TException),
                    !string.IsNullOrWhiteSpace(message) ? message : Resources.NoMessageSpecified);
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
                    !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }
        }

        public static void AgainstNullOrEmptyString(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullReferenceException(string.Format(CultureInfo.CurrentCulture,
                    Resources.EmptyStringException,
                    !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }
        }

        public static void AgainstUndefinedEnum<TEnum>(object value, string name)
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                    string.Format(CultureInfo.CurrentCulture, Resources.UndefinedEnumException,
                        value ?? Resources.NoValueSpecified, typeof(TEnum).FullName,
                        !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified)));
            }
        }

        public static void AgainstEmptyEnumerable<T>(IEnumerable<T> enumerable, string name)
        {
            AgainstNull(enumerable, name);

            if (!enumerable.Any())
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                    string.Format(CultureInfo.CurrentCulture, Resources.EmptyEnumerableException,
                        !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified)));
            }
        }
    }
}