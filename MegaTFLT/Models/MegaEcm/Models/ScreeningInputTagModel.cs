using System;

namespace MegaTFLT.Models.MegaEcm.Models
{
    public class ScreeningInputTagModel : IEquatable<ScreeningInputTagModel>
    {

        public string Input { get; set; }

        public string TagName { get; set; }

        public bool Equals(ScreeningInputTagModel other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Input.Equals(other.Input) && TagName.Equals(other.TagName);
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashInput = Input == null ? 0 : Input.GetHashCode();

            //Get hash code for the Code field.
            int hashTagName = TagName == null ? 0 : TagName.GetHashCode();

            //Calculate the hash code for the product.
            return hashInput ^ hashTagName;
        }
    }
}