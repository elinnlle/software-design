namespace MOSZoo.UI
{
    public class Beautiful
    {
        /// <summary>
        /// Метод для работы с цветом в консоли.
        /// </summary>
        public static void PrintBeautifullyWL(string message, ConsoleColor messageColor)
        {
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        
        /// <summary>
        /// Метод для работы с цветом в консоли (без перехода на новую строку).
        /// </summary>
        public static void PrintBeautifullyW(string message, ConsoleColor messageColor)
        {
            Console.ForegroundColor = messageColor;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}




