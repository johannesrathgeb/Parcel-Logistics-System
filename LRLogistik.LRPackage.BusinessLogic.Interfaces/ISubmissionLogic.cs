using LRLogistik.LRPackage.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Interfaces
{
    public interface ISubmissionLogic
    {
        public Parcel SubmitParcel(Parcel parcel); 
    }
}
