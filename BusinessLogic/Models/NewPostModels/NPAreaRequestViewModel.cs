using Newtonsoft.Json;

namespace BusinessLogic.Models.NewPostModels
{
    
        public class NPAreaProperties
        {
            public int Page { get; set; }
            public string Ref { get; set; } = string.Empty;
        }

        public class NPAreaRequestViewModel
        {
            [JsonProperty(PropertyName = "apiKey")]
            public string ApiKey { get; set; }

            [JsonProperty(PropertyName = "modelName")]
            public string ModelName { get; set; }

            [JsonProperty(PropertyName = "calledMethod")]
            public string CalledMethod { get; set; }

            [JsonProperty(PropertyName = "methodProperties")]
            public NPAreaProperties MethodProperties { get; set; }
        }
    
}
