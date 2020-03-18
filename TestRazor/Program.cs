using System;
using System.IO;
using RazorHosting;

namespace ConsoleApp4
{
    class Context
    {
        public string Message { get; set; } = "Hello World!";
    }

    class Program
    {
        private const string TEMPLATE = @"
@inherits RazorHosting.RazorTemplateBase
@{
    var message = Context.Message;
    <tag>@message</tag>;
}
";
        static void Main()
        {
            var razor = new RazorEngine<RazorTemplateBase>();
            razor.Configuration.CompileToMemory = false;

            try
            {
                var assemblyName = razor.ParseAndCompileTemplate(null, new StringReader(TEMPLATE));

                string output = null;
                if (assemblyName != null)
                {
                    output = razor.RenderTemplateFromAssembly(assemblyName, new Context());
                }
                if (output == null)
                {
                    Console.WriteLine(razor.ErrorMessage);
                    Console.WriteLine(razor.LastGeneratedCode);
                }
                else
                {
                    Console.WriteLine(output);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
