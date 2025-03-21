﻿
namespace InternetBanking.Core.Application.Helpers
{
    public static class CodeGenerator
    {
        public static int Unique9DigitsGenerator()
        {
            // Obtener la fecha y hora actual
            DateTime now = DateTime.Now;

            // Convertir la fecha y hora a un número entero representando la marca de tiempo en segundos
            long timestamp = ((DateTimeOffset)now).ToUnixTimeSeconds();

            // Obtener una instancia de Random para generar un número aleatorio
            Random random = new Random();

            // Generar un número aleatorio de 3 dígitos (entre 0 y 999)
            int randomNumber = random.Next(1000);

            // Combina el timestamp y el número aleatorio para formar el código único
            long uniqueCode = (timestamp * 1000) + randomNumber;

            //devolver el numero
            return (int)uniqueCode;
        }
    }
}
