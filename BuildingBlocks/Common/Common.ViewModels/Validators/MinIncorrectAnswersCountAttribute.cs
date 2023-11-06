using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels.Validators
{
    public class MinIncorrectAnswersCountAttribute : ValidationAttribute
    {
        readonly int _count;

        public MinIncorrectAnswersCountAttribute(int count)
        {
            _count = count;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Invalid converting value");
            }

            var collection = value as IEnumerable<string>;
            if (collection != null && collection.Count() < _count)
                return new ValidationResult($"Collection has less than {_count} elements");

            return ValidationResult.Success;
        }
    }
}
