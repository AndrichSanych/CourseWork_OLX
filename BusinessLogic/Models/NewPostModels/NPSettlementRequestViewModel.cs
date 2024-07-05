using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.NewPostModels
{
    internal class NPSettlementRequestViewModel
    {
        [JsonProperty(PropertyName = "apiKey")]
        public string ApiKey { get; set; }

        [JsonProperty(PropertyName = "modelName")]
        public string ModelName { get; set; }

        [JsonProperty(PropertyName = "calledMethod")]
        public string CalledMethod { get; set; }

        [JsonProperty(PropertyName = "methodProperties")]
        public NPSettlementProperties MethodProperties { get; set; }
    }

    public class NPSettlementProperties
    {
        /// <summary>
        /// Номер сторінки
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// Наявність нової пошти в населеному пункті
        /// </summary>
        public int Warehouse { get; set; } = 1;
        /// <summary>
        /// Кількість населених пунктів за 1 запит
        /// </summary>
        public int Limit { get; set; } = 200;
    }
}
