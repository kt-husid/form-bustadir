using BustadirForm.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BustadirForm.Models
{
    [DataContract]
    public partial class PersonGender
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Code { get; set; }

        [DataMember]
        public String Description { get; set; }
    }
}