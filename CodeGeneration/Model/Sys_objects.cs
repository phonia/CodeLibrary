using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Domain;

namespace CodeGeneration.Model
{
    public partial class Sys_objects:EntityBase,IAggregateRoot
    {
        public override void Validate()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
