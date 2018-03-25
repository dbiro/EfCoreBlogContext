using System;

namespace EfCoreBlogContext
{
    class ConsoleOuputWriter : IOutputWriter
    {
        public void WriteLine(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Console.WriteLine(message);
        }
    }
}
