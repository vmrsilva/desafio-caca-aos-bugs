using Balta.Domain.SharedContext.Abstractions;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Domain.Test.Mocks.DateTimeProvider
{
    internal class DateTimeProvider
    : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
