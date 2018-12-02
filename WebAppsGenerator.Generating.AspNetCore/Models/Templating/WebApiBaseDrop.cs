using System;
using System.Collections.Generic;
using System.Text;
using DotLiquid;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.AspNetCore.Services;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    public class WebApiBaseDrop : BaseDrop
    {
        public WebApiBaseDrop(SolutionPathService pathService, IGeneratorConfiguration generatorConfiguration): base(generatorConfiguration)
        {
            CoreProjectName = pathService.CoreProjectName;
            WebApiProjectName = pathService.WebApiProjectName;
        }

        public string CoreProjectName { get; set; }
        public string WebApiProjectName { get; set; }

    }
}
