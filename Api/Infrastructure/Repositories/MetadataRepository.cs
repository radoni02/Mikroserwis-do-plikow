using Core.Domain.Abstractions;
using Core.Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MetadataRepository : IMetaDataRepository
    {
        private readonly DataContext _db;

        public MetadataRepository(DataContext db)
        {
            _db = db;
        }
        public async Task<List<FileEntity>> GetFilesAsync()
        {
            return await _db.Files.ToListAsync();
            // List<FileEntity> file = new List<FileEntity>();
            //file.Add(new FileEntity() { Id = Guid.NewGuid(), Name="DokerFile" });
            //return  file;
        }
        public async Task<FileEntity> GetOneFile(Guid id)
        {
            var file = await _db.Files.FindAsync(id);
            return file;
        }
        public async Task DeleteFileAsync(FileEntity obj)
        {
            _db.Files.Remove(obj);
            await _db.SaveChangesAsync();
            await _db.Files.ToListAsync();
        }

        public async Task EditFileAsync(FileEntity obj)
        {
            _db.Files.Update(obj);
            await _db.SaveChangesAsync();
        }

        public async Task PostFileAsync(FileEntity obj)
        {
            await _db.Files.AddAsync(obj);
            await _db.SaveChangesAsync();
        }
    }
}
