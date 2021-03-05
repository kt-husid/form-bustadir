using BustadirForm.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BustadirForm.Models
{
    /// <summary>
    /// REG_NR
    /// </summary>
    [DataContract]
    public class Bank
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        // AKA: Bank Code
        [DataMember]
        [Display(Name = "BankCode", ResourceType = typeof(BootstrapResources.Resources))]
        public int RegNumber { get; set; }

        [DataMember]
        [Display(Name = "Name", ResourceType = typeof(BootstrapResources.Resources))]
        public string Name { get; set; }
    }
}