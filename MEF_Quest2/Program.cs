using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections.Generic;
using System.IO;

namespace MEF_Quest2
{
    class Program
    {
        static void Main(string[] args)
        {
            Host host = new Host();
            host.Run();
            Console.Read();
        }
    }

    internal class Host
    {
        [ImportMany(typeof(ILogger))]
        protected IEnumerable<ILogger> _tabs = null;

        public void Run()
        {
            var container = new CompositionContainer();
            container.ComposeParts(this, new TextFileMessage(), new ConsoleMessage());
            foreach (var tab in _tabs)
            {
                tab.WriteMessage("Hello !");
            }
        }
    }

    internal interface ILogger
    {
        void WriteMessage(string message);
    }

    [Export(typeof(ILogger))]
    internal class TextFileMessage : ILogger
    {
        public void WriteMessage(string message)
        {
            TextWriter textWrite = new StreamWriter(@"..\netcoreapp3.1\textwriter.txt");
            textWrite.Write(message);
            textWrite.Flush();
            textWrite.Close();
            textWrite = null;

        }
    }

    [Export(typeof(ILogger))]
    internal class ConsoleMessage : ILogger
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
