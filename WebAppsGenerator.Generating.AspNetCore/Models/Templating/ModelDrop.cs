using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models.Templating;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    public class ModelDrop : EntityDrop
    {
        public bool IsJoinModel { get;  }
        public ModelDrop(Entity entity, bool isJoinModel = false) : base(entity)
        {
            IsJoinModel = isJoinModel;
        }
    }
}
