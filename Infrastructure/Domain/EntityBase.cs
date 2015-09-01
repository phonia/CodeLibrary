using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Domain
{
    public abstract class EntityBase
    {
        private List<BusinessRule> m_brokenRules = new List<BusinessRule>();

        public abstract void Validate();

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            m_brokenRules.Clear();
            Validate();
            return m_brokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            m_brokenRules.Add(businessRule);
        }

        public abstract override int GetHashCode();

        public abstract override bool Equals(object entity);

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

            return entity1.Equals(entity2);
        }

        public static bool operator !=(EntityBase entity1, EntityBase entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
