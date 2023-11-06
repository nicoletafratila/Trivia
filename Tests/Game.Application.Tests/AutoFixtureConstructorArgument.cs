using System.Reflection;
using AutoFixture.Kernel;

namespace Game.Application.Tests
{
    public class AutoFixtureConstructorArgument : ISpecimenBuilder
    {
        private readonly object _value;

        private readonly string _name;

        private readonly IList<Type> _allowedTypes;

        public AutoFixtureConstructorArgument(IList<Type> allowedTypes, string name, object value)
        {
            _allowedTypes = allowedTypes;
            _name = name;
            _value = value;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (!(request is ParameterInfo pi))
            {
                return new NoSpecimen();
            }

            if (pi.Member.DeclaringType != null && !_allowedTypes.Contains(pi.Member.DeclaringType)
                || pi.ParameterType != _value.GetType()
                || pi.Name != _name)
            {
                return new NoSpecimen();
            }

            return _value;
        }
    }
}