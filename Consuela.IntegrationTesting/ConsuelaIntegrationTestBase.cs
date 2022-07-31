using Consuela.Entity;
using Consuela.Lib.Services;
using Consuela.Lib.Services.Dummy;
using Consuela.UnitTesting;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
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

        protected virtual IDateTimeService GetDateTimeService(DateTime? dateTime = null)
        {
            var dtm = dateTime.HasValue ? dateTime.Value : ThirtyFiveDaysAhead;

            var svc = new DateTimeServiceDummy();
            svc.SetDateTime(dtm);

            return svc;
        }

        protected AuditService GetAuditService(IProfile profile, IDateTimeService dateTimeService)
        {
            var fs = new FileService(dateTimeService);

            var audit = GetAuditService(profile, fs, dateTimeService);

            return audit;
        }

        protected AuditService GetAuditService()
        {
            //Profile is set by the Profile.json
            var p = GetDefaultProfile();

            var dtm = GetDateTimeService();

            var fs = new FileService(dtm);

            var audit = GetAuditService(p, fs, dtm);

            return audit;
        }

        protected AuditService GetAuditService(IProfile profile, IFileService fileService, IDateTimeService dateTimeService)
        {
            var excel = new ExcelFileWriterService();

            var audit = new AuditService(profile, fileService, excel, dateTimeService, NullLogger<AuditService>.Instance);

            return audit;
        }

        protected (IProfile, CleanUpService) GetCleanUpService()
        {
            //Profile is set by the Profile.json
            var p = GetDefaultProfile();

            var dtm = GetDateTimeService();

            var fs = new FileService(dtm);

            var audit = GetAuditService(p, fs, dtm);

            var svc = new CleanUpService(audit, fs);

            return (p, svc);
        }
    }
}
