﻿using BlueBadgeProject.Data;
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
    }
}
