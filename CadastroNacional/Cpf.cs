using System;
using System.Linq;

namespace CadastroNacional.PessoaFisica
{
    /// <summary>
    /// Classe para tratar CPF
    /// </summary>
    public static class Cpf
    {
        /// <summary>
        /// Recebe um CPF sem formatação e o retorna formatado
        /// </summary>
        /// <param name="cpfEntrada">CPF sem pontuação com 11 dígitos</param>
        /// <param name="cpfSaida">CPF formtado</param>
        /// <returns>Indica se conseguiu formatar o CPF informado</returns>
        public static bool Formatar(string cpfEntrada, out string cpfSaida)
        {
            var cpfValido = EhValido(cpfEntrada);
            
            cpfSaida = cpfValido
                ? $"{cpfEntrada[..3]}.{cpfEntrada[3..6]}.{cpfEntrada[6..9]}-{cpfEntrada[^2..]}"
                : string.Empty;
            
            return cpfValido;
        }

        /// <summary>
        /// Verifica se o CPF informado é válido
        /// </summary>
        /// <param name="cpf">CPF sem pontuação com 11 dígitos</param>
        /// <returns>Indica se o CPF é válido</returns>
        public static bool EhValido(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11 || cpf.Any(c => !char.IsDigit(c)))
                return false;

            var cpfValido = Novo(false, cpf[..9]);

            return cpfValido == cpf;
        }

        /// <summary>
        /// Gerar um novo CPF a cada execução
        /// </summary>
        /// <param name="formatar">Se deve retornar formatado</param>
        /// <returns>Retorna um novo CPF a cada execução</returns>
        public static string Novo(bool formatar)
        {
            var baseCpfAleatorio = new Random().Next(111111111, 999999999).ToString();

            return Novo(formatar, baseCpfAleatorio);
        }

        private static string Novo(bool formatar, string cpfSemDv)
        { 
            var primeiroDv = GerarDV(cpfSemDv);

            var segundoDv = GerarDV(cpfSemDv, primeiroDv);

            var novoCpf = $"{cpfSemDv}{primeiroDv}{segundoDv}";

            if (formatar)
            {
                Formatar(novoCpf, out var cpfFormatado);

                return cpfFormatado;
            }
            
            return novoCpf;
        }

        private static string GerarDV(string cpfSemDv)
            => GerarDV(cpfSemDv, string.Empty);

        private static string GerarDV(string cpfSemDv, string primeiroDv)
        {
            const int moduloOnze = 11;

            var valorInicial = string.IsNullOrEmpty(primeiroDv) ? 10 : 11;

            if (!string.IsNullOrEmpty(primeiroDv))
                cpfSemDv += primeiroDv;

            var somaFatoresMultiplcados = cpfSemDv
                .Select((s, i) => Convert.ToInt32(char.GetNumericValue(s)) * (valorInicial - i))
                .Sum();

            var restoDiv = somaFatoresMultiplcados % moduloOnze;

            return (restoDiv < 2 ? 0 : moduloOnze - restoDiv).ToString();
        }
    }
}