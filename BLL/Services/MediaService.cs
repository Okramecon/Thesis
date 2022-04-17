using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using DAL.EF;
using System.IO;
using DAL.Entities;
using Common.Exceptions;
using System.Collections.Generic;
using Model.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Common.Extensions;

namespace BLL.Services
{
    public class MediaService
    {
        private AppDbContext AppDbContext { get; set; }

        public MediaService(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        private List<string> AllowedFileExtensions = new List<string>
        {
            ".png",
            ".jpg",
            ".jpeg"
        };

        public async Task<Guid> Add(IFormFile file, string path)
        {
            if (file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName);

                if(!AllowedFileExtensions.Contains(extension))
                {
                    throw new InnerException("Cant submit such file", "8327d2df-f076-4294-a3de-320659161ed9");
                }
                
                var entity = new Media
                {
                    Extension = extension
                };
                AppDbContext.Medias.Add(entity);
                await AppDbContext.SaveChangesAsync();

                var fileName = entity.Id.ToString() + extension;
                string filePath = Path.Combine(path, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return entity.Id;
            }

            throw new InnerException("Provided file is empty!", "d435cb1a-f7b3-420c-b746-3e1a8ae7195a");
        }

        public async Task<List<MediaModels.ListOut>> GetList(List<Guid> ids, string path)
        {
            var medias = await AppDbContext.Medias
                .Where(x => ids.Contains(x.Id))
                .ProjectToType<MediaModels.ListOut>()
                .ToListAsync();

            foreach(var media in medias)
            {
                media.FileName = media.Id.ToString() + media.Extension;
                media.ContentType = media.Extension.GetMimeType();
            }

            return medias;
        }
    }
}
