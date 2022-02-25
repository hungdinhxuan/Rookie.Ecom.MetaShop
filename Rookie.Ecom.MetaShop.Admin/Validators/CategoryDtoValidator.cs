using FluentValidation;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Business.Services;
using Rookie.Ecom.MetaShop.Contracts.Constants;
using Rookie.Ecom.MetaShop.Contracts.Dtos;

namespace Rookie.Ecom.Admin.Validators
{
    public class CategoryDtoValidator : BaseValidator<CategoryDto>
    {
        public CategoryDtoValidator(ICategoryService categoryService)
        {
            RuleFor(m => m.Id)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));

            RuleFor(m => m.Name)
                  .NotEmpty()
                  .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Name)));

            RuleFor(m => m.Name)
               .MaximumLength(ValidationRules.CategoryRules.MaxLenghCharactersForName)
               .WithMessage(string.Format(ErrorTypes.Common.MaxLengthError, ValidationRules.CategoryRules.MaxLenghCharactersForName))
               .When(m => !string.IsNullOrWhiteSpace(m.Name));

            RuleFor(m => m.Desc)
               .MaximumLength(ValidationRules.CategoryRules.MaxLenghCharactersForDesc)
               .WithMessage(string.Format(ErrorTypes.Common.MaxLengthError, ValidationRules.CategoryRules.MaxLenghCharactersForDesc))
               .When(m => !string.IsNullOrWhiteSpace(m.Desc));

            RuleFor(x => x).MustAsync(
             async (dto, cancellation) =>
             {
                 var exit = await categoryService.GetByNameAsync(dto.Name);
                 return exit == null || exit.Id == dto.Id;
             }
          ).WithMessage("Duplicate record")
          .When(m => !string.IsNullOrWhiteSpace(m.Name));
        }
    }
}
