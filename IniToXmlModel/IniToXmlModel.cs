namespace IniToXmlModel
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.IO;

    public class IniToXmlModel
    {
        /// <summary>
        /// Converts the ini file to xml
        /// </summary>
        /// <param name="filePath">The file path of the ini file</param>
        /// <param name="appRootElement">The root element of the new xml example My App Root</param>
        /// <returns>Returns XDocument</returns>
        public static XDocument ConvertIni(string filePath, string appRootElement)
        {
            StreamReader reader = new StreamReader(filePath);
            StringBuilder currentLine = new StringBuilder();
            //Creating our document
            XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(appRootElement));

            //creating a xelement to attach the root
            XElement root = new XElement("root");

            //start reading the ini file
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.Contains("["))
                    {
                        currentLine.Append(line.Substring(1, line.Length - 2));
                        root = new XElement(currentLine.ToString());
                        doc.Root.Add(root);
                        currentLine.Clear();
                    }
                    else if (line.Length != 0)
                    {
                        string[] values = line.Split('=');
                        root.Add(new XElement(root.Name,
                            new XAttribute("Name", values[0]),
                            new XAttribute("Value", values[1])));
                    }
                    line = reader.ReadLine();

                }
            }
            return doc;
        }

        /// <summary>
        /// Reads all lines in file and add \n to format each line
        /// </summary>
        /// <param name="filePath">Path of the file to be read</param>
        /// <returns>String with the content of the file</returns>
        public static string ReadEntireFile(string filePath)
        {
            StringBuilder fileText = new StringBuilder();

            try
            {
                string[] linesFile = File.ReadAllLines(filePath);

                foreach (var line in linesFile)
                {
                    fileText.Append(line);
                    fileText.Append("\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return fileText.ToString();
        }
    }
}
