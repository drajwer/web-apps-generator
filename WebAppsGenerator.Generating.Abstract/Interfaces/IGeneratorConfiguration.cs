using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface IGeneratorConfiguration
    {
        string ProjectName { get; }
        string OutputPath { get; }
    }
}
