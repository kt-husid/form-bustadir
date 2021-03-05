﻿using BustadirForm.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BustadirForm.Models
{
    [DataContract]
    public partial class Country
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "CountryCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string Code { get; set; }

        [DataMember]
        [Display(Name = "Country", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; }
    }
}