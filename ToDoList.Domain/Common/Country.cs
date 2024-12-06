using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Common
{
    public class Country : BaseDomainEntity
    {
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
