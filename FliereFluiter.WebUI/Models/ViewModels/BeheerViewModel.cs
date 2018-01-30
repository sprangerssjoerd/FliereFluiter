using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using FliereFluiter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.WebUI.Models
{
    public class BeheerViewModel
    {
        public List<SeasonInfo> seasonInfoList;
        public SeasonInfo seasonInfo { get; set; }
    }
}