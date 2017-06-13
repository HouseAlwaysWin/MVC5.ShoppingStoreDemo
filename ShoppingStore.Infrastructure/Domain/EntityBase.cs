using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Infrastructure.Domain
{
    public abstract class EntityBase<TId> : IEquatable<EntityBase<TId>>
    {
        private readonly TId id;

        protected EntityBase(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new ArgumentException("This ID cannot be the default value.", "id");
            }
        }

        public TId Id
        {
            get { return id; }
        }

        public override bool Equals(object obj)
        {
            var entity = obj as EntityBase<TId>;
            if (entity != null)
            {
                return Equals(entity);
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(EntityBase<TId> other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id);
        }
    }
}
