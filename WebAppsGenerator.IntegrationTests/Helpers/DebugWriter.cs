using System.Diagnostics;
using System.IO;
using System.Text;

namespace WebAppsGenerator.IntegrationTests.Helpers
{
    /// <summary>
    /// Text writer that writes all given text to debug console
    /// </summary>
    class DebugWriter : TextWriter
    {        
        public override void WriteLine(string value)
        {
            Debug.WriteLine(value);
            base.WriteLine(value);
        }

        public override void Write(string value)
        {
            Debug.Write(value);
            base.Write(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }
}