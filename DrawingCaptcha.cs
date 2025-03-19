using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Models;
using Umbraco.Forms.Core.Services;


namespace Dit.Umb.Delica.Web.App_Plugins.DrawingCaptcha
{
    public class DrawingCaptchaField : Umbraco.Forms.Core.FieldType
    {
        public DrawingCaptchaField()
        {
            Id = new Guid("45beb78b-ca70-4d0c-bad2-231bf087c6fc");
            Name = "Drawing-Captcha-Field";
            Description = "Activate the Drawing Captcha on this form";
            Icon = "icon-shield";
            DataType = FieldDataType.Bit;
            SortOrder = 10;
            SupportsPreValues = false;
            FieldTypeViewName = "~/App_Plugins/DrawingCaptcha/FieldTypes/FieldType.DrawingCaptchaField.cshtml";
            HideLabel = true;
        }

        public override string GetDesignView() =>
            "~/App_Plugins/Drawing_Captcha/FieldTypes/drawingcaptchafield.html";
    }
}
