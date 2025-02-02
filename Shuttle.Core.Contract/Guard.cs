using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

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

            Exception? exception;

            try
            {
                exception = Activator.CreateInstance(typeof(TException), !string.IsNullOrWhiteSpace(message) ? message : Resources.NoMessageSpecified) as TException;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(Resources.InvalidGuardExceptionType, typeof(TException).FullName, ex.Message), ex);
            }

            throw exception ?? new ApplicationException(message);
        }

        public static T AgainstNull<T>(T? value, [CallerArgumentExpression("value")] string? name = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Resources.NullValueException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            return value;
        }

        public static string AgainstNullOrEmptyString(string? value, [CallerArgumentExpression("value")] string? name = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Resources.EmptyStringException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            return value;
        }

        public static Guid AgainstEmptyGuid(Guid value, [CallerArgumentExpression("value")] string? name = null)
        {
            if (Guid.Empty.Equals(value))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.EmptyGuidException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            return value;
        }

        public static TEnum AgainstUndefinedEnum<TEnum>(object? value, [CallerArgumentExpression("value")] string? name = null)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, string.Format(CultureInfo.CurrentCulture, Resources.UndefinedEnumException, value, typeof(TEnum).FullName, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified)));
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value.ToString()!);
        }

        public static IEnumerable<T> AgainstEmptyEnumerable<T>(IEnumerable<T> enumerable, [CallerArgumentExpression("enumerable")] string? name = null)
        {
            if (enumerable == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.NullValueException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified));
            }

            if (!enumerable.Any())
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, string.Format(CultureInfo.CurrentCulture, Resources.EmptyEnumerableException, !string.IsNullOrWhiteSpace(name) ? name : Resources.NoNameSpecified)));
            }

            return enumerable;
        }
    }
}