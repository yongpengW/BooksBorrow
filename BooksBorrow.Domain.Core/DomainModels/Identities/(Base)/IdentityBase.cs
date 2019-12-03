using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.DomainModels.Identities
{
    [Serializable]
    public class IdentityBase
    {
        protected Guid Id { get; private set; }
        protected List<Guid> IdList { get; set; }
        protected List<string> Value { get; set; }

        protected object Object { get; set; }
        public Guid GetPersistId()
        {
            return this.Id;
        }
        public IdentityBase()
        {
            this.IdList = new List<Guid>();
        }
        public List<Guid> GetListPersistId()
        {
            return this.IdList;
        }

        public List<string> GetListValue()
        {
            return this.Value;
        }
        public void AddObject(object value)
        {
            this.Object = value;
        }

        public object getObjectValue()
        {
            return this.Object;
        }

        public void AddValue(List<string> value)
        {
            this.Value = value;
        }
        public void AddValueItem(string value)
        {
            if (Value == null)
            {
                Value = new List<string>();
            }
            this.Value.Add(value);
        }
        public IdentityBase(Guid id)
        {
            List<Guid> IdList = new List<Guid>();

            this.Id = id;
            IdList.Add(id);

        }
        public void AddId(Guid id)
        {
            Id = id;
            this.IdList.Add(id);
        }

        public void AddIds(List<Guid> ids)
        {
            Id = ids[0];
            this.IdList.AddRange(ids);
        }
        public IdentityBase(List<Guid> ids)
        {
            this.IdList = ids;
        }
        public override string ToString()
        {
            return this.Id.ToString();
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (Object.Equals(obj, null))
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Id == ((IdentityBase)obj).Id;
        }

        public static bool operator ==(IdentityBase leftValue, IdentityBase rightValue)
        {
            if (Object.Equals(leftValue, null))
            {
                return Object.Equals(rightValue, null);
            }

            return leftValue.Equals(rightValue);
        }

        public static bool operator !=(IdentityBase leftValue, IdentityBase rightValue)
        {
            return !(leftValue == rightValue);
        }
    }
}
