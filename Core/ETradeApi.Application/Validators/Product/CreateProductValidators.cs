using ETradeApi.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ETradeApi.Application.Validators.Product
{
    public class CreateProductValidators:AbstractValidator<CreateProductCommanRequest>
    {
        public CreateProductValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Ürün İsmi Boş Olamaz!!")
                .MaximumLength(150).MinimumLength(3).WithMessage("Ürün Uzunluğu İstenilen Aralıkta DDeğil!!");

            RuleFor(x => x.Stock)
                .NotEmpty().NotNull().WithMessage("Stok Değeri Boş Olamaz!!")
                .Must(x => x >= 0).WithMessage("Stok Değeri Negatif Olamaz!!");

            RuleFor(x => x.Price)
                .NotEmpty().NotNull().WithMessage("Fiyat Değeri Boş Olamaz!!")
                .Must(x => x >= 0).WithMessage("Fiyat Bilgisi Negatif Olamaz!!");

        }
    }
}
