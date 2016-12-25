using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GameOfLife_WinProj.Classes
{
    public static class XML_Serializer
    {
        public static void SerializeSettings(object obj)
        {
            XmlSerializer x = new XmlSerializer(obj.GetType());
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("settings.xml", settings))
            {
                x.Serialize(writer, obj);
            }
        }

        public static Settings DeserializeSettings(string fileName)
        {
            Settings settings;
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            FileStream fs = new FileStream(fileName, FileMode.Open);
            settings = (Settings)serializer.Deserialize(fs);

            return settings;

        }
    }
    [Serializable]
    public class Settings
    {
        // the number of blocks
        public int GameHeight { get; set; }
        public int GameWidth { get; set; }

        // for use with the random generator
        public byte ChanceOfLife { get; set; }

        // pixels in a cell
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }

        // width and height of the window in pixels
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }

        // min amount of time per update (in seconds)
        public float TurnTime { get; set; }

        public bool RecordGame { get; set; }
    }

    public static class Misc
    {
        public static Settings GameSettings;
        public static void GetSettings()
        {
            GameSettings = XML_Serializer.DeserializeSettings("settings.xml");
        }
        public static void SetWinDemensionsToGame()
        {
            // declare float variables
            float cellWidth = GameSettings.CellWidth,
                cellHeight = GameSettings.CellHeight;

            // adjust cell width and height
            cellWidth = (float)GameSettings.WindowWidth / (float)GameSettings.GameWidth;
            cellHeight = (float)GameSettings.WindowHeight / (float)GameSettings.GameHeight;

            // set values (this truncates the floats)
            GameSettings.CellHeight = (int)cellHeight;
            GameSettings.CellWidth = (int)cellWidth;

            // then adjust the window width and height
            GameSettings.WindowWidth = GameSettings.CellWidth * GameSettings.GameWidth;
            GameSettings.WindowHeight = GameSettings.CellHeight * GameSettings.GameHeight;
        }
    }
}
