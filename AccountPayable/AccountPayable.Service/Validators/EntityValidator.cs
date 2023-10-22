using AccountPayable.Core.Interfaces;

namespace AccountPayable.Service.Validators
{
    public class EntityValidator<T>  where T : IEntity
	{
        private readonly Func<IEntity, bool> validation;

        public EntityValidator(Func<IEntity, bool> validation)
		{
            this.validation = validation;
        }

        public bool IsValid(IEntity entity)
        {
            if (validation == null)
                throw new InvalidOperationException("Validation cannot be null");


            return validation.Invoke(entity);
        }
	}
}

