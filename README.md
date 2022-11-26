# Shuttle.Core.Contract

```
PM> Install-Package Shuttle.Core.Contract
```


A guard implementation that performs assertions/assumptions to prevent invalid code execution.

# Guard

```c#
void Against<TException>(bool assertion, string message) 
	where TException : Exception
```

Throws exception `TException` with the given `message` if the `assertion` is false.  If exception type `TException` does not have a constructor that accepts a `message` then an `InvalidOperationException` is thrown instead.

---

```c#
T AgainstNull<T>(T value, string name)
```

Throws a `NullReferenceException` if the given `value` is `null`; else returns the `value`.

---

```c#
string AgainstNullOrEmptyString(string value, string name)
```

Throws a `NullReferenceException` if the given `value` is `null` or empty/whitespace; else returns the `value`.

---

```c#
TEnum AgainstUndefinedEnum<TEnum>(object value, string name)
```

Throws an `InvalidOperationException` if the provided `value` cannot be found in the given `TEnum`; else returns the `value` as `TEnum`.

---

```c#
IEnumerable<T> AgainstEmptyEnumerable<T>(IEnumerable<T> enumerable, string name)
```

Throws an `InvalidOperationException` if the given `enumerable` does not contain any entries; else returns the `enumerable`.

---

```c#
public static void AgainstEmptyGuid(Guid value, string name)
```

Throws and `ArgumentException` when the `value` is equal to an empty `Guid` (`{00000000-0000-0000-0000-000000000000}`); else returns the `value`.