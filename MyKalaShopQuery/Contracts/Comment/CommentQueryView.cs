namespace MyKalaShopQuery.Contracts.Comment
{
    public class CommentQueryView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public long ParentId { get; set; }
        public string Parent { get; set; }
    }
}
