using Bogus;
using Bogus.Extensions.Brazil;
using CadastroNacional.PessoaFisica;
using Xunit;

namespace CadastroNacional.Test
{
    public class CpfTest
    {
        [Theory(DisplayName = "Formatar CPF com sucesso")]
        [InlineData("40012885835")]
        public void FormatarCpfComSucesso(string cpf)
        {
            var cpfFormatado = Cpf.Formatar(cpf, out var retorno);

            Assert.True(cpfFormatado);
            Assert.Equal("400.128.858-35", retorno);
        }

        [Theory(DisplayName = "Formatar CPF com falha")]
        [InlineData("400.128.858-35")]
        [InlineData("999999999999")]
        [InlineData("123")]
        [InlineData("ABC")]
        [InlineData("123ABC456")]
        [InlineData("")]
        [InlineData(null)]
        public void FormatarCpfComFalha(string cpf)
        {
            var cpfFormatado = Cpf.Formatar(cpf, out var retorno);

            Assert.False(cpfFormatado);
            Assert.Empty(retorno);
        }

        [Fact(DisplayName = "Gerar CPF não formatado com sucesso")]
        public void GerarComSucessoCpfNaoFormatado()
        {
            const int cpfSemFormatacao = 11;
            var cpf = Cpf.Novo(false);

            Assert.NotNull(cpf);
            Assert.NotEmpty(cpf);
            Assert.True(cpf?.Length == cpfSemFormatacao);
        }

        [Fact(DisplayName = "Gerar CPF formatado com sucesso")]
        public void GerarComSucessoCpfFormatado()
        {
            const int cpfComFormatacao = 14;
            var cpf = Cpf.Novo(true);

            Assert.NotNull(cpf);
            Assert.NotEmpty(cpf);
            Assert.True(cpf?.Length == cpfComFormatacao);
        }

        [Fact(DisplayName = "Validar CPF com sucesso")]
        public void ValidarCpfComSucesso()
        {
            var cpf = new Faker().Person.Cpf(false);

            var validacaoCpf = Cpf.EhValido(cpf);

            Assert.True(validacaoCpf);
        }

        [Theory(DisplayName = "Validar CPF com falha")]
        [InlineData("40012885831")]
        [InlineData("40012885832")]
        [InlineData("40012885833")]
        public void ValidarCpfComFalha(string cpf)
        {
            var validacaoCpf = Cpf.EhValido(cpf);

            Assert.False(validacaoCpf);
        }
    }
}
