using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Abstract
{
    public interface ICampingFieldRepository
    {
        IEnumerable<CampingField> CampingFields { get; }

        CampingField getCampingFieldById(int campingFieldId);
        ArrayList isDateInBetween(DateTime beginDateToCheck, DateTime endDateToCheck, int campingfieldId);
    }

}
