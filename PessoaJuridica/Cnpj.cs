using System;
using System.Linq;

namespace CadastroNacional.PessoaJuridica
{
    public class Cnpj
    {
        public static bool Formatar(string cnpjEntrada, out string cnpjSaida)
        {
            if (string.IsNullOrEmpty(cnpjEntrada) || cnpjEntrada.Length != 15 || cnpjEntrada.Any(c => !char.IsDigit(c)))
            {
                cnpjSaida = string.Empty;
                return false;
            }
            else
            {
                cnpjSaida = $"{cnpjEntrada[0]}{cnpjEntrada[1]}{cnpjEntrada[2]}.{cnpjEntrada[3]}{cnpjEntrada[4]}{cnpjEntrada[5]}.{cnpjEntrada[6]}{cnpjEntrada[7]}{cnpjEntrada[8]}-{cnpjEntrada[9]}{cnpjEntrada[10]}";
                return true;
            }
        }

        public static bool EhValido(string cnpj)
        {
            //if (string.IsNullOrEmpty(cnpj) || cnpj.Length != 14 || cnpj.Any(c => !char.IsDigit(c)))
            //{
            //    return false;
            //}

            throw new NotImplementedException();
        }

        public static string Novo(bool formatar)
        {
            var inscricao = new Random().Next(11111111, 99999999).ToString();

            var filial = new Random().Next(1, 9999).ToString().PadLeft(4, '0');

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
            else
            {
                return novoCnpj;
            }
        }

        private static string GerarDV(string cnpjDv)
            => GerarDV(cnpjDv, string.Empty);

        private static string GerarDV(string cnpjSemDigito, string primeiroDv)
        {
            const int moduloOnze = 11;

            if (!string.IsNullOrEmpty(primeiroDv))
            {
                cnpjSemDigito += primeiroDv;
            }

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