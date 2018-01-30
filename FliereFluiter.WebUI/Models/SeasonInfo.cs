using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using FliereFluiter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.WebUI.Models
{
    public class SeasonInfo
    {
        public int seasonId { get; set; }
        public int seasonDateId { get; set; }
        public string name { get; set; }
        [DataType(DataType.Date)]
        public DateTime beginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
        public double price { get; set; }
    }
}