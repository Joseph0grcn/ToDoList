using System;


namespace todolist
{
    internal class ListItem
    {
        public int Index { get; set; }
        public string Content { get; set; }
        public int ImportanceLevel { get; set; }
        public int TotalTaskCount { get; set; } = 10;
        public int CompletedTaskCount { get; set; } = 0;
        public bool Visibility {  get; set; } = true;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;

    }
}
