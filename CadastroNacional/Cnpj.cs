using System;
using System.Linq;

namespace CadastroNacional.PessoaJuridica
{
    /// <summary>
    /// Classe para tratar CNPJ
    /// </summary>
    public static class Cnpj
    {
        /// <summary>
        /// Recebe um CNPJ sem formatação e o retorna formatado
        /// </summary>
        /// <param name="cnpjEntrada">CNPJ sem pontuação com 14 dígitos</param>
        /// <param name="cnpjSaida">CNPJ formtado</param>
        /// <returns>Indica se conseguiu formatar o CNPJ informado</returns>
        public static bool Formatar(string cnpjEntrada, out string cnpjSaida)
        {
            var cnpjValido = EhValido(cnpjEntrada);

            cnpjSaida = cnpjValido
                ? $"{cnpjEntrada[..2]}.{cnpjEntrada[2..5]}.{cnpjEntrada[5..8]}/{cnpjEntrada[8..12]}-{cnpjEntrada[^2..]}"
                : string.Empty;

            return cnpjValido;
        }

        /// <summary>
        /// Verifica se o CNPJ informado é válido
        /// </summary>
        /// <param name="cnpj">CNPJ sem pontuação com 14 dígitos</param>
        /// <returns>Indica se o CNPJ é válido</returns>
        public static bool EhValido(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || cnpj.Length != 14 || cnpj.Any(c => !char.IsDigit(c)))
                return false;

            var cnpjValido = Novo(false, cnpj[..12]);

            return cnpjValido == cnpj;
        }

        /// <summary>
        /// Gerar um novo CNPJ a cada execução
        /// </summary>
        /// <param name="formatar">Se deve retornar formatado</param>
        /// <returns>Retorna um novo CNPJ a cada execução</returns>
        public static string Novo(bool formatar)
        {
            var random = new Random();

            var inscricao = random.Next(11111111, 99999999).ToString();

            var filial = random.Next(1, 9999).ToString().PadLeft(4, '0');

            return Novo(formatar, inscricao + filial);
        }

        private static string Novo(bool formatar, string cnpjSemDv)
        {
            var primeiroDv = GerarDV(cnpjSemDv);

            var segundoDv = GerarDV(cnpjSemDv, primeiroDv);

            var novoCnpj = $"{cnpjSemDv}{primeiroDv}{segundoDv}";

            if (formatar)
            {
                Formatar(novoCnpj, out var cnpjFormatado);

                return cnpjFormatado;
            }
            
            return novoCnpj;
        }

        private static string GerarDV(string cnpjDv)
            => GerarDV(cnpjDv, string.Empty);

        private static string GerarDV(string cnpjSemDigito, string primeiroDv)
        {
            const int moduloOnze = 11;

            if (!string.IsNullOrEmpty(primeiroDv))
                cnpjSemDigito += primeiroDv;

            var valoresParaMultipicar = string.IsNullOrEmpty(primeiroDv)
                ? new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 }
                : new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var somaFatoresMultiplcados = valoresParaMultipicar
                .Select((s, i) => s * char.GetNumericValue(cnpjSemDigito[i]))
                .Sum();

            var restoDiv = somaFatoresMultiplcados % moduloOnze;

            return (restoDiv < 2 ? 0 : moduloOnze - restoDiv).ToString();
        }
    }
}