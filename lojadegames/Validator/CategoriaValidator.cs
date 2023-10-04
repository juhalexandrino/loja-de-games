using FluentValidation;
using LojaDeGames.Model;

namespace LojaDeGames.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator() {
            RuleFor(p => p.Tipo)
                    .NotEmpty()
                    .MinimumLength(2)    
                    .MaximumLength(1000);
        }
        
    }
}
