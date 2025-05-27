
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Models;
using Umbraco.Forms.Core.Services;

namespace DrawingCaptcha
{
    public class DrawingCaptchaField : Umbraco.Forms.Core.FieldType
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        private string _apiKey;
        private string _serverURL;

        public DrawingCaptchaField(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            Id = new Guid("45beb78b-ca70-4d0c-bad2-231bf087c6fc");
            Name = "Drawing-Captcha-Field";
            Description = "Activate the Drawing Captcha on this form";
            Icon = "icon-shield";
            DataType = FieldDataType.String;
            SortOrder = 10;
            SupportsPreValues = false;
            FieldTypeViewName = "~/App_Plugins/Drawing_Captcha/FieldTypes/FieldType.DrawingCaptchaField.cshtml";
            HideLabel = true;

            _configuration = configuration;
            _httpClientFactory = httpClientFactory;

            InitializeConfigurationValues();
        }

        private void InitializeConfigurationValues()
        {
            _apiKey = _configuration.GetValue<string>("Umbraco:Forms:FieldTypes:DrawingCaptcha:APIKey");
            _serverURL = _configuration.GetValue<string>("Umbraco:Forms:FieldTypes:DrawingCaptcha:Server");
        }

        public override string GetDesignView() =>
            "~/App_Plugins/Drawing_Captcha/FieldTypes/drawingcaptchafield.html";

        public override IEnumerable<string> ValidateField(
            Form form,
            Field field,
            IEnumerable<object> postedValues,
            HttpContext context,
            IPlaceholderParsingService placeholderParsingService,
            IFieldTypeStorage fieldTypeStorage)
        {
            var errors = new List<string>();

            var token = postedValues.FirstOrDefault()?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                errors.Add("Captcha token is empty or invalid.");
                return errors;
            }

            var isValid = ValidateCaptchaFieldAsync(token, context).GetAwaiter().GetResult();
            if (!isValid)
            {
                errors.Add("Captcha Callback verification failed. Please try again.");
                return errors;
            }

            return base.ValidateField(form, field, postedValues, context, placeholderParsingService, fieldTypeStorage);
        }

        public async Task<bool> ValidateCaptchaFieldAsync(string token, HttpContext httpContext)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            try
            {
                var requestContext = httpContext.Request;
                string requestUrl = $"{_serverURL}/siteVerify/callback";
                string originUrl = $"{requestContext.Scheme}://{requestContext.Host}";
                var requestData = new
                {
                    apiKey = _apiKey,
                    token
                };

                var jsonContent = JsonConvert.SerializeObject(requestData);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
                {
                    Content = httpContent
                };

                request.Headers.Add("Origin", originUrl);

                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(responseBody);

                return captchaResponse.IsValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during API request: {ex.Message}");
                return false;
            }
        }


        private class CaptchaResponse
        {
            [JsonProperty("isValid")]
            public bool IsValid { get; set; }
        }
    }
}
