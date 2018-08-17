using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrowler
{
    class DiskUtility
    {
        string dirName;
        public DiskUtility(string dirName)
        {
            
            this.dirName = dirName;
        }
        internal async Task SaveOnDisk(string content, string name)
        {
            SanitizeName(ref name);
            SanitizeName(ref dirName);
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, dirName));
            var filepath = Path.Combine(Environment.CurrentDirectory, dirName, name);
            
            try
            {
                
                using (FileStream fileStream = new FileStream(filepath, FileMode.Append, FileAccess.Write))
                {
                    var encodedByteContentBuffer = Encoding.Unicode.GetBytes(content);
                    await fileStream.WriteAsync(encodedByteContentBuffer, 0, encodedByteContentBuffer.Length).ConfigureAwait(false);
                }
            }
            //TODO logging errors
            catch (Exception ex) { }
        }

        private void SanitizeName(ref string name)
        {
            foreach (char symbol in ":/\\<>?*|\"")
            {
                name = name.Replace(symbol, ' ');
            }

        }
    }
}