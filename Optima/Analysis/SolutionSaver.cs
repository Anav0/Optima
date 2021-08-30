using System.IO;
using Optima.Base;

namespace Optima.Analysis
{
    public abstract class SolutionSaver
    {
        private readonly string _additionalInfoHeader;
        protected readonly string FolderPath;
        private string _additionalInfo = "";
        private bool _append;
        protected string Filepath;
        protected StreamWriter Writer { get; private set; }

        public SolutionSaver(string folderPath, string filename, bool append = false, string additionalInfoHeader = "")
        {
            FolderPath = folderPath;
            _additionalInfoHeader = additionalInfoHeader;
            Directory.CreateDirectory(FolderPath);
            ClearFile(filename);
            ChangeFile(filename, append);
        }

        private void ClearFile(string filename)
        {
            File.Delete($"{FolderPath}/{filename}.csv");
        }

        public void ChangeFile(string filename, bool append = false)
        {
            _append = append;
            Filepath = $"{FolderPath}/{filename}.csv";
            Close();
            Open();
            var header = $"{GetHeader()},{_additionalInfoHeader}";
            Writer.WriteLine(header);
            Close();
        }

        public void Open()
        {
            Writer = new StreamWriter(Filepath, _append);
        }

        public void AppendToFile(Solution solution)
        {
            Writer.WriteLine($"{SolutionAsCsvRow(solution)},{_additionalInfo}");
        }

        public void Close()
        {
            Writer?.Close();
        }

        protected abstract string GetHeader();
        protected abstract string SolutionAsCsvRow(Solution solution);

        public void SetAdditionalInfo(string info)
        {
            _additionalInfo = info;
        }
    }
}