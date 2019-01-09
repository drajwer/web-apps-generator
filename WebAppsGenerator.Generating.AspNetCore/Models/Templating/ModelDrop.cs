﻿using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generating.AspNetCore.Models.Templating
{
    public class ModelDrop : AnnotatedEntityDrop
    {
        public bool IsJoinModel { get;  }
        public ModelDrop(Entity entity, bool isJoinModel = false) : base(entity)
        {
            IsJoinModel = isJoinModel;
        }
    }
}
