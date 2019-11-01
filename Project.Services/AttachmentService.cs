using BlueBadgeProject.Data;
using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class AttachmentService
    {
        private readonly Guid _userId;

        public AttachmentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateAttachment(AttachmentCreate model)
        {
            var entity =
    new Attachment()
    {
        OwnerId = _userId,
        Name = model.Name,
        Description = model.Description,
        IsPrimary = model.IsPrimary,
        CreatedUtc = DateTimeOffset.Now
    };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Attachments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AttachmentListItem> GetAttachments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Attachments
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new AttachmentListItem
                                {
                                    AttachmentId = e.AttachmentId,
                                    Name = e.Name,
                                    Description = e.Description,
                                    IsPrimary = e.IsPrimary,
                                    CreatedUtc = DateTimeOffset.Now
                                }
                        );

                return query.ToArray();
            }
        }

        public AttachmentDetail GetAttachmentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attachments
                        .Single(e => e.AttachmentId == id && e.OwnerId == _userId);
                return
                    new AttachmentDetail
                    {
                        AttachmentId = entity.AttachmentId,
                        Name = entity.Name,
                        Description = entity.Description,
                        IsPrimary = entity.IsPrimary,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateAttachment(AttachmentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attachments
                        .Single(e => e.AttachmentId == model.AttachmentId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.IsPrimary = model.IsPrimary;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAttachment (int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Attachments
                        .Single(e => e.AttachmentId == noteId && e.OwnerId == _userId);

                ctx.Attachments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
