using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Shuttle.Core.Contract.Tests
{
    [TestFixture]
    public class GuardFixture
    {
        public enum Level
        {
            One = 1,
            Two = 2,
            Three = 3
        }

        [Test]
        public void Should_be_able_to_guard_against()
        {
            Assert.That(() => Guard.Against<ApplicationException>(false, "some-message"), Throws.Nothing);
            Assert.That(() => Guard.Against<ApplicationException>(true, "some-message"),
                Throws.TypeOf<ApplicationException>());
            Assert.That(() => Guard.Against<ApplicationException>(true, null), Throws.TypeOf<ApplicationException>());
        }

        [Test]
        public void Should_be_able_to_guard_against_null()
        {
            var o = new object();

            Assert.That(() => Guard.AgainstNull(o, nameof(o)), Throws.Nothing);
            Assert.That(() => Guard.AgainstNull(o, null), Throws.Nothing);
            Assert.That(() => Guard.AgainstNull(o, string.Empty), Throws.Nothing);

            o = null;

            Assert.That(() => Guard.AgainstNull(o, nameof(o)), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNull(o, null), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNull(o, string.Empty), Throws.TypeOf<NullReferenceException>());

            var item = new FixtureItem
            {
                Name = "unit-test"
            };

            Assert.That(() => Guard.AgainstNull(item, nameof(item)), Throws.Nothing);
            Assert.That(() => Guard.AgainstNull(item, null), Throws.Nothing);
            Assert.That(() => Guard.AgainstNull(item, string.Empty), Throws.Nothing);
            Assert.That(Guard.AgainstNull(item, nameof(item)).Name, Is.EqualTo("unit-test"));

            item = null;

            Assert.That(() => Guard.AgainstNull(item, nameof(item)), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNull(item, null), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNull(item, string.Empty), Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void Should_be_able_to_guard_against_null_or_empty_string()
        {
            var s = "value";

            Assert.That(() => Guard.AgainstNullOrEmptyString(s, nameof(s)), Throws.Nothing);
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, null), Throws.Nothing);
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, string.Empty), Throws.Nothing);
            Assert.That(Guard.AgainstNullOrEmptyString(s, nameof(s)), Is.EqualTo("value"));

            s = null;

            Assert.That(() => Guard.AgainstNullOrEmptyString(s, nameof(s)), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, null), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, string.Empty), Throws.TypeOf<NullReferenceException>());

            s = string.Empty;

            Assert.That(() => Guard.AgainstNullOrEmptyString(s, nameof(s)), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, null), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, string.Empty), Throws.TypeOf<NullReferenceException>());

            s = "   ";

            Assert.That(() => Guard.AgainstNullOrEmptyString(s, nameof(s)), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, null), Throws.TypeOf<NullReferenceException>());
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, string.Empty), Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void Should_be_able_to_guard_against_undefined_enum()
        {
            var level = Level.One;

            Assert.That(() => Guard.AgainstUndefinedEnum<Level>(level, nameof(level)), Throws.Nothing);
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>(level, null), Throws.Nothing);
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>(level, string.Empty), Throws.Nothing);
            Assert.That(Guard.AgainstUndefinedEnum<Level>(level, nameof(level)), Is.EqualTo(Level.One));

            Assert.That(() => Guard.AgainstUndefinedEnum<Level>(1, nameof(level)), Throws.Nothing);
            Assert.That(Guard.AgainstUndefinedEnum<Level>(1, nameof(level)), Is.EqualTo(Level.One));

            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("One", nameof(level)), Throws.Nothing);
            Assert.That(Guard.AgainstUndefinedEnum<Level>("One", nameof(level)), Is.EqualTo(Level.One));

            Assert.That(() => Guard.AgainstUndefinedEnum<Level>(4, nameof(level)), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("Four", nameof(level)), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("Four", null), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("Four", string.Empty), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Should_be_able_to_guard_against_an_empty_enumerable()
        {
            Assert.That(() => Guard.AgainstEmptyEnumerable(new List<string>(), "list"), Throws.TypeOf<InvalidOperationException>());

            var list = new List<string>
            {
                "item-one"
            };

            Assert.That(() => Guard.AgainstEmptyEnumerable(list, nameof(list)), Throws.Nothing);
            Assert.That(Guard.AgainstEmptyEnumerable(list, nameof(list)).ElementAt(0), Is.EqualTo("item-one"));
        }

        [Test]
        public void Should_be_able_to_guard_against_empty_guid()
        {
            Assert.That(() => Guard.AgainstEmptyGuid(Guid.Empty, string.Empty), Throws.TypeOf<ArgumentException>());

            var guid = Guid.NewGuid();

            Assert.That(() => Guard.AgainstEmptyGuid(guid, nameof(guid)), Throws.Nothing);
            Assert.That(Guard.AgainstEmptyGuid(guid, nameof(guid)), Is.EqualTo(guid));
        }
    }
}