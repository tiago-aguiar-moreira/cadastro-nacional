using Bogus;
using Bogus.Extensions.Brazil;
using CadastroNacional.PessoaJuridica;
using Xunit;

namespace CadastroNacional.Test
{
    public class CnpjTest
    {
        [Theory(DisplayName = "Formatar CNPJ com sucesso")]
        [InlineData("11444777000161")]
        public void FormatarCnpjComSucesso(string cnpj)
        {
            var cnpjFormatado = Cnpj.Formatar(cnpj, out var retorno);

            Assert.True(cnpjFormatado);
            Assert.Equal("11.444.777/0001-61", retorno);
        }

        [Theory(DisplayName = "Formatar CNPJ com falha")]
        [InlineData("11.444.777/0001-62")]
        [InlineData("999999999")]
        [InlineData("123")]
        [InlineData("ABC")]
        [InlineData("123ABC456")]
        [InlineData("")]
        [InlineData(null)]
        public void FormatarCnpjComFalha(string cnpj)
        {
            var cnpjFormatado = Cnpj.Formatar(cnpj, out var retorno);

            Assert.False(cnpjFormatado);
            Assert.Empty(retorno);
        }

        [Fact(DisplayName = "Gerar CNPJ não formatado com sucesso")]
        public void GerarCnpjNaoFormatadoComSucesso()
        {
            const int tamanhoCnpjSemFormatacao = 14;
            var cnpj = Cnpj.Novo(false);

            Assert.NotNull(cnpj);
            Assert.NotEmpty(cnpj);
            Assert.True(cnpj?.Length == tamanhoCnpjSemFormatacao);
        }

        [Fact(DisplayName = "Gerar CNPJ formatado com sucesso")]
        public void GerarComSucessoCnpjFormatado()
        {
            const int tamanhoCnpjSemFormatacao = 18;
            var cnpj = Cnpj.Novo(true);

            Assert.NotNull(cnpj);
            Assert.NotEmpty(cnpj);
            Assert.True(cnpj?.Length == tamanhoCnpjSemFormatacao);
        }
        
        [Fact(DisplayName = "Validar CNPJ com sucesso")]
        public void ValidarCnpjComSucesso()
        {
            var cnpj = new Faker().Company.Cnpj(false);

            var validacaoCnpj = Cnpj.EhValido(cnpj);

            Assert.True(validacaoCnpj);
        }

        [Theory(DisplayName = "Validar CNPJ com falha")]
        [InlineData("11444777000162")]
        [InlineData("11444777000163")]
        [InlineData("11444777000164")]
        public void ValidarCnpjComFalha(string cnpj)
        {
            var validacaoCnpj = Cnpj.EhValido(cnpj);

            Assert.False(validacaoCnpj);
        }
    }
}
