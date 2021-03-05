using BustadirForm.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BustadirForm.Models
{
    [DataContract]
    public partial class PersonTitle
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        [Required(ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "Required")]
        public string Description { get; set; }
    }
}