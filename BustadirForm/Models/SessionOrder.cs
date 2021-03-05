using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BustadirForm.Models
{
    public class SessionOrder
    {
        public string Id { get; set; }
        public DateTime? Date { get; set; }
        public int OrderId { get; set; }
    }

    public class DibsPreOrderInfo
    {
        public string AcceptUrl { get; set; }
        public string AcceptReturnUrl { get; set; }
        public int Amount { get; set; }
        public int Currency { get; set; }
        public int OrderId { get; set; }
        public int Merchant { get; set; }
        public string EmailAddress { get; set; }
        public string MSG { get; set; }
    }
}