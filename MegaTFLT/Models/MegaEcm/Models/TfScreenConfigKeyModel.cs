using System;

namespace MegaTFLT.MegaEcm.Models
{
    public class TfScreenConfigKeyModel : IEquatable<TfScreenConfigKeyModel>
    {
        public TfScreenConfigKeyModel(int messageSourceCode, string tagName, string entityType)
        {
            this.MessageSourceCode = messageSourceCode;
            this.TagName = tagName;
            this.EntityType = entityType;
            //Console.WriteLine($"{messageSourceCode},{tagName},{entityType}");
        }
        public int MessageSourceCode { get; private set; }
        public string TagName { get; private set; }
        public string EntityType { get; private set; }
        public bool Equals(TfScreenConfigKeyModel other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            bool isEqualMessageSourceCode = MessageSourceCode == other.MessageSourceCode;
            bool isEqualTagName = (TagName == null && other.TagName == null) || TagName.Equals(other.TagName);
            bool isEqualEntityType = (EntityType == null && other.EntityType == null) || EntityType.Equals(other.EntityType);
            return isEqualMessageSourceCode && isEqualTagName && isEqualEntityType;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            //Get hash code for the Code field.
            int hashTagName = TagName == null ? 0 : TagName.GetHashCode();
            //Get hash code for the Code field.
            int hashEntityType = EntityType == null ? 0 : EntityType.GetHashCode();
            //Calculate the hash code for the product.
            return MessageSourceCode ^ hashTagName ^ hashEntityType;
        }
    }
}