using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace anci.VideoEncoder
{
    public class Constants
    {
        public Dictionary<String, String> AudioFormats { get; set; } = new Dictionary<string, string>(10);
        public Dictionary<String, String> Extensions { get; set; } = new Dictionary<string, string>(10);
        public Dictionary<String, String> Resolutions { get; set; } = new Dictionary<string, string>(10);
        public Dictionary<String, String> Destinations { get; set; } = new Dictionary<string, string>(10);
        public Dictionary<String, String> VideoFormats { get; set; } = new Dictionary<string, string>(10);
        public Dictionary<String, String> BitRatesVideo { get; set; } = new Dictionary<string, string>(10);
        public Dictionary<String, String> BitRatesAudio { get; set; } = new Dictionary<string, string>(10);
        public string BasePattern { get; set; } = " -y -i {SOURCE} {RESOLUTION} {VIDEO} {AUDIO} -strict -1 {BITRATEV} {BITRATEA} {SUBREMOVE} -map 0 {DEST}";
        public string ConcatPattern { get; set; } = "-f concat -safe 0 -i \"{TEMPFILE}\" -c copy {DEST}";

        public string ffmpeg_exe { get; set; } = Path.Combine(Application.StartupPath, "ffmpeg.exe");

        public static String settings_file = Path.Combine(Application.StartupPath, "custom.json");


        public static Constants Init()
        {
            try
            {
                if (File.Exists(settings_file))
                {
                    JavaScriptSerializer settings_reader = new JavaScriptSerializer();
                    Constants obj = (Constants)settings_reader.Deserialize(File.ReadAllText(settings_file), typeof(Constants));
                    return obj;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading custom settings file: {ex.Message}");
            }

            return new Constants().InitDefaults();
        }

        public Constants InitDefaults()
        {
            VideoFormats.Add("<copy as is>", "-c:v copy");
            VideoFormats.Add("<from file extension>", "");
            VideoFormats.Add("MP4 / h264", "-c:v h264");
            VideoFormats.Add("h265 / hevc", "-c:v hevc");
            VideoFormats.Add("vp9", "-c:v vp9");
            VideoFormats.Add("webm", "-c:v webm");

            AudioFormats.Add("<copy as is>", "-c:a copy");
            AudioFormats.Add("MP3", "-c:a mp3");
            AudioFormats.Add("AAC", "-c:a aac");
            AudioFormats.Add("AC3", "-c:a ac3");

            Extensions.Add(".mp4", ".mp4");
            Extensions.Add(".avi", ".avi");
            Extensions.Add(".mkv", ".mkv");

            Destinations.Add("<same folder>", "SAME");
            Destinations.Add("<specific folder>", "SPECIFIC");

            Resolutions.Add("<as is>", "");
            Resolutions.Add("1080p", "-s hd1080");
            Resolutions.Add("720p", "-s hd720");
            Resolutions.Add("576p", "-s pal");
            Resolutions.Add("480p", "-s ntsc");

            BitRatesVideo.Add("<as is>", "");
            BitRatesVideo.Add("500k", "500k");
            BitRatesVideo.Add("1000k", "1000k");
            BitRatesVideo.Add("1500k", "1500k");
            BitRatesVideo.Add("2000k", "2000k");

            BitRatesAudio.Add("<as is>", "");
            BitRatesAudio.Add("128k", "128k");
            BitRatesAudio.Add("192k", "192k");
            BitRatesAudio.Add("256k", "256k");
            BitRatesAudio.Add("320k", "320k");

            SaveDefaultFile();

            return this;
        }

        private void SaveDefaultFile()
        {
            try
            {
                JavaScriptSerializer settings_reader = new JavaScriptSerializer();
                String ser = settings_reader.Serialize(this);
                File.WriteAllText(settings_file, ser);
            }
            catch { }
        }

        internal static string BitRateAString(string value)
        {
            return String.IsNullOrEmpty(value) ? "" : $"-b:a {value}";
        }

        internal static string BitRateVString(string value)
        {
            return String.IsNullOrEmpty(value) ? "" : $"-b:v {value} -maxrate {value} -bufsize 1M";
        }
    }
}
