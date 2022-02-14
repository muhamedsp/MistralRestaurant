using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MeasureType
    {
        mL,

        L,

        g,

        Kg,

        unit
    }
}
