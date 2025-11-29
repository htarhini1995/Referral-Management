using FleetManagement.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FleetManagement.EF.Storage
{
    public class DocumentService
    {
        private readonly string _rootPath;

        public DocumentService(IOptions<FileStorageOptions> options)
        {
            // Base folder where the app runs
            var basePath = Directory.GetCurrentDirectory();
            _rootPath = Path.Combine(basePath, options.Value.RootPath);
            Directory.CreateDirectory(_rootPath);
        }

        public async Task<StoredDocument> UploadAsync(
            string fileName,
            string contentType,
            long sizeBytes,
            Stream fileStream,
            string uploadedBy,
            CancellationToken ct)
        {
            if (fileStream == null || sizeBytes <= 0)
                throw new ArgumentException("File stream is empty.", nameof(fileStream));

            // Save file to disk
            var ext = Path.GetExtension(fileName);
            var newName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(_rootPath, newName);

            await using (var fs = File.Create(fullPath))
            {
                await fileStream.CopyToAsync(fs, ct);
            }

            // Save metadata to DB
            using var db = ReferralDbContext.Factory!.CreateDbContext();

            var entity = new StoredDocument
            {
                OriginalFileName = fileName,
                StoragePath = newName,
                ContentType = contentType,
                SizeBytes = sizeBytes,
                UploadedAtUtc = DateTime.UtcNow,
                UploadedBy = uploadedBy
            };

            db.StoredDocuments.Add(entity);
            await db.SaveChangesAsync(ct);

            return entity;
        }

        public async Task<DocumentContentResult?> GetContentAsync(long id, CancellationToken ct)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();

            var entity = await db.StoredDocuments
                .FirstOrDefaultAsync(d => d.Id == id, ct);

            if (entity == null)
                return null;

            var fullPath = Path.Combine(_rootPath, entity.StoragePath);
            if (!File.Exists(fullPath))
                return null;

            await using var fs = File.OpenRead(fullPath);
            await using var ms = new MemoryStream();
            await fs.CopyToAsync(ms, ct);

            return new DocumentContentResult
            {
                Content = ms.ToArray(),
                FileName = entity.OriginalFileName,
                ContentType = entity.ContentType
            };
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct)
        {
            using var db = ReferralDbContext.Factory!.CreateDbContext();

            var entity = await db.StoredDocuments
                .FirstOrDefaultAsync(d => d.Id == id, ct);

            if (entity == null)
                return false;

            var fullPath = Path.Combine(_rootPath, entity.StoragePath);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            db.StoredDocuments.Remove(entity);
            await db.SaveChangesAsync(ct);

            return true;
        }
    }
}
