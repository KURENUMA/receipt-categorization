namespace templateApp.Shared
{
    public class Pram
    {
        public string Type { get; set; } = ""; // パラメータの型情報
        public object? Value { get; set; } = null; // パラメータの値
    }
    public class SearchGridPrams
    {
        public int Id { get; set; } //queryId
        public Pram? Pram1 { get; set; } = null;
        public Pram? Pram2 { get; set; } = null;
        public Pram? Pram3 { get; set; } = null;

    }
}