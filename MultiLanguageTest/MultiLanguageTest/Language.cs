using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiLanguageTest
{
    public class Language
    {
        Properties.Resources resources;
        private void LoadLanguage(string language = "")
        {
            if (string.IsNullOrEmpty(language))
            {
                language = Thread.CurrentThread.CurrentUICulture.Name;
            }
        }
    }

    
}
