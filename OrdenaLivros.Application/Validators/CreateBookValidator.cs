using FluentValidation;
using OrdenaLivros.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenaLivros.Application.Validators
{
    public class CreateBookValidator : AbstractValidator<BookModel>
    {
        public CreateBookValidator()
        {
            RuleFor(book => book.Title)
           .NotEmpty().WithMessage("O título é obrigatório.")
           .Must(title => !string.IsNullOrWhiteSpace(title)).WithMessage("O título não pode ser apenas espaços.");

            RuleFor(book => book.AuthorName)
                .NotEmpty().WithMessage("O nome do autor é obrigatório.")
                .Must(author => !string.IsNullOrWhiteSpace(author)).WithMessage("O nome do autor não pode ser apenas espaços.");

            RuleFor(book => book.EditionYear)
                .GreaterThan(0).WithMessage("O ano da edição deve ser maior que zero.");
        }
    }
}
