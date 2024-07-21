using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

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
                exception = (TException) Activator.CreateInstance(typeof(TException), !string.IsNullOrWhiteSpace(message) ? message : Resources.NoMessageSpecified);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(Resources.InvalidGuardExceptionType, typeof(TException).FullName, ex.Message), ex);
            }

            throw exception;
        }

#if NET6_0_OR_GREATER
        public static T AgainstNull<T>(T value, [CallerArgumentExpression("value")] string name = null)
#else
        public static T AgainstNull<T>(T value, string name)
#endif
        {
            if (value == null)
            {
                throw new NullReferenceException(string.Format(CultureInfo.CurrentCulture, Resources.NullValueException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            return value;
        }

#if NET6_0_OR_GREATER
        public static string AgainstNullOrEmptyString(string value, [CallerArgumentExpression("value")] string name = null)
#else
        public static string AgainstNullOrEmptyString(string value, string name)
        #endif
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullReferenceException(string.Format(CultureInfo.CurrentCulture, Resources.EmptyStringException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            return value;
        }

#if NET6_0_OR_GREATER
        public static Guid AgainstEmptyGuid(Guid value, [CallerArgumentExpression("value")] string name = null)
#else
        public static Guid AgainstEmptyGuid(Guid value, string name)
#endif
        {
            if (Guid.Empty.Equals(value))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.EmptyGuidException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            return value;
        }

#if NET6_0_OR_GREATER
        public static TEnum AgainstUndefinedEnum<TEnum>(object value, [CallerArgumentExpression("value")] string name = null)
#else
        public static TEnum AgainstUndefinedEnum<TEnum>(object value, string name)
#endif
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, string.Format(CultureInfo.CurrentCulture, Resources.UndefinedEnumException, value ?? Resources.NoValueSpecified, typeof(TEnum).FullName, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified)));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
        }

#if NET6_0_OR_GREATER
        public static IEnumerable<T> AgainstEmptyEnumerable<T>(IEnumerable<T> enumerable, [CallerArgumentExpression("enumerable")] string name = null)
#else
        public static IEnumerable<T> AgainstEmptyEnumerable<T>(IEnumerable<T> enumerable, string name)
#endif
        {
            if (enumerable == null)
            {
                throw new NullReferenceException(string.Format(CultureInfo.CurrentCulture, Resources.NullValueException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            if (!enumerable.Any())
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, string.Format(CultureInfo.CurrentCulture, Resources.EmptyEnumerableException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified)));
            }

            return enumerable;
        }
    }
}