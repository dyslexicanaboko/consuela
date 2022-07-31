using Consuela.Entity;
using NUnit.Framework;
using System;
using System.IO;

namespace Consuela.IntegrationTesting
{
    [TestFixture]
    public class AuditServiceTests
        : ConsuelaIntegrationTestBase
    {
        [Test]
        public void Generate_log_files()
        {
            var p = GetDefaultProfile();

            p.Audit.Path = Path.Combine(BaseDirectory, "Audit");

            var dtm = GetDateTimeService(DateTime.Now);

            var svc = GetAuditService(p, dtm);

            var fake = new FileInfoEntity
            {
                CreationTime = dtm.Now,
                DirectoryName = BaseDirectory,
                FullName = BaseDirectory + "\\A.txt",
                Name = "A.txt"
            };

            for (int i = 0; i < 10; i++)
            {
                svc.LogFile(fake);

                svc.LogDirectory(BaseDirectory);
            }

            svc.SaveLog();
        }
    }
}
