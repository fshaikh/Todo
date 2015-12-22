using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class BaseCollection<T> :List<T> where T:DataObjectBase
    {
    }
}
