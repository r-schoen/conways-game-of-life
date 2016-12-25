using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IniLoader
{
    public class IniSettings : IEnumerable
    {
        private List<Setting> settings;

        public IniSettings()
        {
            settings = new List<Setting>();
        }

        /// <summary>
        /// Allows you to get (and set?) the value by name
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Setting this[string str]
        {
            get
            {
                return settings.Where(x => x.Item == str).First();
            }
            // TODO write setter
        }

        public void GetSettings()
        {

        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        

    }
    public struct Setting
    {
        public string Item;
        public double Value;
    }
}
