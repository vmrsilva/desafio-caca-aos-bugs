using Balta.Domain.SharedContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Domain.Test.Mocks.DateTimeProvider
{
    internal class FakeDateTimeProvider
    : IDateTimeProvider
    {
        public static IDateTimeProvider Default { get; } = new FakeDateTimeProvider();

        private DateTime? _staticDateTime;
        public DateTime UtcNow => _staticDateTime ?? DateTime.UtcNow;

        public FakeDateTimeProvider(DateTime? staticDate)
        {
            _staticDateTime = staticDate;
        }

        public FakeDateTimeProvider()
            : this(null)
        {
        }

        public void ChangeDate(DateTime dateTime)
        {
            _staticDateTime = dateTime;
        }
    }
}
