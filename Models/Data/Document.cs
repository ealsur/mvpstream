namespace MVPStream.Models.Data
{
    public abstract class Document
    {
        public Document(string t)
        {
            type = t;
        }
        public string type { get; private set; }
    }
}
