using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace Infrastructure.Base
{
    public abstract class BasicFactory<TEntity> : DriverUser
        where TEntity : class
    {
        protected BasicFactory(DriverUser driverUser) : base(driverUser)
        {
            InitClasses();
        }

        protected abstract void InitClasses();

        private List<Type> RegisteredImplementations { get; } = new List<Type>();

        protected void RegisterClass(Type requestStrategyImpl)
        {
            if (!requestStrategyImpl.IsSubclassOf(typeof(TEntity)))
                throw new Exception("Class must inherit from the abstract Class");

            RegisteredImplementations.Add(requestStrategyImpl);
        }

        protected TInstance Create<TInstance>(params object[] parameters) where TInstance : TEntity
        {
            //typeof (TInstance)
            Type type = RegisteredImplementations.FirstOrDefault(impl => impl == typeof(TInstance));
            CreationExtraLogic(type);
            if (type == null)
                throw new Exception("Could not find a SignInForm implementation for this SignInFormType");
            if(parameters.Any())
                return (TInstance)Activator.CreateInstance(type, this, parameters);
            return (TInstance)Activator.CreateInstance(type, this);
        }

        protected virtual void CreationExtraLogic(Type type)
        {
        }
    }
}