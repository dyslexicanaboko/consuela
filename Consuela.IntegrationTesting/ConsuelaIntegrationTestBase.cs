using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.Dummy;
using Consuela.UnitTesting;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System.IO;

namespace Consuela.IntegrationTesting
{
    public class ConsuelaIntegrationTestBase
        : ConsuelaTestBase
    {
        protected override string BaseDrive { get; set; } = "C:";

        //Leave off trailing backslash for comparison purposes
        protected override string BaseDirectory { get; set; } = @"C:\Dump\IntegrationTests";
        
        protected virtual string EntitySpacesCodeGen { get; set; } = @"C:\Dump\IntegrationTests\EntitySpacesCodeGen";

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(BaseDirectory);

            Directory.CreateDirectory(EntitySpacesCodeGen);
        }

        protected virtual IDateTimeService GetDateTimeService()
        {
            var svc = new DateTimeServiceDummy();
            svc.SetDateTime(ThirtyFiveDaysAhead);

            return svc;
        }

        protected (IProfile, CleanUpService) GetCleanUpService()
        {
            //Profile is set by the Profile.json
            var p = GetDefaultProfile();

            var dtm = GetDateTimeService();

            var fs = new FileService(dtm);

            var audit = new AuditService(p, fs, dtm, NullLogger<AuditService>.Instance);

            var svc = new CleanUpService(audit, fs);

            return (p, svc);
        }
    }
}
