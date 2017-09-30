using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace projeto.Controllers
{
    public class CalculadoraController : Controller
    {
        public String Index()
        {
            return "Olá Mundo!";
        }
        public String NovaMensagem(string valor = "padrão")
        {
            return "O valor passado foi: " + valor;
        }
        public string Somar(decimal valor1 = 0, decimal valor2 = 0)
        {
            return string.Format("{0:N4} + {1:N4} = {2:N4}", valor1, valor2, valor1 + valor2);
            //decimal valorSomado = valor1 + valor2;
            //return valorSomado;
        }
        public String Subtrair(decimal valor1 = 0, decimal valor2 = 0)
        {
            return string.Format("{0:N4} - {1:N4} = {2:N4}", valor1, valor2, valor1 - valor2);
            //decimal valorSubtraido = valor1 - valor2;
            //return valorSubtraido;
        }
        public String Multiplicar(decimal valor1 = 1, decimal valor2 = 1)
        {
            return string.Format("{0:N4} * {1:N4} = {2:N4}", valor1, valor2, valor1 * valor2);
            //decimal valorMultiplicado = valor1 * valor2;
            //return valorMultiplicado; 
        }
        public String Dividir(decimal valor1 = 1, decimal valor2 = 1)
        {
            if (valor2 == 0) {
                return "Vacilaum, num dá pra didivir por zero mané";
            } else {

                return string.Format("{0:N4} / {1:N4} = {2:N4}", valor1, valor2, valor1 / valor2);
                //decimal valorDividido = valor1 / valor2;
                //return valorDividido;
            }
        }
    }
}