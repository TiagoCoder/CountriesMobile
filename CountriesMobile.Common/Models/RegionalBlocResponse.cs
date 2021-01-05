﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CountriesMobile.Common.Models
{
    public class RegionalBlocResponse
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public List<string> OtherAcronyms { get; set; }
        public List<string> OtherNames { get; set; }
    }
}
