using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesUsingEF.data
{
    public class ImageRepository
    {
        private string _connectionString;

        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Image> GetAllImages()
        {
            using var context = new ImagesDBContext(_connectionString);

            return context.Images.ToList();
        }

        public Image GetById(int id)
        {
            using var context = new ImagesDBContext(_connectionString);

            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void Upload(string file, string imageName)
        {
            using var context = new ImagesDBContext(_connectionString);
            context.Add(new Image
            {
                File = file,
                ImageName = imageName,
                Likes = 0,
                UploadTime = DateTime.Now
            });

            context.SaveChanges();
        }

        public void Like(int id)
        {
            using var context = new ImagesDBContext(_connectionString);

            var image = context.Images.FirstOrDefault(i => i.Id == id);

            image.Likes++;

            context.SaveChanges();

        }
    }
}
