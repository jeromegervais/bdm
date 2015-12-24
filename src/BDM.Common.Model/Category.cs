﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Common.Model
{
    public class Category : IdObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
