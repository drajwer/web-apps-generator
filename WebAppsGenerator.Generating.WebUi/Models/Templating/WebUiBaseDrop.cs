﻿using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.WebUi.Services;

namespace WebAppsGenerator.Generating.WebUi.Models.Templating
{
    /// <summary>
    /// Drop only basic information about generated web UI project
    /// </summary>
    public class WebUiBaseDrop : BaseDrop
    {
        public WebUiBaseDrop(SolutionPathService pathService, IGeneratorConfiguration generatorConfiguration) : base(generatorConfiguration)
        {
            WebUiProjectName = pathService.WebUiProjectName;
        }

        public string WebUiProjectName { get; set; }
    }
}
