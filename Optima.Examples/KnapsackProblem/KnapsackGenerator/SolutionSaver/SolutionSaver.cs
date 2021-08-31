using System;
using System.IO;
using Optima.Base;

namespace Optima.Examples.KnapsackGenerator
{
    public abstract class SolutionSaver
    {
        private bool _append;
        protected StreamWriter Writer { get; private set; }
        protected string Fullpath;
        protected string MASTER_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/CSV";
        private string _additionalInfo = "";
        private string _additionalInfoHeader = "";

        public SolutionSaver(string filename, bool append = false, string additionalInfoHeader = "")
        {
            _additionalInfoHeader = additionalInfoHeader;
            Directory.CreateDirectory(MASTER_PATH);
            ClearFile(filename);
            ChangeFile(filename, append);
        }

        private void ClearFile(string filename)
        {
            File.Delete($"{MASTER_PATH}/{filename}.csv");
        }

        public void ChangeFile(string filename, bool append = false)
        {
            _append = append;
            Fullpath = $"{MASTER_PATH}/{filename}.csv";
            Close();
            Open();
            var header = $"{GetHeader()},{_additionalInfoHeader}";
            Writer.WriteLine(header);
            Close();
        }

        public void Open()
        {
            Writer = new(Fullpath, _append);
        }

        public void AppendToFile(Solution solution)
        {
            var row = $"{SolutionAsCsvRow(solution)},{_additionalInfo}";
            Writer.WriteLine(row);
        }

        public void Close() => Writer?.Close();

        protected abstract string GetHeader();
        protected abstract string SolutionAsCsvRow(Solution solution);

        public void SetAdditionalInfo(string info) => _additionalInfo = info;
    }
}