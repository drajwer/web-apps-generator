using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Tests.Mocks
{
    public class BaseGeneratorMock : BaseGenerator
    {
        public new FileServiceMock FileService { get; }

        public BaseGeneratorMock(IGeneratorConfiguration generatorConfiguration, IDropFactory dropFactory, ITemplatingConfigProvider configProvider, FileServiceMock fileService) : base(generatorConfiguration, dropFactory, configProvider, fileService)
        {
            FileService = fileService;
        }
    }
}
