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
    public class GunService
    {
        private readonly Guid _userId;

        public GunService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateGun(GunCreate model)
        {
            var entity =
                new Gun()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Description = model.Description,
                    IsPrimary = model.IsPrimary,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Guns.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GunListItem> GetGuns()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Guns
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new GunListItem
                                {
                                    GunId = e.GunId,
                                    Name = e.Name,
                                    Description = e.Description,
                                    IsPrimary = e.IsPrimary,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public GunDetail GetGunById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Guns
                        .Single(e => e.GunId == id && e.OwnerId == _userId);
                return
                    new GunDetail
                    {
                        GunId = entity.GunId,
                        Name = entity.Name,
                        Description = entity.Description,
                        IsPrimary = entity.IsPrimary,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateGun(GunEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Guns
                    .Single(e => e.GunId == model.GunId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.IsPrimary = model.IsPrimary;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
