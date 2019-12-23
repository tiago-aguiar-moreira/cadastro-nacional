using System;
using System.Linq;

namespace CadastroNacional.PessoaFisica
{
    public static class Cpf
    {
        public static bool Formatar(string cpfEntrada, out string cpfSaida)
        {
            if (string.IsNullOrEmpty(cpfEntrada) || cpfEntrada.Length != 11 || cpfEntrada.Any(c => !char.IsDigit(c)))
            {
                cpfSaida = string.Empty;
                return false;
            }
            else
            {
                cpfSaida = $"{cpfEntrada[0]}{cpfEntrada[1]}{cpfEntrada[2]}.{cpfEntrada[3]}{cpfEntrada[4]}{cpfEntrada[5]}.{cpfEntrada[6]}{cpfEntrada[7]}{cpfEntrada[8]}-{cpfEntrada[9]}{cpfEntrada[10]}";
                return true;
            }
        }

        public static bool EhValido(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11 || cpf.Any(c => !char.IsDigit(c)))
            {
                return false;
            }

            var cpfValido = Novo(false, cpf[0..^2]);

            return cpfValido == cpf;
        }

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
            else
            {
                return novoCpf;
            }
        }

        private static string GerarDV(string cpfSemDv)
            => GerarDV(cpfSemDv, string.Empty);

        private static string GerarDV(string cpfSemDv, string primeiroDv)
        {
            const int moduloOnze = 11;

            var valorInicial = string.IsNullOrEmpty(primeiroDv) ? 10 : 11;

            if (!string.IsNullOrEmpty(primeiroDv))
            {
                cpfSemDv += primeiroDv;
            }

            var somaFatoresMultiplcados = cpfSemDv.Select((s, i) => Convert.ToInt32(char.GetNumericValue(s)) * (valorInicial - i)).Sum();

            var restoDiv = somaFatoresMultiplcados % moduloOnze;

            return (restoDiv < 2 ? 0 : moduloOnze - restoDiv).ToString();
        }
    }
}