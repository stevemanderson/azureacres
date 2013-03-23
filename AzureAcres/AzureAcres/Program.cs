using System;

namespace AzureAcres
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (AzureAcres game = new AzureAcres())
            {
                game.Run();
            }
        }
    }
#endif
}

