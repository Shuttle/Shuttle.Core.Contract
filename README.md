# Shuttle.Core.Contract

```
PM> Install-Package Shuttle.Core.Contract
```


A guard implementation that performs assertions/assumptions to prevent invalid code execution.

# Guard

``` c#
void Against<TException>(bool assertion, string message) where TException : Exception
```

Throws exception `TException` with the given `message` if the `assertion` is false.  If exception type `TException` does not have a constructor that accepts a `message` then an `InvalidOperationException` is thrown instead.

``` c#
void AgainstNull(object value, string name)
```

Throws a `NullReferenceException` if the given `value` is `null`.

``` c#
void AgainstNullOrEmptyString(string value, string name)
```

<<<<<<< HEAD
Throws a `NullReferenceException` if the given `value` is `null` or empty.

``` c#
void AgainstUndefinedEnum<TEnum>(object value, string name)
```

Throws an `InvalidOperationException` if the given `value` is not defined for enumeration type `TEnum`.
=======
Throws a `NullReferenceException` is the given `value` is `null` or empty.
>>>>>>> f144f3a0ed8a8cb705b0b8077029c767d10cd846
