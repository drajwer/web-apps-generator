using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.IntegrationTests.Extensions;
using WebAppsGenerator.IntegrationTests.Helpers;

namespace WebAppsGenerator.IntegrationTests
{
    [TestClass]
    public class SamplesTests : SamplesTestBase
    {
        [DataTestMethod]
        [DataRow("Library", "books")]
        [DataRow("Shop", "orders")]
        [DataRow("University", "students")]
        public async Task FullIntegrationTest(string directoryName, string sampleCollectionName)
        {
            await RunIntegrationTest(directoryName, sampleCollectionName);
        }    
    }
}
