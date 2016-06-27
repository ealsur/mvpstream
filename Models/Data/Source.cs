namespace MVPStream.Models.Data
{
    public enum SourceType:int
    {
        RSS=0,
        YouTube=1,
        Vimeo=2
    }
    public class Source
    {
        public SourceType Type { get; set; }
        public string Url { get; set; }
    }
}
