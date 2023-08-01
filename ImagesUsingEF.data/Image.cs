namespace ImagesUsingEF.data
{
    public class Image
    {
        public int Id { get; set; }

        public string File { get; set; }

        public string ImageName { get; set; }

        public int Likes { get; set; }

        public DateTime UploadTime { get; set; }

    }
}