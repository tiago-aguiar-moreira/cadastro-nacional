using System;
using CadastroNacional.PessoaFisica;
using CadastroNacional.PessoaJuridica;

namespace CadastroNacional.Exemplos
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CNPJ
            Console.WriteLine(">>> CNPJ ");
            Console.WriteLine();

            var cnpjNovoFormatado = Cnpj.Novo(true);
            Console.WriteLine("CNPJ gerado de forma aleatória (Formatado)");
            Console.WriteLine(cnpjNovoFormatado);
            Console.WriteLine();

            var cnpjNovoNaoFormatado = Cnpj.Novo(false);
            Console.WriteLine("CNPJ gerado de forma aleatória (Não formatado)");
            Console.WriteLine(cnpjNovoNaoFormatado);
            Console.WriteLine();

            const string cnpjValido = "33741488000167";
            Console.WriteLine("Validando CNPJ válido");
            Console.WriteLine($"CNPJ {cnpjValido} é válido? R: {(Cnpj.EhValido(cnpjValido) ? "Sim" : "Não")}");
            Console.WriteLine();

            const string cnpjInvalido = "337414880001677";
            Console.WriteLine("Validando CNPJ inválido");
            Console.WriteLine($"CNPJ {cnpjInvalido} é válido? R: {(Cnpj.EhValido(cnpjInvalido) ? "Sim" : "Não")}");
            Console.WriteLine();

            Cnpj.Formatar(cnpjValido, out var cnpjFormatado);
            Console.WriteLine("Formatando CNPJ");
            Console.WriteLine($"Antes: {cnpjValido} >> Depois: {cnpjFormatado}");
            Console.WriteLine();

            #endregion

            #region CPF
            Console.WriteLine(">>> CPF");
            Console.WriteLine();

            var cpfNovoFormatado = Cpf.Novo(true);
            Console.WriteLine("CPF gerado de forma aleatória (Formatado)");
            Console.WriteLine(cpfNovoFormatado);
            Console.WriteLine();

            var cpfNovoNaoFormatado = Cpf.Novo(false);
            Console.WriteLine("CPF gerado de forma aleatória (Não formatado)");
            Console.WriteLine(cpfNovoNaoFormatado);
            Console.WriteLine();

            const string cpfValido = "34751646044";
            Console.WriteLine("Validando CPF válido");
            Console.WriteLine($"CNPJ {cpfValido} é válido? R: {(Cpf.EhValido(cpfValido) ? "Sim" : "Não")}");
            Console.WriteLine();

            const string cpfInvalido = "34751646045";
            Console.WriteLine("Validando CPF inválido");
            Console.WriteLine($"CNPJ {cpfInvalido} é válido? R: {(Cpf.EhValido(cpfInvalido) ? "Sim" : "Não")}");
            Console.WriteLine();

            Cpf.Formatar(cpfValido, out var cpfFormatado);
            Console.WriteLine("Formatando CPF");
            Console.WriteLine($"Antes: {cpfValido} >> Depois: {cpfFormatado}");
            Console.WriteLine();
            #endregion

            Console.ReadLine();
        }
    }
}