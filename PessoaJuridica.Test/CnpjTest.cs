using CadastroNacional.PessoaJuridica;
using System;
using Xunit;

namespace PessoaJuridica.Test
{
    public class CnpjTest
    {
        [Fact(DisplayName = "Gerar com sucesso CPF não formatado")]
        public void GerarComSucessoCpfNaoFormatado()
        {
            const int tamanhoCnpjSemFormatacao = 15;
            var cnpj = Cnpj.Novo(false);

            Assert.NotNull(cnpj);
            Assert.NotEmpty(cnpj);
            Assert.True(cnpj?.Length == tamanhoCnpjSemFormatacao);
        }
    }
}
