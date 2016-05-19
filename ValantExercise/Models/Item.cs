using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ValantExercise.Models
{
    /// <summary>
    /// This class represents the domain item that we are concerned with.
    /// </summary>
    public class Item
    {
        [JsonProperty(PropertyName = "label")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Label is required.")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "expiration")]
        public DateTime Expiration { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        public bool Expired { get; set; } = false;
    }
}