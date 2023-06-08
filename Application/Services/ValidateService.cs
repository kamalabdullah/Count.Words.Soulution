using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ValidateService : IValidateService
    {
        public bool ValidatePath(string dirPath)
        {
            if (!string.IsNullOrWhiteSpace(dirPath))
            {
                DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(dirPath));
                return dir.Exists;
            }
           return false;
        }
    }
}
