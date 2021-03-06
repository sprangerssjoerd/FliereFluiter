﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Abstract
{
    public interface ISeasonRepository
    {
        IEnumerable<Season> Seasons { get; }
        Season getSeasonById(int id);
        IEnumerable<Season> getSeasons();
        void UpdateSeason(Season season);
        Season AddSeason(Season season);
    }

}
