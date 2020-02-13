using System;
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
        }

        [Test]
        public void Should_be_able_to_guard_against_null_or_empty_string()
        {
            var s = "value";

            Assert.That(() => Guard.AgainstNullOrEmptyString(s, nameof(s)), Throws.Nothing);
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, null), Throws.Nothing);
            Assert.That(() => Guard.AgainstNullOrEmptyString(s, string.Empty), Throws.Nothing);

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
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>(1, nameof(level)), Throws.Nothing);
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("One", nameof(level)), Throws.Nothing);

            Assert.That(() => Guard.AgainstUndefinedEnum<Level>(4, nameof(level)), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("Four", nameof(level)), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("Four", null), Throws.TypeOf<InvalidOperationException>());
            Assert.That(() => Guard.AgainstUndefinedEnum<Level>("Four", string.Empty), Throws.TypeOf<InvalidOperationException>());
        }
    }
}