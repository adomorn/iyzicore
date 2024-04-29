﻿using Newtonsoft.Json;

namespace Iyzicore.Model;

public class InstallmentPrice
{
    [JsonProperty(PropertyName = "InstallmentPrice")]
    public string Price { get; set; }

    public string TotalPrice { get; set; }
    public int? InstallmentNumber { get; set; }
}