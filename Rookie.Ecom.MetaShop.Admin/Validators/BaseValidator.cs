using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rookie.Ecom.Admin.Validators
{
    /// <summary>
    /// BaseValidator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        /// <summary>
        /// RuleFor.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public new IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
            => base.RuleFor(expression).Configure(ConfigurePropertyName);

        /// <summary>
        /// RuleForEach.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public new IRuleBuilderInitialCollection<T, TProperty> RuleForEach<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> expression)
            => base.RuleForEach(expression).Configure(ConfigurePropertyName);

        private static void ConfigurePropertyName(PropertyRule rule)
        {
            rule.MessageBuilder = context =>
            {
                context.MessageFormatter.AppendPropertyName(context.PropertyName);
                return context.GetDefaultMessage();
            };

        }
    }
}
