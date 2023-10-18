﻿using Entities;

namespace ServiceContracts.DTO;

public class CountryAddRequest
{
    public string? CountryName { get; set; }

    public Country ToCountry() => new() { CountryName = CountryName };
}