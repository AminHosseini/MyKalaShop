using _0_Framework.Domain;

namespace ShopManagement.Domain.SlideAgg
{
    public class Slide : EntityBase
    {
        public string PicturePath { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string ButtonText { get; private set; }
        public string Link { get; private set; }
        public bool IsDeleted { get; private set; }

        public Slide(string picturePath, string pictureAlt, string pictureTitle,
            string heading, string title, string text, string buttonText, string link)
        {
            PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            ButtonText = buttonText;
            Link = link;
            IsDeleted = false;
        }

        public void Edit(string picturePath, string pictureAlt, string pictureTitle,
            string heading, string title, string text, string buttonText, string link)
        {
            if (!string.IsNullOrWhiteSpace(picturePath))
                PicturePath = picturePath;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            ButtonText = buttonText;
            Link = link;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
