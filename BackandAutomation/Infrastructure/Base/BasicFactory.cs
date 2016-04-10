using System;
using System.Linq;
using Core;

namespace Infrastructure.Base
{
    public abstract class BasicFactory<TEntity> : DriverUser
        where TEntity : class
    {
        protected BasicFactory(DriverUser driverUser) : base(driverUser)
        {
        }

        protected TInstance Create<TInstance>(params object[] parameters) where TInstance : TEntity
        {
            Type type = typeof (TInstance);
            CreationExtraLogic(type);
            if (parameters.Any())
                return (TInstance) Activator.CreateInstance(type, this, parameters);
            return (TInstance) Activator.CreateInstance(type, this);
        }

        protected virtual void CreationExtraLogic(Type type)
        {
        }
    }
}