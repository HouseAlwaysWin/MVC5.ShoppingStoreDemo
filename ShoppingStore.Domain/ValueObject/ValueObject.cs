using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Domain.ValueObject
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        // Check attributes are same in two obj
        protected abstract IEnumerable<object> AttributesInEqualityCheck();
        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public virtual bool Equals(T obj)
        {
            if (obj == null)
            {
                return false;
            }
            return AttributesInEqualityCheck().SequenceEqual(obj.AttributesInEqualityCheck());
        }


        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(Equals(left, right));
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var obj in AttributesInEqualityCheck())
            {
                hash = hash * 31 + (obj == null ? 0 : obj.GetHashCode());
            }

            return hash;
            return base.GetHashCode();
        }
    }
}
