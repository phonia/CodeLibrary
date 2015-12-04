using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFInfrastructure.Domain
{
    public abstract class EntityBase
    {
        private List<BusinessRules> _brokenRules = new List<BusinessRules>();

        public abstract void Validate();

        public IEnumerable<BusinessRules> GetBrokenRules()
        {
            if (_brokenRules == null) _brokenRules = new List<BusinessRules>();
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(BusinessRules businessRule)
        {
            if (_brokenRules == null) _brokenRules = new List<BusinessRules>();
            _brokenRules.Add(businessRule);
        }

        public abstract override int GetHashCode();

        public abstract override bool Equals(object obj);

        public static bool operator ==(EntityBase entity1, EntityBase entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.GetType() != entity2.GetType())
            {
                return false;
            }

            if (entity1.Equals(entity2))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(EntityBase entity1, EntityBase entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
