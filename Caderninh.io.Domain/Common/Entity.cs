using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caderninh.io.Domain.Common
{
    public class Entity
    {
        public Guid Id { get; init; }

        protected Entity(Guid id) => Id = id;

        protected Entity()
        {
        }
    }
}