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
    public class ClassService
    {
        private readonly Guid _userId;

        public ClassService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateClass(ClassCreate model)
        {
            var entity =
                new Class()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Description = model.Description,
                    PrimaryGun = model.PrimaryGun,
                    PrimaryAttach = model.PrimaryAttach,
                    SecondaryGun = model.SecondaryGun,
                    SecondaryAttach = model.SecondaryAttach,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Classes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ClassListItem> GetClasses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Classes
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ClassListItem
                                {
                                    ClassId = e.ClassId,
                                    Name = e.Name,
                                    Description = e.Description,
                                    PrimaryGun = e.PrimaryGun,
                                    PrimaryAttach = e.PrimaryAttach,
                                    SecondaryGun = e.SecondaryGun,
                                    SecondaryAttach = e.SecondaryAttach,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public ClassDetail GetClassById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Classes
                        .Single(e => e.ClassId == id && e.OwnerId == _userId);
                return
                    new ClassDetail
                    {
                        ClassId = entity.ClassId,
                        Name = entity.Name,
                        Description = entity.Description,
                        PrimaryGun = entity.PrimaryGun,
                        PrimaryAttach = entity.PrimaryAttach,
                        SecondaryGun = entity.SecondaryGun,
                        SecondaryAttach = entity.SecondaryAttach,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateClass(ClassEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Classes
                        .Single(e => e.ClassId == model.ClassId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.PrimaryGun = model.PrimaryGun;
                entity.PrimaryAttach = model.PrimaryAttach;
                entity.SecondaryGun = model.SecondaryGun;
                entity.SecondaryAttach = model.SecondaryAttach;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteClass(int classId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Classes
                        .Single(e => e.ClassId == classId && e.OwnerId == _userId);

                ctx.Classes.Remove(entity); 

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
