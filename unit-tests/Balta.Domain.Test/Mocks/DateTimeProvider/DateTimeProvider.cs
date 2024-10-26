using Balta.Domain.SharedContext.Abstractions;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Domain.Test.Mocks.DateTimeProvider
{
    public static class DateTimeProviderFake
    {
        public static IDateTimeProvider Create(DateTime? dateTime = null)
        {
            var dateTimeProviderFake = A.Fake<IDateTimeProvider>();

            var dateTimeUtcNow = dateTime?.ToUniversalTime() ?? DateTime.UtcNow;

            A.CallTo(() => dateTimeProviderFake.UtcNow).Returns(dateTimeUtcNow);

            return dateTimeProviderFake;
        }
    }
}
