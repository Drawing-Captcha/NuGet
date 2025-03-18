﻿using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Forms.Core.Providers;

namespace Dit.Umb.Delica.Web.App_Plugins.DrawingCaptcha
{
        public class FieldComposer : IComposer
        {
            public void Compose(IUmbracoBuilder builder)
            {
                builder.WithCollectionBuilder<FieldCollectionBuilder>()
                    .Add<DrawingCaptchaField>();
            }
        }
}
